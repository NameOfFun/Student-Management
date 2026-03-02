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

        //Edit a Student
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if(student == null)
            {
                return NotFound();
            }

            ViewBag.Department = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

                if (existingStudent == null)
                    return NotFound();

                // update fields manually
                existingStudent.FullName = student.FullName;
                existingStudent.DepartmentId = student.DepartmentId;
                existingStudent.Email = student.Email;
                existingStudent.Phone = student.Phone;
                existingStudent.Address = student.Address;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Gender = student.Gender;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Department = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName,
                Selected = d.DepartmentId == student.DepartmentId
            }).ToList();
            return View(student);
        }
    }
}
