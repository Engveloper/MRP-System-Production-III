using LoDeProduccion.MRP.items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LoDeProduccion.MRP
{
    /// <summary>
    /// Lógica de interacción para ProductionProgram.xaml
    /// </summary>
    public partial class ProductionProgram : UserControl
    {
        private items.ProductionProgram Program;
        private ObservableCollection<ProductItem> data = new ObservableCollection<ProductItem>(); 
        public ProductionProgram(items.ProductionProgram program)
        {
            InitializeComponent();
            Program = program;
            inventoryTable.ItemsSource = data;
            this.Loaded += ProductionProgram_Loaded;
        }

        private void ProductionProgram_Loaded(object sender, RoutedEventArgs e)
        {
            var productItems = Program.productItems;
            productItems.ForEach(delegate (ProductItem item)
            {
                data.Add(item);
            });

        }
    }
}
