using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.Orders
{
    public class OrderRepository
    {
        private List<IOrder> orders;
        public OrderRepository()
        {
            orders = new List<IOrder>();
        }

        public void Add(IOrder newOrder)
        {
            orders.Add(newOrder);
        }

        public void Remove(IOrder removedOrder)
        {
            orders = orders.Where(o => !string.Equals(removedOrder.OrderNumber, o.OrderNumber)).ToList();
        }

        public List<IOrder> Get()
        {
            return orders;
        }
    }

    /*
    Mock refactor of OrderRepository:
    
        - I wasn't able to create a functional database for the repositories, but I wanted to create a mock
          refactor to show how I would implement an API for adding/removing/getting data.

        - Given more time and experience using the .NET framework, I would have liked to implement a database
          and interact with it using an ORM like the Entity Framework.

        - The mock refactor below is a simple example of how I would implement an API for interacting with the
          database using something like the Entity Framework.

    -------------------------------------------------------------------------------------------------------------
    // AppDbContext.cs - Create a DbContext for interacting with the database
    public class AppDbContext : DbContext
    {
        public DbSet<IOrder> Orders { get; set; }    // Create a DbSet for orders, in place of "private List<IOrder> orders; orders = new List<IOrder>();"

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }

    -------------------------------------------------------------------------------------------------------------
    // OrderRepository.cs - Refactor the OrderRepository to interact with the database
    public class OrderRepository
    {
        private readonly AppDbContext _context;    // Add a private field for the AppDbContext

        public OrderRepository(AppDbContext context)
        {
            _context = context;    // Bring in the AppDbContext to interact with the database
        }

        public void Add(IOrder newOrder)    // Add a new order to the database
        {
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }

        public void Remove(IOrder removedOrder)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == removedOrder.OrderNumber);    // First find the order in the database
            if (order != null)    // If the order exists, remove it from the database
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public List<IOrder> Get()   // Get all orders from the database
        {
            return _context.Orders.ToList();
        }
    }
    */
}
