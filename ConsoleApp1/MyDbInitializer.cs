namespace ConsoleApp1
{
    using ConsoleApp1.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<Model1>
    {
        protected override async void Seed(Model1 context)
        {
            if (context.Orders.Any())
            {
                return; //Данные уже есть 
            }
            var service1 = new Service() { Id = 1, Name = "Услуга1" };
            var service2 = new Service() { Id = 2, Name = "Услуга2" };
            var service3 = new Service() { Id = 3, Name = "Услуга3" };
            var service4 = new Service() { Id = 4, Name = "Услуга4" };
            context.Services.Add(service1);
            context.Services.Add(service2);
            context.Services.Add(service3);
            context.Services.Add(service4);
            context.SaveChanges();

            var order1 = new Order() { Id = 1, OrderDate = DateTime.Now, Status = "AddedService123" };
            var order2 = new Order() { Id = 2, OrderDate = DateTime.Now, Status = "AddedSevice23" };
            var order3 = new Order() { Id = 3, OrderDate = DateTime.Now, Status = "AddedService4" };
            var order4 = new Order() { Id = 4, OrderDate = DateTime.Now, Status = "AddedWithoutService" };

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);
            context.Orders.Add(order4);

            order1.OrderServices.Add(new OrderService() { Service = service1, Quantity = 1 });
            order1.OrderServices.Add(new OrderService() { Service = service2, Quantity = 2 });
            order1.OrderServices.Add(new OrderService() { Service = service3, Quantity = 3 });

            order2.OrderServices.Add(new OrderService() { Service = service2, Quantity = 21 });
            order2.OrderServices.Add(new OrderService() { Service = service3, Quantity = 23 });

            order3.OrderServices.Add(new OrderService() { Service = service4, Quantity = 24 });

            context.SaveChanges();

            base.Seed(context);
        }
    }

}