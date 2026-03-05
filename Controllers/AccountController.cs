using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private StudentManagementContext _context;
        public AccountController(StudentManagementContext context)
        {
            _context = context;
        }

        // Show Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

    }
}
