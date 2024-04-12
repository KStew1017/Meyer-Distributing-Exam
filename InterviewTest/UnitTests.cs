using InterviewTest.Orders;
using InterviewTest.Returns;
using InterviewTest.Customers;
using InterviewTest.Products;

/*
    Im not sure how much test coverage is necessary, but I will test the following:

    1. Add Methods for both Order and Return
       - Test that they are added to the order/return
       - Test that the product/return number and selling price are correct
       - Test that the product/return is the correct type
    2. Create Methods for both Order and Return
       - Test that orders/returns are added to the customer repo
       - Test that the order/return number is correct
       - Test that the products/returns are correct
       - Test that the products/returns are the correct type
*/

public class UnitTests
{
    private static ICustomer GetDummyCustomer()
    {
        var orderRepository = new OrderRepository();
        var returnRepository = new ReturnRepository();
        return new CarDealershipCustomer(orderRepository, returnRepository);
    }

    [Fact]
    public void Order_AddProduct_ProductAddedToOrderRepo()
    {
        // Arrange
        var customer = GetDummyCustomer();
        Order order = new Order("TestOrder", customer);

        // Act
        order.AddProduct(new ReplacementBumper());
        order.AddProduct(new SyntheticOil());

        // Assert
        Assert.Equal(2, order.Products.Count);
        Assert.Equal("Sherman 036-87-1", order.Products[0].Product.GetProductNumber());
        Assert.Equal("Mobil 1 5W-30", order.Products[1].Product.GetProductNumber());
        Assert.Equal(155, order.Products[0].Product.GetSellingPrice());
        Assert.Equal(25, order.Products[1].Product.GetSellingPrice());
        Assert.IsType<SyntheticOil>(order.Products[1].Product);
        Assert.IsType<ReplacementBumper>(order.Products[0].Product);
    }

    [Fact]
    public void CustomerBase_CreateOrder_OrderAddedToCustomer()
    {
        // Arrange
        var customer = GetDummyCustomer();
        Order order = new Order("TestOrder", customer);

        // Act
        order.AddProduct(new ReplacementBumper());
        order.AddProduct(new SyntheticOil());
        customer.CreateOrder(order);

        // Assert
        Assert.Contains("TestOrder", customer.GetOrders()[0].OrderNumber);
        Assert.Equal(2, customer.GetOrders()[0].Products.Count);
        Assert.Equal("Sherman 036-87-1", customer.GetOrders()[0].Products[0].Product.GetProductNumber());
        Assert.Equal("Mobil 1 5W-30", customer.GetOrders()[0].Products[1].Product.GetProductNumber());
        Assert.Equal(155, customer.GetOrders()[0].Products[0].Product.GetSellingPrice());
        Assert.Equal(25, customer.GetOrders()[0].Products[1].Product.GetSellingPrice());
        Assert.IsType<SyntheticOil>(customer.GetOrders()[0].Products[1].Product);
        Assert.IsType<ReplacementBumper>(customer.GetOrders()[0].Products[0].Product);
    }

    [Fact]
    public void Return_AddProduct_ProductAddedToReturnRepo()
    {
        // Arrange
        var customer = GetDummyCustomer();
        Order order = new Order("TestOrder", customer);
        order.AddProduct(new ReplacementBumper());
        order.AddProduct(new SyntheticOil());
        customer.CreateOrder(order);

        Return returnOrder = new Return("TestReturn", order);

        // Act
        returnOrder.AddProduct(customer.GetOrders()[0].Products[0]);
        returnOrder.AddProduct(customer.GetOrders()[0].Products[1]);

        // Assert
        Assert.Equal(2, returnOrder.ReturnedProducts.Count);
        Assert.Equal("Sherman 036-87-1", returnOrder.ReturnedProducts[0].OrderProduct.Product.GetProductNumber());
        Assert.Equal("Mobil 1 5W-30", returnOrder.ReturnedProducts[1].OrderProduct.Product.GetProductNumber());
        Assert.Equal(155, returnOrder.ReturnedProducts[0].OrderProduct.Product.GetSellingPrice());
        Assert.Equal(25, returnOrder.ReturnedProducts[1].OrderProduct.Product.GetSellingPrice());
        Assert.IsType<SyntheticOil>(returnOrder.ReturnedProducts[1].OrderProduct.Product);
        Assert.IsType<ReplacementBumper>(returnOrder.ReturnedProducts[0].OrderProduct.Product);
    }

    [Fact]
    public void CustomerBase_CreateReturn_ReturnAddedToCustomer()
    {
        // Arrange
        var customer = GetDummyCustomer();
        Order order = new Order("TestOrder", customer);
        order.AddProduct(new ReplacementBumper());
        order.AddProduct(new SyntheticOil());
        customer.CreateOrder(order);

        Return returnOrder = new Return("TestReturn", order);
        returnOrder.AddProduct(customer.GetOrders()[0].Products[0]);
        returnOrder.AddProduct(customer.GetOrders()[0].Products[1]);

        // Act
        customer.CreateReturn(returnOrder);

        // Assert
        Assert.Contains("TestReturn", customer.GetReturns()[0].ReturnNumber);
        Assert.Equal(2, customer.GetReturns()[0].ReturnedProducts.Count);
        Assert.Equal("Sherman 036-87-1", customer.GetReturns()[0].ReturnedProducts[0].OrderProduct.Product.GetProductNumber());
        Assert.Equal("Mobil 1 5W-30", customer.GetReturns()[0].ReturnedProducts[1].OrderProduct.Product.GetProductNumber());
        Assert.Equal(155, customer.GetReturns()[0].ReturnedProducts[0].OrderProduct.Product.GetSellingPrice());
        Assert.Equal(25, customer.GetReturns()[0].ReturnedProducts[1].OrderProduct.Product.GetSellingPrice());
        Assert.IsType<SyntheticOil>(customer.GetReturns()[0].ReturnedProducts[1].OrderProduct.Product);
        Assert.IsType<ReplacementBumper>(customer.GetReturns()[0].ReturnedProducts[0].OrderProduct.Product);
    }
}