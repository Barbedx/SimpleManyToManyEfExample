using System;
using System.Collections.Generic;
namespace ConsoleApp1.Models
{
    public class Order
    {
        public Order()
        {
            OrderServices = new HashSet<OrderService>();
        }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public virtual ICollection<OrderService> OrderServices { get; set; }
        public override string ToString()
        {
            return $"Id = {Id} , Ordate ={OrderDate.ToShortDateString()}, Status = {Status} ";
        }

    }
}
