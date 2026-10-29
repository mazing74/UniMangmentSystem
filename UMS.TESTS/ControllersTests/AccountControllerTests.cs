using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UMS.DAL.Models;
using UMS.PL.Controllers;
using UMS.PL.ViewModels;

namespace UMS.TESTS
{
    public class AccountControllerTests
    {
        

        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly AccountController _controller;




        public AccountControllerTests()
        {
            // Arrange: Mock UserManager
            var userStore = new Mock<IUserStore<ApplicationUser>>();

            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStore.Object,
                null!, null!, null!, null!, null!, null!, null!, null!);

            // Arrange: Mock SignInManager
            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                null!,
                null!,
                null!,
                null!);

            _controller = new AccountController(
                _userManagerMock.Object,
                _signInManagerMock.Object);
        }
        #region Helpers
        private static RegisterViewModel ValidRegisterVM() => new()
        {
            FullName = "Mazin Test",
            Email = "mazin@example.com",
            Password = "Password123!",
            IsAgree = true
        };

        private static LoginViewModel ValidLoginVM() => new()
        {
            Email = "mazin@example.com",
            Password = "Password123!",
            RememberMe = false
        }; 
        #endregion

        #region Login

        [Fact]
        public void LoginGet_WhenCallIt_ReturnsViewOfLogin()
        {
            // Act
            var result = _controller.Login();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

     
        [Fact]
        public async Task LoginPost_ValidCrednetialsButTheEmailDoesNotExist_AddModelStatErrorInTheSameView()
        {
            var ValidLoginVm = new LoginViewModel()
            {
                Email = "Mazen@Gmail.com",
                Password = "P@@sw0rd",
                RememberMe = true
            };

            _userManagerMock.Setup(u => u.FindByEmailAsync(ValidLoginVm.Email)).ReturnsAsync((ApplicationUser?)null);

            //Act

            var result = await _controller.Login(ValidLoginVm);

            //Assert
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal(view.Model, ValidLoginVm);
            Assert.Equal("Invalid username or password", _controller.ModelState[" "]!.Errors.Single().ErrorMessage);

        } 
        #endregion


        #region Register

        [Fact]
        public void Register_Get_ReturnsView()
        {
            // Act
            var result = _controller.Register();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Register_ValidModel_RedirectsToLogin()
        {
            // Arrange
            var vm = ValidRegisterVM();
            _userManagerMock
                .Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), vm.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(vm);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AccountController.Login), redirect.ActionName);
        }

        [Fact]
        public async Task Register_ValidModel_SetsUserNameFromEmailPrefix()
        {
            // Arrange
            var vm = ValidRegisterVM();
            ApplicationUser? createdUser = null;

            _userManagerMock
                .Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), vm.Password))
                .Callback<ApplicationUser, string>((user, _) => createdUser = user)
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await _controller.Register(vm);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal("mazin", createdUser!.UserName);
            Assert.Equal(vm.Email, createdUser.Email);
            Assert.Equal(vm.FullName, createdUser.FullName);
        }

        #endregion

    }
}