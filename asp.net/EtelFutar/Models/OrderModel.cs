namespace EtelFutar.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<OrderDetailModel> Orders { get; set; } = new List<OrderDetailModel>();
        public string? State { get; set; }
    }
}
