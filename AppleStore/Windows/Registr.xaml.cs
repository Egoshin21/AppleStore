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
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Window, INotifyPropertyChanged
    {
        public User AddUser { get; set; }
        public List<Role> RoleList { get; set; }
        public Registr(User Registr)
        {
            InitializeComponent();
            DataContext = this;
            AddUser = Registr;
            using (var context = new aegoshinContext())
            {
                RoleList = context.Roles.ToList();
                role.ItemsSource = RoleList;
                role.DisplayMemberPath = "Role1";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Invalidate(string ComponentName = "RoleList")
        {
            if (PropertyChanged != null)
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(ComponentName));
        }
        private void savereg_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new aegoshinContext())
            {
                try
                {
                    User user = null;
                    if (AddUser.IdUser != 0)
                        user = context.Users.Find(AddUser.IdUser);
                    else
                        user = new User();

                    if (user != null)
                    {
                        user.User1 = AddUser.User1;
                        user.Password = AddUser.Password;
                        user.Login = AddUser.Login;
                        user.RoleIdRole = AddUser.RoleIdRoleNavigation.IdRole;

                        if (user.IdUser == 0)
                            context.Users.Add(user);
                        else
                            context.Users.Update(user);

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
