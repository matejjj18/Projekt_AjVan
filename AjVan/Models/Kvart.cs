using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjVan.Models
{
    public class Kvart
    {
        public long Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Teren> Tereni { get; set; }
    }
}