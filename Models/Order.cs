namespace Bakery.Models
{
    public class Order{

        public int Id { get; set; }
        public string OrderEmail { get; set; }
        public string OrderShipping { get; set; }
        public int OrderQuantity { get; set; }    
    }
}