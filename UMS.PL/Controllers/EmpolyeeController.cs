using Microsoft.AspNetCore.Mvc;
using UMS.BLL.InterFace;
using UMS.DAL.Models;

namespace UMS.PL.Controllers
{
    public class EmpolyeeController : Controller
    {
        private readonly IEmpolyeeRepository _empolyeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmpolyeeController(IEmpolyeeRepository empolyeeRepository,IDepartmentRepository departmentRepository)
        {
            _empolyeeRepository = empolyeeRepository;
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index(string searchedvalue)
        {
            var employees = _empolyeeRepository.SearchByName(searchedvalue);
            return View(employees);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _empolyeeRepository.Add(employee);
                TempData["SuccessMessageOfEmpolyee"] = "Employee created successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var empolyee = _empolyeeRepository.GetById(id.Value);
            if (empolyee is null)
                return NotFound();

            return View(ViewName, empolyee);
        }
        public IActionResult Edit(int? id)
        {
            return Details(id.Value, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _empolyeeRepository.Ubdate(employee);
                   return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }
        public IActionResult Delete(int? id)
        {
            return Details(id.Value, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            try
            {
                _empolyeeRepository.Delete(employee);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }
        }

    }
}
