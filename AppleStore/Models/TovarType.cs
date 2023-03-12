using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class TovarType
    {
        public TovarType()
        {
            Tovars = new HashSet<Tovar>();
        }

        public int IdTovarType { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Tovar> Tovars { get; set; }
    }
}
