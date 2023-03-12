using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class TovarSale
    {
        public int IdTovarSale { get; set; }
        public DateTime DateSale { get; set; }
        public int Quantity { get; set; }
        public int TovarsIdTovars { get; set; }
        public int ClientsIdClients { get; set; }
        public int UsersIdUser { get; set; }

        public virtual Client ClientsIdClientsNavigation { get; set; } = null!;
        public virtual Tovar TovarsIdTovarsNavigation { get; set; } = null!;
        public virtual User UsersIdUserNavigation { get; set; } = null!;
    }
}
