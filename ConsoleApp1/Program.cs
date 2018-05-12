using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ConsoleApp1.Models;
using System;
using System.Data.Entity;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Введите номер заказа для просмотра, 0 для всех заказов, или Q для выхода");
                var key = Console.ReadLine();
                if (key.ToUpper() == "Q") return;

                if (key.ToUpper() == "0")
                {
                    ShowAll();
                    continue;
                }

                if (!Int32.TryParse(key, out int id))
                {
                    Console.WriteLine("Ошибка ввода");
                    continue;
                }

                if (ShowOrder(id, out Order CurentOrder))// Если заказ найден
                {
                    Console.WriteLine("Редактировать заказ? Y:N");
                    if (Console.ReadLine().ToUpper() == "Y")
                    {
                        EditOrder(id, CurentOrder);
                    }
                }
                Console.WriteLine();
                //Console.Clear();
            }
        }

        private static void ShowAll()
        {
            using (var db = new Model1())
            {

                foreach (var order in db.Orders)
                {
                    ShowOrder(order);
                }
            }
        }

        private static void EditOrder(int id, Order order)
        {
            while (true)
            {
                Console.WriteLine("Чтобы изменить Статус = 1 ; Услуги = 2; остальное - отмена ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Введите новый статус:");
                        order.Status = Console.ReadLine();
                        using (var db = new Model1())
                        {
                            db.Entry(order).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        break;

                    case "2":
                        bool exitflag = false;
                        while (!exitflag)
                        {
                            Console.WriteLine("Добавить -1 , изменить количество - 2? ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    AddNewServiceToOrder(order);
                                    break;
                                case "2":
                                    EditServiceQuantity(order);
                                    break;
                                default:
                                    exitflag = true;
                                    break;
                            }
                        }
                        break;

                    default:
                        return;
                }
                ShowOrder(order);

            }
        }

        private static void AddNewServiceToOrder(Order order)
        {
            using (var db = new Model1())
            {

                foreach (var item in db.Services)
                {
                    Console.WriteLine($"Id = {item.Id}, Имя = {item.Name}");
                }
                Console.WriteLine($"Введите код статьи:");
                if (Int32.TryParse(Console.ReadLine(), out int res))
                {
                    if (order.OrderServices.Any(x => x.ServiceId == res))
                    {
                        Console.WriteLine("в данном заказе уже есть такая услуга!"); ;
                        return;
                    }
                    var service = db.Services.FirstOrDefault(x => x.Id == res);

                    if (service == null)
                    {
                        Console.WriteLine("услуга не найдена!");
                        return;
                    }

                    Console.WriteLine("Введите количество:");
                    if (Int32.TryParse(Console.ReadLine(), out int quant))
                    {
                        var orservice = new OrderService()
                        {
                            Order = order,
                            Service = service,
                            Quantity = quant
                        };
                        try
                        {
                            db.OrderServices.Add(orservice);
                            //db.Entry(order).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Cannot save");
                        }
                    }



                }
                else Console.WriteLine("Ошибка ввода");
            }
        }

        private static void EditServiceQuantity(Order order)
        {
            foreach (var item in order.OrderServices)
            {
                Console.WriteLine($" Код:{item.ServiceId} Услуга: {item.Service.Name}  в количестве: {item.Quantity}");

            }
            Console.WriteLine("Введите номер услуги которую хотите изменить: (вернутся- Q)");
            if (Int32.TryParse(Console.ReadLine(), out int serviceid))
            {
                var orserv = order.OrderServices.FirstOrDefault(x => x.ServiceId == serviceid);
                if (orserv == null)
                {
                    Console.WriteLine("услуга не найдена");
                }
                else
                {
                    Console.WriteLine("Введите количество");
                    if (Int32.TryParse(Console.ReadLine(), out int quantity))
                    {
                        orserv.Quantity = quantity;
                        using (var db = new Model1())
                        {
                            db.Entry(orserv).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                }
            }
        }

        private static bool ShowOrder(int id, out Order order)
        {
            using (var db = new Model1())
            {
                order = db.Orders.FirstOrDefault(x => x.Id == id);
                return ShowOrder(order);
            }
        }

        private static bool ShowOrder(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("Заказ не найден");
                return false;
            }
            else
            {
                Console.WriteLine(order);
                foreach (var item in order.OrderServices)
                {
                    Console.WriteLine($"Услуга: {item.Service.Name}  в количестве: {item.Quantity}");

                }
                return true;
            }
        }
    }
}
