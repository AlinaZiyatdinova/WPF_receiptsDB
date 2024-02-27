
using Microsoft.EntityFrameworkCore;
using prakt10_receiptsDB.Models;
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

namespace prakt10_receiptsDB.Views
{
    /// <summary>
    /// Логика взаимодействия для ReceiptsPage.xaml
    /// </summary>
    public partial class ReceiptsPage : Page
    {
        private List<Receipt> receipts;
        public ReceiptsPage()
        {
            InitializeComponent();
            using (ReceiptsDbContext context = new())
            {
                receiptsItemsControl.ItemsSource = context
                .Ingridients
                .Include(r => r.ReceiptIngridients)
                .ThenInclude(ri => ri.IngridientNavigation)
                .ToList();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (ReceiptsDbContext context = new())
            {
                var query = context.Receipts.Include(r=>r.ReceiptIngridients).ThenInclude(ri => ri.IngridientNavigation).AsQueryable();

                string searchString = textBox_Search.Text;
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    query = query.Where(p => p.Name.Contains(searchString));
                }

                var filtredList = query.ToList();
                receiptsItemsControl.ItemsSource = filtredList;
            }
        }
    }
}
