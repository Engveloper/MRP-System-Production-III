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
        public MRP()
        {
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
                var itemNode = createItemNode(name, quantity, duration);
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
                    Arbol.Items.Add(createItemNode(name, quantity, duration));
                } else
                {
                    MessageBox.Show("Usted no ha selecionado un item padre :V", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            if (Arbol.Items.Count == 0)
            {
                string nombre = IngNombreProducto.Text;
                string tiempo = IngTiempoEsperaProducto.Text;
                int numero = Int32.Parse(IngNumeroComponentes.Text);
                MenuItem temp = new MenuItem(nombre, tiempo, numero);
                TreeViewItem test = new TreeViewItem();
                Arbol.Items.Add(temp);

            }
            Arbol.Visibility = Visibility.Visible;
        }

        private TreeViewItem createItemNode(string name, int quantity, int duration)
        {
            ItemComponent component = new ItemComponent(name, quantity, duration);
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = component;
            return treeItem;
        }
    }
}
