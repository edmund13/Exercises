using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketPriceTests
{
    class Supermarket
    {

        public List<StockItem> Stock { get; set; }

        public Supermarket()
        {
            Stock = new List<StockItem>();
        }
 
        internal void AddStockItem(StockItem stockItem)
        {
            Stock.Add(stockItem);
        }

        internal int SellItem(string name, int numberOfItems)
        {

            StockItem items = Stock.Single(x => x.Name == name);
            
            int freeItems = ( numberOfItems / items.Rule.buy ) * items.Rule.get;

            int checkedOutItems = numberOfItems + freeItems;

            items.NumberOfItems -= checkedOutItems;

            return checkedOutItems;
        }

        internal int GetRemainingItems(string name)
        {
            return Stock.Single(x => x.Name == name).NumberOfItems;
        }

        internal double GetMinStockValue(string name)
        {
            StockItem items = Stock.Single(x => x.Name == name);

            int itemPortions = (items.NumberOfItems / (items.Rule.buy + items.Rule.get));

            int remainingSingularItems = items.NumberOfItems % (items.Rule.buy + items.Rule.get);

            int totalUnitsCostable = remainingSingularItems + (itemPortions * (items.Rule.buy));

            return totalUnitsCostable * items.Price;
        }

        internal double GetMaxStockValue(string name)
        {
            StockItem items = Stock.Single(x => x.Name == name);

            return items.NumberOfItems * items.Price;
        }
    }
}
