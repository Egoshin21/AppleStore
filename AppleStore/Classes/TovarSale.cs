using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class TovarSale
    {
        public string Titl
        {
            get
            {
                return TovarsIdTovarsNavigation?.Title;
            }
        }
        public string Clients
        {
            get
            {
                return ClientsIdClientsNavigation?.Fio;
            }
        }

        public string name
        {
            get
            {
                return UsersIdUserNavigation.Login;
            }
        }

        public string Quantit
        {
            get
            {
                return Quantity + " шт";
            }
        }

        public string DataSale
        {
            get
            {
                return DateSale + "";
            }
        }

    }
}
    
