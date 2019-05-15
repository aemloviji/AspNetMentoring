using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Module5.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdministratorController(UserManager<IdentityUser> manager)
        {
            _userManager = manager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}