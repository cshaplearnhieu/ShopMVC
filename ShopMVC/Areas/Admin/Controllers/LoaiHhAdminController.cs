using Microsoft.AspNetCore.Mvc;
using ShopMVC.Data;
using ShopMVC.Models;

namespace ShopMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/loai")]
    public class LoaiController : Controller
    {
        private readonly HshopContext db;

        public LoaiController(HshopContext context)
        {
            db = context;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var loaiList = db.Loais.ToList();
            return View(loaiList);
        }

        [HttpGet]
        [Route("them")]
        public IActionResult Them()
        {
            return View();
        }

        [HttpPost]
        [Route("them")]
        public IActionResult Them(Loai loai)
        {
            if (ModelState.IsValid)
            {
                db.Loais.Add(loai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        [HttpGet]
        [Route("sua/{id}")]
        public IActionResult Sua(int id)
        {
            var loai = db.Loais.Find(id);
            if (loai == null)
            {
                return NotFound();
            }
            return View(loai);
        }

        [HttpPost]
        [Route("sua/{id}")]
        public IActionResult Sua(Loai loai)
        {
            if (ModelState.IsValid)
            {
                db.Loais.Update(loai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        [Route("xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            var loai = db.Loais.Find(id);
            if (loai != null)
            {
                db.Loais.Remove(loai);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
