# Meyer-Distributing-Exam

- Using .NET 8.0

---

## Challenge Requirements

### Code Implementations
1. Implementations of getting total sales, returns, and profits can be found at __"CustomerBase.cs(41-83)"__.
2. Recording of when an order was placed can be found at "Order.cs(15)", with an **OrderDate** property filled via the constructor when the Order is instantiated.

### Bug fixes
1. Fixed bug where Meyer Truck Equipment's returns were not being processed by adding __"customer.CreateReturn(rga);"__ to the **ProcessTruckAccessoriesExample()** method at __"Program.cs(67)"__.
2. Fixed bug where Ruxer Ford Lincoln, Inc.'s total were incorrect by removing the **OrderRepository()** and **ReturnRepository()** instantiations from the **Program** class at __"Program.cs(12-13)"__, and instead instantiating them in the **GetTruckAccessoriesCustomer()** method at __"Program.cs(90-91)"__ and **GetCarDealershipCustomer()** method at __"Program.cs(97-98)"__. This resolved the issue of shared state between customers.

### Bonus
1. Added unit tests for ordering and return processes in __"UnitTests.cs"__.
2. Created mock refactors of the **OrderRepository** and **ReturnRepository** classes in __"OrderRepository.cs(30)"__ and __"ReturnRepository.cs(30)"__, respectively, to simulate interactions with a db.