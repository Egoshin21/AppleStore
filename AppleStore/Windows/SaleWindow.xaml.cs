
using AppleStore.Models;
using MaterialDesignThemes.Wpf;
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
using System.IO;

namespace AppleStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для SaleWindow.xaml
    /// </summary>
    public partial class SaleWindow : Window, INotifyPropertyChanged
    {
        public int Id { get; }
        public string Login { get; }

        public decimal TotalSum;


        private IEnumerable<Tovar> _TovarList;
        public List<TovarType> TovarTypeList { get; set; }
        public List<Manufacture> ManufactureList { get; set; }
        public IEnumerable<Chek> ChekList { get; set; }
        public IEnumerable<Client> ClientList { get; set; }
        public IEnumerable<Tovar> TovarList
        {
            get
            {
                var Result = _TovarList;

                if (ManufactureFilterId > 0)
                    Result = Result.Where(
                        p => p.ManufactureIdManufacture == ManufactureFilterId);

                if (TovarTypeFilterId > 0)
                    Result = Result.Where(
                        p => p.TovarTypeIdTovarType == TovarTypeFilterId);
                switch (SortType)
                {
                    // сортировка по названию продукции
                    case 1:
                        Result = Result.OrderBy(p => p.Title);
                        break;
                    case 2:
                        Result = Result.OrderByDescending(p => p.Title);
                        break;
                    case 3:
                        Result = Result.OrderByDescending(p => p.Price);
                        break;
                    case 4:
                        Result = Result.OrderBy(p => p.Price);
                        break;
                }
                if (SearchFilter != "")
                    Result = Result.Where(
                        p => p.Title.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0
                    );
                return Result;
            }
            set
            {
                _TovarList = value;
                Invalidate();
            }
        }
        public SaleWindow(int id, string name)
        {
            InitializeComponent();
            DataContext = this;
            using (var context = new aegoshinContext())
            {
                ChekList = context.Cheks.Include(tovar => tovar.TovarsIdTovarsNavigation)
                    .ToList();
            }

            decimal totalSum = ChekList.Sum(c => c.Quantity * c.TovarsIdTovarsNavigation.Price);
            Itogo.Content = totalSum.ToString();

            TotalSum = totalSum;

            using (var context = new aegoshinContext())
            {

                ClientList = context.Clients.ToList();
                ChekList = context.Cheks.ToList();
                TovarList = context.Tovars.ToList();
                TovarTypeList = context.TovarTypes.ToList();
                TovarTypeList.Insert(0, new TovarType { Title = "Все типы товаров" });
                ManufactureList = context.Manufactures.ToList();
                ManufactureList.Insert(0, new Manufacture { Title = "Все поставщики" });
            }

            Id = id;
            Login = name;
            Name.Content = Login;

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void Invalidate(string ComponentName = "TovarList")
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(ComponentName));
        }

        private void Invalidate1(string ComponentName = "ChekList")
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(ComponentName));
        }

        private void Invalidate2(string ComponentName = "TotalSum")
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(ComponentName));
        }

        private string SearchFilter = "";
        private void SearchFilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();
        }


        public string[] SortList { get; set; } = 
        {
            "Без сортировки",
            "название по убыванию",
            "название по возрастанию",
            "цена по убыванию",
            "цена по возрастанию"
        };

        private int SortType = 0;
        private void SortTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortType = SortTypeComboBox.SelectedIndex;
            Invalidate();
        }


        private int TovarTypeFilterId = 0;
        private void TovarTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TovarTypeFilterId = (TovarTypeFilter.SelectedItem as TovarType).IdTovarType;
            Invalidate();
        }


        private int ManufactureFilterId = 0;
        private void ManufactureFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ManufactureFilterId = (ManufactureFilter.SelectedItem as Manufacture).IdManufacture;
            Invalidate();
        }

        private void listv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var EditWindow = new Windows.EditSale(listv.SelectedItem as Tovar);
            if ((bool)EditWindow.ShowDialog())
            {
                using (var context = new aegoshinContext())
                {
                    ChekList = context.Cheks.ToList();
                    Invalidate1();

                    TovarList = context.Tovars.ToList();
                    Invalidate();

                    decimal totalSum = ChekList.Sum(c => c.Quantity * c.TovarsIdTovarsNavigation.Price);
                    Itogo.Content = totalSum.ToString();
                    TotalSum = totalSum;
                    Invalidate2();
                }
            }
        }


        private int ClientFilterId = 0;
        private void cln_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientFilterId = (cln.SelectedItem as Client).IdClients;
        }

        private void btnpay_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new aegoshinContext())
            {
                try
                {
                    TovarSale tovarSale = null;
                    tovarSale = new TovarSale();
                    tovarSale.UsersIdUser = Id;
                    tovarSale.ClientsIdClients = ClientFilterId;
                    int AddSale = context.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO `aegoshin`.`TovarSale` (`Tovars_idTovars`, `DateSale`,`Quantity`,`Clients_idClients`,`Users_IdUser`) SELECT `Chek`.`Tovars_idTovars`,  now(),  `Chek`.`Quantity`,  ({tovarSale.ClientsIdClients}), ({Id})  FROM `aegoshin`.`Chek`; DELETE FROM `aegoshin`.`Chek`;");
                    context.TovarSales.Add(tovarSale);
                    MessageBox.Show("Покупка оплачена!");
                    ChekList = context.Cheks.ToList();
                    Invalidate1();

                    TovarList = context.Tovars.ToList();
                    Invalidate();

                    ChekList = context.Cheks.Include(tovar => tovar.TovarsIdTovarsNavigation)
                        .ToList();
                    decimal totalSum = ChekList.Sum(c => c.Quantity * c.TovarsIdTovarsNavigation.Price);
                    Itogo.Content = totalSum.ToString();
                    TotalSum = totalSum;
                    Invalidate2();
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void DelTov_Click(object sender, RoutedEventArgs e)
        {
            var selectedTovar = (sender as Button).Tag as Chek;
            // Ищем чек, в котором находится этот товар
            using (var context = new aegoshinContext())
            {
                var chek = context.Cheks.FirstOrDefault(c => c.IdChek == selectedTovar.IdChek);
                if (chek != null)
                {  //Удаление товара из чека
                    context.Cheks.Remove(chek);
                    context.SaveChanges();
                    ChekList = context.Cheks.ToList();
                    Invalidate1();

                    TovarList = context.Tovars.ToList();
                    Invalidate();

                    decimal totalSum = ChekList.Sum(c => c.Quantity * c.TovarsIdTovarsNavigation.Price);
                    Itogo.Content = totalSum.ToString();

                    TotalSum = totalSum;
                    Invalidate2();
                }
            }
        }
    }
}

