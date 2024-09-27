using Microsoft.AspNetCore.Mvc;
using ShopMVC.Data;
using ShopMVC.ViewModels;
using ShopMVC.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace ShopMVC.Controllers
{
    public class CartController : Controller
    {
		private readonly HshopContext db;

		public CartController(HshopContext context) 
        { 
        db = context;
        }
        
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }  public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
				if (hangHoa == null)
				{
                    TempData["message"] = $"không tìm thấy sản phẩm có mã {id}";
                    return Redirect("/404");
				}
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHh = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
			}
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
              
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if(item != null)
            {
                gioHang.Remove(item);
				HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}
        [Authorize]
        public IActionResult Checkout()
        {
            if(Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }
    }
}
