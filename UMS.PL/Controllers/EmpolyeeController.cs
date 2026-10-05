using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UMS.BLL.InterFace;
using UMS.DAL.Models;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    public class EmpolyeeController : Controller
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmpolyeeController(IunitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index(string searchedvalue)
        {
            var employees = _unitOfWork.EmpolyeeRepository.SearchByName(searchedvalue);
            var empVm = _mapper.Map<IEnumerable <Employee>,IEnumerable <EmployeeVM>>(employees);
            return View(empVm);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeVM employeevm)
        {
            if (ModelState.IsValid)
            {
                var MappedEmp = _mapper.Map<EmployeeVM, Employee>(employeevm);
                _unitOfWork.EmpolyeeRepository.Add(MappedEmp);
                _unitOfWork.Compelete();

                TempData["SuccessMessageOfEmpolyee"] = "Employee created successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(employeevm);
        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var empolyee = _unitOfWork.EmpolyeeRepository.GetById(id.Value);

            if (empolyee is null)
                return NotFound();

            var empVm = _mapper.Map<Employee, EmployeeVM>(empolyee);
            return View(ViewName, empVm);
        }
        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return Details(id.Value, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employeevM, [FromRoute] int id)
        {
            if (id != employeevM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeVM, Employee>(employeevM);
                    _unitOfWork.EmpolyeeRepository.Ubdate(employee);
                    _unitOfWork.Compelete();

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeevM);
        }
        public IActionResult Delete(int? id)
        {
            return Details(id.Value, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeVM employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }
            try
            {
                var employee = _mapper.Map<EmployeeVM, Employee>(employeeVm);
                _unitOfWork.EmpolyeeRepository.Delete(employee);
                _unitOfWork.Compelete();

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVm);
            }
        }

    }
}
