using AppleStore.Models;
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
    /// Логика взаимодействия для EditSale.xaml
    /// </summary>
    public partial class EditSale : Window, INotifyPropertyChanged
    {
        public User AddUser { get; set; }
        public Tovar CurrentTovar { get; set; }
        public Chek CurrentSale { get; set; }
        public EditSale(Tovar tov)
        {
            InitializeComponent();
            DataContext = this;
            CurrentTovar = tov;
            CurrentSale = new Chek();
            using (var context = new aegoshinContext())
            {
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

        private void edbtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new aegoshinContext())
            {
                try
                {
                    Chek chek = null;

                    chek = new Chek();

                    if (chek != null)
                    {
                        // сюда добавлять проверки

                        chek.Quantity = CurrentSale.Quantity;
                        chek.TovarsIdTovars = CurrentTovar.IdTovars;


                        if (chek.IdChek == 0)
                            context.Cheks.Add(chek);
                        else
                            context.Cheks.Update(chek);

                        if (context.SaveChanges() > 0)
                        {
                            DialogResult = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    if (ex.InnerException != null)
                        MessageBox.Show(ex.InnerException.Message);
                    else
                        MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
