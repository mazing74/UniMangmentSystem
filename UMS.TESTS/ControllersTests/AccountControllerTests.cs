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
        //Login_InvalidModel_ReturnsLoginView
        //Login_InvalidCredentials_ReturnsLoginViewWithError
        //Login_ValidCredentials_RedirectsToHome
        //Register_ValidModel_CreatesUser
        //Register_DuplicateEmail_ReturnsRegisterView

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
    }
}