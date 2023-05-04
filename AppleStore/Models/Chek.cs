using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class Chek
    {

        public int IdChek { get; set; }
        public int TovarsIdTovars { get; set; }
        public int Quantity { get; set; }

        public virtual Tovar TovarsIdTovarsNavigation { get; set; } = null!;
    }
}
