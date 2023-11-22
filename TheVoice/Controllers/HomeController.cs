using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheVoice.Data;
using TheVoice.Models;
using TheVoice.Models.ViewModels;
using TheVoice.Providers.Builders;
using TheVoice.Providers.DB;

namespace TheVoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (_context == null)
            {

            }

            //if mentor
            {
                var vm = await GetMentorHomeBag();
                if (vm != null)
                { return View("MentorsHomeIndex", vm); }
                else
                {/*throw him out*/ }
            }

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private async Task<MentorHomeIndexVM?> GetMentorHomeBag()
        {
            Mentor? mentor = await DBDataProvider.GetMentorFullDataForUser(_context, _userManager, HttpContext);
            if (mentor != null)
            { return ClassBuilder.ConvertToMentorHomeIndexVM(mentor); }
            else
            { return null; }
        }
    }
}