using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }

        public abstract string GetName();
        
        public void CreateOrder(IOrder order)
        {
            _orderRepository.Add(order);
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }

        public float GetTotalSales()
        {
            List<IOrder> Orders = _orderRepository.Get();
            float totalSales = 0;

            foreach (IOrder order in Orders)
            {
                foreach (OrderedProduct product in order.Products)
                {
                    totalSales += product.Product.GetSellingPrice();
                }
            }

            return totalSales;
        }

        public float GetTotalReturns()
        {
            List<IReturn> Returns = _returnRepository.Get();
            float totalReturns = 0;

            foreach (IReturn rga in Returns)
            {
                foreach (ReturnedProduct product in rga.ReturnedProducts)
                {
                    totalReturns += product.OrderProduct.Product.GetSellingPrice();
                }
            }

            return totalReturns;
        }

        public float GetTotalProfit()
        {
            return GetTotalSales() - GetTotalReturns();
        }

        public DateTime GetOrderDate()
        {
            List<IOrder> orders = _orderRepository.Get();
            return orders[0].OrderDate;
        }
    }
}
