using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.Returns
{
    public class ReturnRepository
    {
        private List<IReturn> returns;
        public ReturnRepository()
        {
            returns = new List<IReturn>();
        }

        public void Add(IReturn newReturn)
        {
            returns.Add(newReturn);
        }

        public void Remove(IReturn removedReturn)
        {
            returns = returns.Where(o => !string.Equals(removedReturn.ReturnNumber, o.ReturnNumber)).ToList();
        }

        public List<IReturn> Get()
        {
            return returns;
        }
    }

    /*
    Mock refactor of ReturnRepository:
    
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
        public DbSet<IReturn> Returns { get; set; }    // Create a DbSet for returns, in place of "private List<IReturn> returns; returns = new List<IReturn>();"

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }

    -------------------------------------------------------------------------------------------------------------
    // ReturnRepository.cs - Refactor the ReturnRepository to interact with the database
    public class ReturnRepository
    {
        private readonly AppDbContext _context;    // Add a private field for the AppDbContext

        public ReturnRepository(AppDbContext context)
        {
            _context = context;    // Bring in the AppDbContext to interact with the database
        }

        public void Add(IReturn newReturn)    // Add a new return to the database
        {
            _context.Returns.Add(newReturn);
            _context.SaveChanges();
        }

        public void Remove(IReturn removedReturn)
        {
            var return = _context.Returns.FirstOrDefault(o => o.ReturnNumber == removedReturn.ReturnNumber);    // First find the return in the database
            if (return != null)    // If the return exists, remove it from the database
            {
                _context.Returns.Remove(return);
                _context.SaveChanges();
            }
        }

        public List<IReturn> Get()   // Get all returns from the database
        {
            return _context.Returns.ToList();
        }
    }
    */
}
