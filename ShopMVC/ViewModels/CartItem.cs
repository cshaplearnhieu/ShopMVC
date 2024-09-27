namespace ShopMVC.ViewModels
{
    public class CartItem
    {
        public int MaHh { get; set; }
        public String Hinh { get; set; }
        public String TenHh { get; set; }
        public Double DonGia { get; set; }
        public int SoLuong { get; set; }
        public Double ThanhTien => SoLuong * DonGia;

    }
}
