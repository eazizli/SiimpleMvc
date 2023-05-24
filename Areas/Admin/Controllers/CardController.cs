using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using SiimpleMvc.DataContex;
using SiimpleMvc.Models;

namespace SiimpleMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly SiimpleDbContext _siimpleDb;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CardController(SiimpleDbContext siimpleDb,IWebHostEnvironment webHostEnvironment)
        {
            _siimpleDb = siimpleDb;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Card> cards=_siimpleDb.Cards.ToList();
            return View(cards);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Card card)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string guid =Guid.NewGuid().ToString();
            string newfile = guid + card.Images.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","img",newfile);
            using(FileStream filestrem=new FileStream(path,FileMode.CreateNew))
            {
                await card.Images.CopyToAsync(filestrem);
            }
            Card newcard = new Card()
            {
                ImageName = newfile,
                Name = card.Name,
                IconName = card.IconName,
                Title = card.Title,
            };
            _siimpleDb.Cards.Add(newcard);
            _siimpleDb.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            Card card = await _siimpleDb.Cards.FindAsync(id);
            if(card == null)
            {
                return NotFound();
            }
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _siimpleDb.Cards.Remove(card);
            _siimpleDb.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Card card=await _siimpleDb.Cards.FindAsync(id);
            return View(card);
        }
        public async Task<IActionResult> Update(int id)
        {
            Card card = await _siimpleDb.Cards.FindAsync(id);
            
            return(View(card));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Card card)
        {
            
            Card newcard=await _siimpleDb.Cards.FindAsync(id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            string guid = Guid.NewGuid().ToString();
            string newfile = guid + card.Images.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", newfile);
            using(FileStream filestrem =new FileStream(path, FileMode.Create))
            {
                await card.Images.CopyToAsync(filestrem);
            }
            newcard.ImageName = newfile;
            newcard.Name=card.Name;
            newcard.IconName=card.IconName;
            newcard.Title=card.Title;
            _siimpleDb.Cards.Update(newcard);
            _siimpleDb.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
