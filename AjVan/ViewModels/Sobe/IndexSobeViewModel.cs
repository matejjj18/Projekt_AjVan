using AjVan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjVan.ViewModels.Sobe
{
    public class IndexSobeViewModel
    {
        public IEnumerable<Soba> Sobe { get; set; }
        public IEnumerable<Sport> Sportovi { get; set; }
        public IEnumerable<Kvart> Kvartovi { get; set; }
    }
}