using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class User
    {
        public User()
        {
            Cheks = new HashSet<Chek>();
            TovarSales = new HashSet<TovarSale>();
        }

        public int IdUser { get; set; }
        public string User1 { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleIdRole { get; set; }

        public virtual Role RoleIdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Chek> Cheks { get; set; }
        public virtual ICollection<TovarSale> TovarSales { get; set; }
    }
}
