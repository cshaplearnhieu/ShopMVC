using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Helpers;

namespace ShopMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HshopContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Hshop"));
            });

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
			////https://docs.automapper.org/en/stable/Dependency-injection.html
			//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

			//cau hinh automapper
			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			builder.Services.AddSingleton(mapper);


			builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option
                =>
            {
                option.LoginPath = "/KhachHang/DangNhap";
                option.AccessDeniedPath = "/AccessDenied";
            } );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
