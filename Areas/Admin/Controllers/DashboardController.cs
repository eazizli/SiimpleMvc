using Microsoft.AspNetCore.Mvc;

namespace SiimpleMvc.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
