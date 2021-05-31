using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeProduccion.MRP.items
{
    class ProductionProgram
    {
        public ProductItem MainProductItem { get; set; }

        public List<ProductItem> productItems;

        public ProductionProgram()
        {
            productItems = new List<ProductItem>();
        }

        public void addProductItem(ProductItem newItem, ProductItem parent)
        {
            newItem.ParentItem = parent;
            productItems.Add(newItem);
        }

        public void removeProductItem(ProductItem item)
        {
            productItems.Remove(item);
        }


    }
}
