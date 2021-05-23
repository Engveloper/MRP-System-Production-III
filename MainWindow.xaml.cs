using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoDeProduccion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
    private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnCloseMenu.Visibility = Visibility.Visible;
        }

        private void F_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl uc;
            Contenedor.Children.Clear();

            switch (((ListView)sender).SelectedIndex)
            {
                case 1:
                    uc = new UCLOTES();
                    Contenedor.Children.Add(uc);
                    break;
                case 2:
                    uc = new ModeloQ();
                    Contenedor.Children.Add(uc);
                    break;
                case 3:
                    uc = new PlanAgregado();
                    Contenedor.Children.Add(uc);
                    break;
                default:
                    uc = new UCInicio();
                    Contenedor.Children.Add(uc);
                    break;
            }

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
