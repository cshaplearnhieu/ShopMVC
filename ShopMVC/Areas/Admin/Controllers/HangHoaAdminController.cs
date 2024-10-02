using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;

namespace ShopMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/hanghoa")]
    public class HangHoaController : Controller
    {
        private readonly HshopContext db;

        public HangHoaController(HshopContext context)
        {
            db = context;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        [Route("them")]
        public IActionResult Them()
        {
            ViewBag.LoaiList = db.Loais.ToList();
            return View();
        }

        [HttpPost]
        [Route("them")]
        public IActionResult Them(HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(hangHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiList = db.Loais.ToList();
            return View(hangHoa);
        }

        [HttpGet]
        [Route("sua/{id}")]
        public IActionResult Sua(int id)
        {
            var hangHoa = db.HangHoas.Find(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewBag.LoaiList = db.Loais.ToList();
            return View(hangHoa);
        }

        [HttpPost]
        [Route("sua/{id}")]
        public IActionResult Sua(HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Update(hangHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiList = db.Loais.ToList();
            return View(hangHoa);
        }

        [Route("xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            var hangHoa = db.HangHoas.Find(id);
            if (hangHoa != null)
            {
                db.HangHoas.Remove(hangHoa);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
