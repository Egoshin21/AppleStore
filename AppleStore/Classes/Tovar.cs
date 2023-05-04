using System;
using System.Collections.Generic;

namespace AppleStore.Models
{
    public partial class Tovar
    {
        public override string ToString()
        {
            return Title.ToString();
        }

        public Uri ImagePreview
        {
            get
            {
                var imageName = Environment.CurrentDirectory + (Image ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : null;
            }
        }

        public string Pricce
        {
            get
            {
                return Price + " руб";
            }
        }
    }
}
