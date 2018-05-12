using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class OrderService
    {

        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Service  Service { get; set; }

    }
}
