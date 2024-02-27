using Microsoft.EntityFrameworkCore;
using prakt10_receiptsDB.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prakt10_receiptsDB
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

        private void button_goToReceiptesPage_Click(object sender, RoutedEventArgs e)
        {
            var page = new ReceiptsPage();
            mainFrame.Navigate(page);
        }

        private void button_goToPageShowList_Click(object sender, RoutedEventArgs e)
        {
            var page = new AllIngridientsPage();
            mainFrame.Navigate(page);   
        }

        private void button_addReceptesPage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В разработке", "Результат действий", MessageBoxButton.OK, MessageBoxImage.None);
        }
    }
}