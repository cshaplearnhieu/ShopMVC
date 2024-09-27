using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.ViewComponents
{
	public class DanhMucViewComponent : ViewComponent
	{
		private readonly HshopContext db;

		public DanhMucViewComponent(HshopContext context) => db = context;

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var loais = await db.Loais.ToListAsync();
			var viewModel = loais.Select(l => new MenuLoaiVM
			{
				MaLoai = l.MaLoai,
				TenLoai = l.TenLoai
			}).ToList();

			return View(viewModel); 
		}
	}
}
