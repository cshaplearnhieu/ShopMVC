using System.ComponentModel.DataAnnotations;

namespace ShopMVC.ViewModels
{
	public class RegisterVM
	{
		[Display(Name ="Tên đăng nhập")]
		[Required(ErrorMessage ="Vui lòng nhập tên đăng nhập")]
		[MaxLength(20, ErrorMessage ="Tối đa 20 ký tự")]
		public string MaKh { get; set; }


		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		[DataType(DataType.Password)]
		public string MatKhau { get; set; }


		[Display(Name = "Họ tên")]
		[Required(ErrorMessage = "Vui lòng nhập họ tên")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
		public string HoTen { get; set; }


		[Display(Name = "Giới tính")]
		[Required(ErrorMessage = "Vui lòng chọn giới tính")]
		public bool GioiTinh { get; set; } = true;


		[Display(Name = "Ngày sinh")]
		[DataType(DataType.Date)]
		public DateTime? NgaySinh { get; set; }


		
		[Display(Name = "Địa chỉ")]
		[Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
		[MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
		public string DiaChi { get; set; }


		[Display(Name = "Điện thoại")]
		[Required(ErrorMessage = "Vui lòng nhập điện thoại")]
		[MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
		[RegularExpression(@"0[98753]\d{8}", ErrorMessage ="Số điện thoại không hợp lệ")]
		public string DienThoai { get; set; }


		[Display(Name = "Email")]
		[Required(ErrorMessage = "Vui lòng nhập email")]
		[EmailAddress(ErrorMessage ="Chưa đúng định dạng email")]
		public string Email { get; set; }


		[Display(Name = "Hình")]
		
		public string? Hinh { get; set; }
	}
}
