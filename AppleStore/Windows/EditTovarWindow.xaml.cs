
using AppleStore.Models;
using Microsoft.Win32;
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
    /// Логика взаимодействия для EditTovarWindow.xaml
    /// </summary>
    public partial class EditTovarWindow : Window, INotifyPropertyChanged
    {
        public List<Manufacture> ManufactureList { get; set; }
        public List<TovarType> TovarTypeList { get; set; }
        public Tovar CurrentTovar { get; set; }
        public EditTovarWindow(Tovar EditTovar)
        {
            InitializeComponent();
            DataContext = this;
            CurrentTovar = EditTovar;
            using (var context = new aegoshinContext())
            {
                ManufactureList = context.Manufactures.ToList();
                manuf.ItemsSource = ManufactureList;
                TovarTypeList = context.TovarTypes.ToList();
                tovtype.ItemsSource = TovarTypeList;
                manuf.DisplayMemberPath = "Title";
            }
        }
        public string WindowName
        {
            get
            {
                return CurrentTovar.IdTovars == 0 ? "Новый товар" : "Редактирование товара";
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


        private void PhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();
            GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg)|*.png;*.jpg";
            GetImageDialog.InitialDirectory = Environment.CurrentDirectory;
            if (GetImageDialog.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(GetImageDialog.FileName));
                CurrentTovar.Image = GetImageDialog.FileName.Substring(Environment.CurrentDirectory.Length);
                Invalidate();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new aegoshinContext())
            {
                try
                {
                    Tovar tovar = null;
                    if (CurrentTovar.IdTovars != 0)
                        tovar = context.Tovars.Find(CurrentTovar.IdTovars);
                    else
                        tovar = new Tovar();

                    if (tovar != null)
                    {
                        tovar.Title = CurrentTovar.Title;
                        tovar.Quantity = CurrentTovar.Quantity;
                        tovar.Description = CurrentTovar.Description;
                        tovar.TovarTypeIdTovarType = CurrentTovar.TovarTypeIdTovarTypeNavigation.IdTovarType;
                        tovar.ManufactureIdManufacture = CurrentTovar.ManufactureIdManufactureNavigation.IdManufacture;
                        tovar.Price = CurrentTovar.Price;
                        tovar.Image = CurrentTovar.Image;

                        if (tovar.IdTovars == 0)
                            context.Tovars.Add(tovar);
                        else
                            context.Tovars.Update(tovar);

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

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new aegoshinContext())
            {
                try
                {
                    var userCount = context.Users
                        .Where(ps => ps.IdUser == CurrentTovar.IdTovars).Count();

                    if (userCount > 0)
                        throw new Exception("Нельзя удалять продукт с продажами");

                    var tovar = context.Tovars.Find(CurrentTovar.IdTovars);
                    context.Tovars.Remove(tovar);
                    if (context.SaveChanges() > 0)
                    {
                        DialogResult = true;
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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
