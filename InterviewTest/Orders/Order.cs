using System.Collections.Generic;
using InterviewTest.Customers;
using InterviewTest.Products;
using System;

namespace InterviewTest.Orders
{
    public class Order : IOrder
    {
        public Order(string orderNumber, ICustomer customer)
        {
            OrderNumber = orderNumber;
            Customer = customer;
            Products = new List<OrderedProduct>();
            OrderDate = DateTime.Now;
        }

        public string OrderNumber { get; }
        public ICustomer Customer { get; }
        public List<OrderedProduct> Products { get; }
        public DateTime OrderDate { get; }

        public void AddProduct(IProduct product)
        {
            Products.Add(new OrderedProduct(product));
        }
    }
}
