using Microsoft.AspNetCore.Mvc;
using SiimpleMvc.DataContex;
using SiimpleMvc.Models;

namespace SiimpleMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiimpleDbContext _context;

        public HomeController(SiimpleDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Card> cards = _context.Cards.ToList();
            return View(cards);
        }
    }
}
