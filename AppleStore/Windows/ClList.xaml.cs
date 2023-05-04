using AppleStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для ClList.xaml
    /// </summary>
    public partial class ClList : Window, INotifyPropertyChanged
    {
        public IEnumerable<Client> ClientList { get; set; }
        public ClList()
        {
            InitializeComponent();
            DataContext = this;
            using (var context = new aegoshinContext())
            {
                ClientList = context.Clients.ToList();
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
