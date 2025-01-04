namespace EtelFutar.Models
{
    public class OrderDetailModel
    {
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}