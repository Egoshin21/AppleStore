using AppleStore.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace AppleStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для TovSale.xaml
    /// </summary>
    public partial class TovSale : Window
    {
        public List<TovarSale> SalesList { get; set; }
        public TovSale()
        {
            InitializeComponent();
            DataContext = this;

            using (aegoshinContext db = new aegoshinContext())
            {
                try
                {
                    SalesList = db.TovarSales
                        .Include(p => p.TovarsIdTovarsNavigation)
                           .Include(c => c.ClientsIdClientsNavigation)
                                 .Include(c => c.UsersIdUserNavigation)
                        .ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
