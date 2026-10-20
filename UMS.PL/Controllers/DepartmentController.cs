using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.BLL.InterFace;
using UMS.DAL.Models;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IunitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            var DeptVm = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentVM>>(departments);
            return View(DeptVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM departmentVm)
        {
            var checkDepartment = await _unitOfWork.DepartmentRepository.GetByIdAsync(departmentVm.Id);
            if (checkDepartment is null)
            {
                if (ModelState.IsValid)
                {
                    var Dept = _mapper.Map<DepartmentVM, Department>(departmentVm);
                  await  _unitOfWork.DepartmentRepository.AddAsync(Dept);
                  await  _unitOfWork.CompeleteAsync();
                    TempData["SuccessMessageOfDepartment"] = "Department created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View(departmentVm);
            }
            else
                return BadRequest();
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);

            if (department is null)
                return NotFound();
            var DeptVm = _mapper.Map<Department, DepartmentVM>(department);

            return View(ViewName, DeptVm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id.Value, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentVM departmentVm, [FromRoute] int id)
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
                   await _unitOfWork.CompeleteAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id.Value, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DepartmentVM departmentVm, [FromRoute] int id)
        {
            if (id != departmentVm.Id)
            {
                return BadRequest();
            }

            try
            {
                var Dept = _mapper.Map<DepartmentVM, Department>(departmentVm);

                _unitOfWork.DepartmentRepository.Delete(Dept);
               await  _unitOfWork.CompeleteAsync();
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
