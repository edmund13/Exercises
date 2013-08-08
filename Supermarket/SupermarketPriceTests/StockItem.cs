using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketPriceTests
{
    class StockItem
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int NumberOfItems { get; set; }

        public PromotionRule Rule { get; set; }
    }
}
