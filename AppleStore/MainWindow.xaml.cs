using AppleStore.Models;
using AppleStore.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppleStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public List<TovarType> TovarTypeList { get; set; }
        public List<Manufacture> ManufactureList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            using (var context = new aegoshinContext())
            {
                TovarList = context.Tovars
                    .Include(t => t.TovarTypeIdTovarTypeNavigation)
                    .Include(m => m.ManufactureIdManufactureNavigation)
                    .ToList();
                TovarTypeList = context.TovarTypes.ToList();
                TovarTypeList.Insert(0, new TovarType { Title = "Все типы товаров" });
                ManufactureList = context.Manufactures.ToList();
                ManufactureList.Insert(0, new Manufacture { Title = "Все поставщики" });
            }
        }
        private IEnumerable<Tovar> _TovarList;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Invalidate(string ComponentName = "TovarList")
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

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var NewEditWindow = new EditTovarWindow(ListView.SelectedItem as Tovar);
            if ((bool)NewEditWindow.ShowDialog())
            {
                // при успешном сохранении продукта перечитываем список продукции
                using (var context = new aegoshinContext())
                {
                    TovarList = context.Tovars
                        .Include(t => t.TovarTypeIdTovarTypeNavigation)
                        .Include(m => m.ManufactureIdManufactureNavigation)
                        .ToList();
                    Invalidate();
                }
            }
        }

        private void AddTov_Click(object sender, RoutedEventArgs e)
        {
            var NewEditwindow = new EditTovarWindow(new Tovar());
            if ((bool)NewEditwindow.ShowDialog())
            {
                using (var context = new aegoshinContext())
                {
                    TovarList = context.Tovars.ToList();
                    Invalidate();
                }
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var NewEditWindow = new EditClient(new Client());
            if ((bool)NewEditWindow?.ShowDialog())
            {
                using (var context = new aegoshinContext())
                {
                    ClientList = context.Clients.ToList();
                    Invalidate();
                }
            }
        }

        private void ClList_Click(object sender, RoutedEventArgs e)
        {
            var auto = new ClList();
            auto.Show();
        }

        private void SaleList_Click(object sender, RoutedEventArgs e)
        {
            var auto = new TovSale();
            auto.Show();
        }

        private void ex_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
