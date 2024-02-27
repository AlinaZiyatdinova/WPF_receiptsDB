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
    /// Логика взаимодействия для AllIngridientsPage.xaml
    /// </summary>
    public partial class AllIngridientsPage : Page
    {
        public AllIngridientsPage()
        {
            InitializeComponent();
            using (ReceiptsDbContext context = new())
            {
                listView_ingridients.ItemsSource = context
                .Ingridients
                .Include(r => r.ReceiptIngridients)
                .ThenInclude(ri => ri.ReceiptNavigation)
                .ToList();
            }
        }

        private void textBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (ReceiptsDbContext context = new())
            {
                var query = context.Ingridients.Include(r => r.ReceiptIngridients).ThenInclude(ri => ri.ReceiptNavigation).AsQueryable();

                string searchString = textBox_Search.Text;
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    query = query.Where(p => p.Name.Contains(searchString));
                }

                var filtredList = query.ToList();
                listView_ingridients.ItemsSource = filtredList;
            }
        }
    }
}
