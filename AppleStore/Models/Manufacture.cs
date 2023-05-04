using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class Manufacture
    {
        public Manufacture()
        {
            Tovars = new HashSet<Tovar>();
        }

        public int IdManufacture { get; set; }
        public string Title { get; set; } = null!;


        public virtual ICollection<Tovar> Tovars { get; set; }
    }
}
