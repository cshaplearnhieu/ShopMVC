using Microsoft.AspNetCore.Mvc;
using ShopMVC.Data;
using ShopMVC.ViewModels;

namespace ShopMVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly HshopContext db;

        public MenuLoaiViewComponent(HshopContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai =  lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai); 
            return View(data);
        }
    }
}
