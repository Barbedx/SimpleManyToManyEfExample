using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Service
    {
        public Service()
        {
            OrderServices = new HashSet<OrderService>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OrderService> OrderServices{ get; set; }

    }
}
