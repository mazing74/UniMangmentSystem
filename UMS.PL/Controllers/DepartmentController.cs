using Microsoft.AspNetCore.Mvc;
using UMS.BLL.InterFace;
using UMS.DAL.Models;

namespace UMS.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            var checkDepartment = _departmentRepository.GetById(department.Id);
            if (checkDepartment is null)
            {
                if (ModelState.IsValid)
                {
                    _departmentRepository.Add(department);
                    return RedirectToAction(nameof(Index));
                }
                return View(department);
            }
            else
                return BadRequest();
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = _departmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();

            return View(ViewName, department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id.Value, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Ubdate(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(department);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id.Value, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);
        }


    }
}
