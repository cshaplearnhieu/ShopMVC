using AutoMapper;
using ShopMVC.Data;
using ShopMVC.ViewModels;

namespace ShopMVC.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterVM, KhachHang>();
            //.ForMenber(kh => kh.HoTen, option => option.MapFropm(RegisterVM => RegisterVM.HoTen))
            //    .ReverseMap();
        }
    }
}