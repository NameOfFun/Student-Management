using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private StudentManagementContext _context;
        public StudentController(StudentManagementContext context)
        {
            _context = context;
        }

        // Show all Students
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int pageSize = 1;

            var query = _context.Students.Include(d => d.Department).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.FullName.Contains(search) || s.Email.Contains(search) || s.Phone.Contains(search) || s.Address.Contains(search));
            }

            int totalItem = await query.CountAsync();

            var students = await query.OrderBy(s => s.StudentId).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalItems = (int)Math.Ceiling((double)totalItem / pageSize);
            ViewBag.Search = search;

            return View(students);
        }

        // Create a new Student
        [HttpGet]
        [Authorize(Roles = "Admin, Lecturer")]
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
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
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

        // Detail a Student
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students.Include(s => s.Department).FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        // Delete a Student
        [HttpGet]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
