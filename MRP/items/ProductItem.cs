using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeProduccion.MRP.items
{
    public class ProductItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Duration { get; set; }
        public int SecurityStock { get; set; }
        public int Stock { get; set; }
        public int PendingOrder { get; set; }
        public ProductItem ParentItem { get; set; }

        public bool isMainItem()
        {
            return this.ParentItem == null;
        }

        public ProductItem(string name, int quantity, int duration, ProductItem parent)
        {
            Name = name;
            Quantity = quantity;
            Duration = duration;
            ParentItem = parent;
        }
        
        public ProductItem(string name, int quantity, int duration)
        {
            Name = name;
            Quantity = quantity;
            Duration = duration;
        }
    }
}
