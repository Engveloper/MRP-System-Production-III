using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoDeProduccion.MRP.items
{
    /// <summary>
    /// Lógica de interacción para ItemComponent.xaml
    /// </summary>
    public partial class ItemComponent : UserControl
    {
        public ProductItem pItem { get; }
        public ItemComponent(string name, int quantity, int duration)
        {
            InitializeComponent();
            this.nameLb.Content = $"{name} ({quantity} unds.)";
            this.quantityLb.Content = $"{duration} dias";
            pItem = new ProductItem(name, quantity, duration);
        }
        
        public ItemComponent(ProductItem pItem)
        {
            InitializeComponent();
            this.pItem = pItem;
            this.nameLb.Content = $"{pItem.Name} ({pItem.Quantity} unds.)";
            this.quantityLb.Content = $"{pItem.Duration} dias";
        }
    }
}
