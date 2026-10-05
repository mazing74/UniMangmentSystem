using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UMS.BLL.InterFace;
using UMS.DAL.Models;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IunitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var DeptVm = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentVM>>(departments);
            return View(DeptVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentVM departmentVm)
        {
            var checkDepartment = _unitOfWork.DepartmentRepository.GetById(departmentVm.Id);
            if (checkDepartment is null)
            {
                if (ModelState.IsValid)
                {
                    var Dept = _mapper.Map<DepartmentVM, Department>(departmentVm);
                    _unitOfWork.DepartmentRepository.Add(Dept);
                    _unitOfWork.Compelete();
                    TempData["SuccessMessageOfDepartment"] = "Department created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View(departmentVm);
            }
            else
                return BadRequest();
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);

            if (department is null)
                return NotFound();
            var DeptVm = _mapper.Map<Department, DepartmentVM>(department);

            return View(ViewName, DeptVm);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id.Value, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentVM departmentVm, [FromRoute] int id)
        {
            if (id != departmentVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var Dept = _mapper.Map<DepartmentVM, Department>(departmentVm);

                    _unitOfWork.DepartmentRepository.Ubdate(Dept);
                    _unitOfWork.Compelete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(departmentVm);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id.Value, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentVM departmentVm, [FromRoute] int id)
        {
            if (id != departmentVm.Id)
            {
                return BadRequest();
            }

            try
            {
                var Dept = _mapper.Map<DepartmentVM, Department>(departmentVm);

                _unitOfWork.DepartmentRepository.Delete(Dept);
                _unitOfWork.Compelete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(departmentVm);
        }


    }
}
