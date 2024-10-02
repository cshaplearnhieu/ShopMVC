using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ShopMVC.Data;
using ShopMVC.Helpers;
using ShopMVC.ViewModels;

namespace ShopMVC.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly HshopContext db;
		private readonly IMapper _mapper;

		public KhachHangController(HshopContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}
		#region Register
		[HttpGet]
		public IActionResult DangKy()
		{

			return View();
		}
		[HttpPost]
		public IActionResult DangKy(RegisterVM model/*, IFormFile Hinh*/)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//var khachHang = new KhachHang
					//{
					//	MaKh = model.MaKh,
					//	HoTen = model.HoTen,
					//	GioiTinh = model.GioiTinh,
					//	NgaySinh = model.NgaySinh,
					//	DiaChi = model.DiaChi,
					//	DienThoai = model.DienThoai,
					//	Email = model.Email,
					//	// Các thuộc tính không có trong RegisterVM
					//	RandomKey = MyUtil.GenerateRandomKey(),
					//	MatKhau = model.MatKhau.ToMd5Hash(MyUtil.GenerateRandomKey()),
					//	HieuLuc = true,
					//	VaiTro = 0
					//};
					var khachHang = _mapper.Map<KhachHang>(model);
					khachHang.RandomKey = MyUtil.GenerateRandomKey();
					khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
					khachHang.HieuLuc = true; // xử lý khi có email
					khachHang.VaiTro = 0;

					//if (Hinh != null)
					//{
					//	khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
					//}

					db.Add(khachHang);
					db.SaveChanges();
                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                    
                    return RedirectToAction("Index", "Home");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}");
				}
                
            }
			return View();
		}

		#endregion


		#region Login in
		[HttpGet]
		public IActionResult DangNhap(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();

		}
		[HttpPost]
		public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var KhachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
				if (KhachHang == null)
				{
					ModelState.AddModelError("Loi", "Sai tên đăng nhập hoặc mật khẩu");
				}
				else
				{
					if (!KhachHang.HieuLuc)
					{
						ModelState.AddModelError("Loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");
					}
					else
					{
						if (KhachHang.MatKhau != model.Password.ToMd5Hash(KhachHang.RandomKey))
						{
							ModelState.AddModelError("Loi", "Sai tên đăng nhập hoặc mật khẩu");
						}
						else
						{
							var claims = new List<Claim> {
							new Claim(ClaimTypes.Email, KhachHang.Email),
							new Claim(ClaimTypes.Name, KhachHang.HoTen),
							new Claim(MySetting.CLAIM_CUSTOMERID, KhachHang.MaKh),

                            //claim - role động 
                            new Claim(ClaimTypes.Role, "Customer")
							};

							var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
							var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

							await HttpContext.SignInAsync(claimsPricipal);

							if (Url.IsLocalUrl(ReturnUrl))
							{
								return Redirect(ReturnUrl);
							}
							else
							{
								return Redirect("/");
							}
						}
					}
				}
			}
			return View();
		}
		#endregion
		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}
}
