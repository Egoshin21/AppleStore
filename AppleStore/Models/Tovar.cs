using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class Tovar
    {
        public Tovar()
        {
            Cheks = new HashSet<Chek>();
            TovarSales = new HashSet<TovarSale>();
        }

        public int IdTovars { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public int TovarTypeIdTovarType { get; set; }
        public int ManufactureIdManufacture { get; set; }

        public virtual Manufacture ManufactureIdManufactureNavigation { get; set; } = null!;
        public virtual TovarType TovarTypeIdTovarTypeNavigation { get; set; } = null!;
        public virtual ICollection<Chek> Cheks { get; set; }
        public virtual ICollection<TovarSale> TovarSales { get; set; }
    }
}
