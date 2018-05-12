namespace ConsoleApp1
{
    using ConsoleApp1.Models;
    using System.Data.Entity;
    using System.Linq;
    public class Model1 : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public Model1()
            : base("name=Model1")
        {
            Database.SetInitializer(new MyDbInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderService> OrderServices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //��� ��� ����� ��������� - ����� ����� ��������� � ������� ��������� ����� ��������� using System.ComponentModel.DataAnnotations;
            //�� ��� ��� �����.
            modelBuilder.Entity<Order>().HasKey(x => x.Id);//������� ����
            modelBuilder.Entity<Service>().HasKey(x => x.Id);//�������� ����
            modelBuilder.Entity<OrderService>().HasKey(x => new { x.OrderId, x.ServiceId });//��������������� ����

            //����������� ����� ����� ��������� � �������� ����
            modelBuilder.Entity<Order>().HasMany(x => x.OrderServices) // ����� �����  ����� �����
                .WithRequired(x => x.Order) //��� ������ ������ ������������ �����
                .HasForeignKey(x => x.OrderId)//����� ������� ����
                                               .WillCascadeOnDelete();//��������� ��������. 

            modelBuilder.Entity<Service>().HasMany(x => x.OrderServices)
                .WithRequired(x => x.Service)
                .HasForeignKey(x => x.ServiceId)
                .WillCascadeOnDelete();//��������� ��������. 


            base.OnModelCreating(modelBuilder);
        }
    }

}