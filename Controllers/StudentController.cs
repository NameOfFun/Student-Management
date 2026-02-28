using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create()
        {
            ViewBag.Department = await _context.Departments.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Department = await _context.Departments.ToListAsync();
            return View();
        }
    }
}
