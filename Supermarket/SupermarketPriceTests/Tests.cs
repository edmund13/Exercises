using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupermarketPriceTests
{
    [TestClass]
    public class Tests
    {
        private Supermarket superMarket;

        [TestMethod]
        public void AddNewStock()
        {
            //Arrange
            superMarket = new Supermarket();
            
            //Act
            StockItem item = GetStockItem();

            superMarket.AddStockItem(item);

            //Assert
           Assert.AreEqual(superMarket.Stock.Count, 1, "Only one item added");

        }

        [TestMethod]
        public void BuyItem()
        {
            //Arrange
            const string name = "Item";
            const int numberOfItemsBought = 3;
            Supermarket supermarket = new Supermarket();
            supermarket.AddStockItem(GetStockItem());

            //Act
            int checkedOutItems = supermarket.SellItem(name, numberOfItemsBought);

            int remainingItems = supermarket.GetRemainingItems(name);

            //Assert
            Assert.AreEqual(checkedOutItems, 4);
            Assert.AreEqual(remainingItems, 6);
        }

        [TestMethod]
        public void GetStockValue()
        {
            //Arrange
            const string name = "Item";
            Supermarket supermarket = new Supermarket();
            supermarket.AddStockItem(GetStockItem());

            //Act
            double minStockValue = supermarket.GetMinStockValue(name);
            double maxStockValue = supermarket.GetMaxStockValue(name);

            //Assert
            Assert.AreEqual(minStockValue, 7.00); //Assuming all itwms get bought with Buy 2 get 1 Free
            Assert.AreEqual(maxStockValue, 10.00); //Assuming all items are bought individually
        }

        private StockItem GetStockItem()
        {
            const string name = "Item";
            const double price = 1.00;
            const int numberOfItems = 10;
            PromotionRule rule = new PromotionRule
            {
                buy = 2, // buy 2 get 1 free
                get = 1
            };

            return new StockItem
            {
                Name = name,
                Price = price,
                NumberOfItems = numberOfItems,
                Rule = rule
            };
        }
    }
}
