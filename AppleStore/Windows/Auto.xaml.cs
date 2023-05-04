using AppleStore.Models;
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
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Window
    {
        public int Id { get; }
        public string Login { get; }
        public Auto()
        {
            InitializeComponent();
        }

        private void Entbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                aegoshinContext db = new aegoshinContext();

                var uSerBD = db.Users.FirstOrDefault(x => x.User1 == login.Text && x.Password == password.Password);
                if (uSerBD == null)
                {
                    MessageBox.Show("Такого пользователя нет !", "Ошибка при авторизации",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (uSerBD.RoleIdRole)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте , Администратор - " + uSerBD.Login + "!");
                            MainWindow sda = new MainWindow();
                            sda.Show();
                            this.Close();

                            break;

                        case 2:
                            MessageBox.Show("Здравствуйте " + uSerBD.Login + "!");

                            var id = uSerBD.IdUser;
                            var name = uSerBD.Login;
                            SaleWindow sd = new SaleWindow(id, name);
                            sd.Show();
                            this.Close();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Regbtn_Click(object sender, RoutedEventArgs e)
        {
            var NewEditwindow = new Registr(new User());
            if ((bool)NewEditwindow.ShowDialog())
            {
                using (var context = new aegoshinContext()) ;
            }
        }
    }
}
