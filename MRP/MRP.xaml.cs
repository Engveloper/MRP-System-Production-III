using LoDeProduccion.MRP.items;
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

namespace LoDeProduccion.MRP
{
    /// <summary>
    /// Lógica de interacción para MRP.xaml
    /// </summary>
    public partial class MRP : UserControl
    {
        private items.ProductionProgram program;

        public MRP()
        {
            program = new items.ProductionProgram();
            InitializeComponent();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            bool mainItem = false;
            if (Arbol.Items.Count == 0)
            {
                Arbol.Visibility = Visibility.Visible;
                mainItem = true;
            }

            string name = IngNombreProducto.Text;
            int quantity = int.Parse(IngNumeroComponentes.Text);
            int duration = int.Parse(IngTiempoEsperaProducto.Text);

            var itemSelected = Arbol.SelectedItem;

            if (itemSelected != null)
            {
                var treeViewItemSelected = (TreeViewItem) itemSelected;
                var parentItemComponent = (ItemComponent)treeViewItemSelected.Header;
                var parentProductItem = parentItemComponent.pItem;
                var itemNode = createItemNode(name, quantity, duration, parentProductItem);
                int test = Arbol.Items.IndexOf(treeViewItemSelected);

                if (test != -1)
                {
                    var background = Brushes.Gray.Clone();
                    background.Opacity = 0.5;
                    itemNode.Background = background;
                }
                treeViewItemSelected.Items.Add(itemNode);
            } else {
                if (mainItem)
                {
                    Arbol.Items.Add(createItemNode(name, quantity, duration, null));
                } else
                {
                    MessageBox.Show("Usted no ha selecionado un item padre :V", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            container.Children.Clear();
            ProductionProgram productionProgramUI = new ProductionProgram(program);
            container.Children.Add(productionProgramUI);
        }

        private TreeViewItem createItemNode(
            string name,
            int quantity,
            int duration,
            ProductItem parent
        ){
            var productItem = new ProductItem(name, quantity, duration);
            program.addProductItem(productItem, parent);
            ItemComponent component = new ItemComponent(productItem);
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = component;
            return treeItem;
        }
    }
}
