using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class Chek
    {
        public Uri ImagePreview
        {
            get
            {
                var imageName = Environment.CurrentDirectory + (TovarsIdTovarsNavigation?.Image ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : null;
            }
        }

        public string Titl
        {
            get
            {
                return TovarsIdTovarsNavigation?.Title;
            }
        }
        public string Pric
        {
            get
            {
                return TovarsIdTovarsNavigation?.Price + " руб";
            }
        }
        public string Quant
        {
            get
            {
                return Quantity + " шт";
            }
        }
    }
}
