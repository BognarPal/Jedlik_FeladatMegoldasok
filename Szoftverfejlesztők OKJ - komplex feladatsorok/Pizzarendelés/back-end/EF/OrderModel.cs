using System.ComponentModel.DataAnnotations;

namespace back_end.EF
{
    public class OrderModel
    {
        [Required]
        public int? Id { get; set; }
        public PizzaModel Pizza { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }

        public int PizzaId { get; set; }
    }
}
