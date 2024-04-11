using System;
using System.Linq;
using InterviewTest.Customers;
using InterviewTest.Orders;
using InterviewTest.Products;
using InterviewTest.Returns;

namespace InterviewTest
{
    public class Program
    {
        // The previous implementation of the OrderRepository and ReturnRepository were static and readonly, which made it impossible to create new instances of them. This was the cause of the bug where Ruxer Ford Lincoln, Inc.'s totals were incorrect. The same repository instances were being used for both of the customers, leading to shared state. I removed the previous fields that were here and created new instances of the repositories in the GetTruckAccessoriesCustomer and GetCarDealershipCustomer methods, which fixed the issue.

        static void Main(string[] args)
        {
            // ------------------------
            // Coding Challenge Requirements
            // ------------------------


            // ------------------------
            // Code Implementations
            // ------------------------
            // 1: Implement get total sales, returns, and profit in the CustomerBase class.
            // 2: Record when an item was purchased.


            // ------------------------
            // Bug fixes
            // ------------------------
            // ~~ Run the console app after implementing the Code Changes section above! ~~
            // 1: Meyer Truck Equipment's returns are not being processed.
            // 2: Ruxer Ford Lincoln, Inc.'s totals are incorrect.
            

            // ------------------------
            // Bonus
            // ------------------------
            // 1: Create unit tests for the ordering and return process.
            // 2: Create a database and refactor all repositories to save/update/pull from it.

            ProcessTruckAccessoriesExample();

            ProcessCarDealershipExample();

            Console.ReadKey();
        }

        private static void ProcessTruckAccessoriesExample()
        {
            var customer = GetTruckAccessoriesCustomer();

            IOrder order = new Order("TruckAccessoriesOrder123", customer);
            order.AddProduct(new HitchAdapter());
            order.AddProduct(new BedLiner());
            customer.CreateOrder(order);

            IReturn rga = new Return("TruckAccessoriesReturn123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);

            ConsoleWriteLineResults(customer);
        }

        private static void ProcessCarDealershipExample()
        {
            var customer = GetCarDealershipCustomer();

            IOrder order = new Order("CarDealerShipOrder123", customer);
            order.AddProduct(new ReplacementBumper());
            order.AddProduct(new SyntheticOil());
            customer.CreateOrder(order);

            IReturn rga = new Return("CarDealerShipReturn123", order);
            rga.AddProduct(order.Products.First());
            customer.CreateReturn(rga);

            ConsoleWriteLineResults(customer);
        }

        private static ICustomer GetTruckAccessoriesCustomer()
        {
            var orderRepo = new OrderRepository();
            var returnRepo = new ReturnRepository();
            return new TruckAccessoriesCustomer(orderRepo, returnRepo);
        }

        private static ICustomer GetCarDealershipCustomer()
        {
            var orderRepo = new OrderRepository();
            var returnRepo = new ReturnRepository();
            return new CarDealershipCustomer(orderRepo, returnRepo);
        }

        // I decided to have some fun with formatting the console output

        private static void ConsoleWriteLineResults(ICustomer customer)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"***** {customer.GetName()} *****");
            Console.ResetColor();

            Console.Write("Total Sales: "); Console.ForegroundColor = ConsoleColor.Green;  Console.Write($"{customer.GetTotalSales().ToString("c")}\n"); Console.ResetColor();

            Console.Write("Total Returns: "); Console.ForegroundColor = ConsoleColor.DarkRed;  Console.Write($"{customer.GetTotalReturns().ToString("c")}\n"); Console.ResetColor();

            Console.Write("Total Profit: "); 
            Console.ForegroundColor = customer.GetTotalProfit() >= 0 ? ConsoleColor.Green : ConsoleColor.DarkRed;
            Console.Write($"{customer.GetTotalProfit().ToString("c")}\n");
            Console.ResetColor();
            
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Orders:");
            Console.ResetColor();
            int i = 1;
            foreach (var order in customer.GetOrders())
            {
                Console.WriteLine($"    Order {i++}:");
                Console.Write($"        Order Number:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" {order.OrderNumber}\n");
                Console.ResetColor();
                Console.Write($"        Order Date:"); 
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" {order.OrderDate}\n");
                Console.ResetColor();
                Console.WriteLine("        Products:");
                foreach (var product in order.Products)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"            {product.Product.GetProductNumber()}");
                    Console.ResetColor();
                    Console.Write(" -");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" {product.Product.GetSellingPrice().ToString("c")}\n");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Returns:");
            Console.ResetColor();
            int j = 1;
            foreach (var rga in customer.GetReturns())
            {
                Console.WriteLine($"    Return {j++}:");
                Console.Write($"        Return Number:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" {rga.ReturnNumber}\n");
                Console.ResetColor();
                Console.WriteLine("        Returned Products:");
                foreach (var product in rga.ReturnedProducts)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"            {product.OrderProduct.Product.GetProductNumber()}");
                    Console.ResetColor();
                    Console.Write(" -");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($" {product.OrderProduct.Product.GetSellingPrice().ToString("c")}\n");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
