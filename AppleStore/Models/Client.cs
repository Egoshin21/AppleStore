using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class Client
    {
        public Client()
        {
            TovarSales = new HashSet<TovarSale>();
        }

        public int IdClients { get; set; }
        public string Fio { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<TovarSale> TovarSales { get; set; }
    }
}
