using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UMS.BLL.InterFace;
using UMS.DAL.Models;
using UMS.PL.Helpers;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    public class EmpolyeeController : Controller
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmpolyeeController(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchedvalue)
        {
            var employees =  _unitOfWork.EmpolyeeRepository.SearchByName(searchedvalue);
            var empVm = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeVM>>(employees);
            return View(empVm);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeevm)
        {
            if (ModelState.IsValid)
            {

                if (employeevm.Image is not null)
                {
                    employeevm.ImageName = DocumentSettings.UploadFile(employeevm.Image, "Images");

                }
                var MappedEmp = _mapper.Map<EmployeeVM, Employee>(employeevm);
                await _unitOfWork.EmpolyeeRepository.AddAsync(MappedEmp);
               await _unitOfWork.CompeleteAsync();

                TempData["SuccessMessageOfEmpolyee"] = "Employee created successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(employeevm);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var empolyee = await _unitOfWork.EmpolyeeRepository.GetByIdAsync(id.Value);

            if (empolyee is null)
                return NotFound();

            var empVm = _mapper.Map<Employee, EmployeeVM>(empolyee);
            return View(ViewName, empVm);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return await Details(id.Value, "Edit");
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
                    if (employeevM.ImageName is not null && employeevM.Image is not null)
                    {
                        DocumentSettings.DeleteFile("Images", employeevM.ImageName);
                        employeevM.ImageName = DocumentSettings.UploadFile(employeevM.Image, "Images");

                    }
                    var employee = _mapper.Map<EmployeeVM, Employee>(employeevM);
                    _unitOfWork.EmpolyeeRepository.Ubdate(employee);
                    _unitOfWork.CompeleteAsync();

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeevM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id.Value, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }
            try
            {

                var employee = _mapper.Map<EmployeeVM, Employee>(employeeVm);
                 _unitOfWork.EmpolyeeRepository.Delete(employee);
                var result = await _unitOfWork.CompeleteAsync();
                if (result > 0 && employeeVm.ImageName is not null)
                {
                    DocumentSettings.DeleteFile("Images", employeeVm.ImageName);
                }
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
