using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private StudentManagementContext _context;
        public StudentController(StudentManagementContext context)
        {
            _context = context;
        }

        // Show all Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.Include(d => d.Department).ToListAsync();

            return View(students);
        }

        // Create a new Student
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Department = _context.Departments.Select(d => new SelectListItem
                                        {
                                            Value = d.DepartmentId.ToString(),
                                            Text = d.DepartmentName 
                                        }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Department = _context.Departments.Select(d => new SelectListItem
                                        {
                                            Value = d.DepartmentId.ToString(),
                                            Text = d.DepartmentName
                                        }).ToList();
            return View(student);
        }
    }
}
