using Microsoft.AspNetCore.Mvc;
using ShopMVC.Data;

namespace ShopMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        
        HshopContext db = new HshopContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
