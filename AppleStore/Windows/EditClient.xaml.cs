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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window, INotifyPropertyChanged
    {
        public Client AddClient { get; set; }
        public EditClient(Client EditClient)
        {
            InitializeComponent();
            DataContext = this;
            AddClient = EditClient;
            using (var context = new aegoshinContext()) ;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new aegoshinContext())
            {
                try
                {
                    Client client = null;
                    if (AddClient.IdClients != 0)
                        client = context.Clients.Find(AddClient.IdClients);
                    else
                        client = new Client();

                    if (client != null)
                    {
                        client.Fio = AddClient.Fio;
                        client.Email = AddClient.Email;
                        client.Phone = AddClient.Phone;

                        if (client.IdClients == 0)
                            context.Clients.Add(client);
                        else
                            context.Clients.Update(client);

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

        private void Exi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
