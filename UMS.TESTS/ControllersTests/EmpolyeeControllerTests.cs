using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.BLL.InterFace;
using UMS.PL.Controllers;

namespace UMS.PL.TESTS.ControllersTests
{
    public class EmpolyeeControllerTests
    {
        private readonly EmpolyeeController _employeeController;
        private readonly Mock<IunitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;


        public EmpolyeeControllerTests()
        {

            _unitOfWork = new Mock<IunitOfWork>();

            _mapper = new Mock<IMapper>();

            _employeeController = new EmpolyeeController(_unitOfWork.Object, _mapper.Object);

        }



    }
}
