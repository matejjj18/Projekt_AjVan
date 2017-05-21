using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjVan.Models.ViewModels
{
    public class SobaKomentarViewModel
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime Pocetak { get; set; }
        public TimeSpan Trajanje { get; set; }
        public virtual Korisnik Admin { get; set; }
        public virtual Sport Sport { get; set; }
        public virtual Teren Teren { get; set; }
        public int MaksimalniBrojIgraca { get; set; }
        public virtual ICollection<Korisnik> Igraci { get; set; }

        //ovo se koristi pri slanju upisanog komentara u formu iz viewa Details u kontroler Komentari, akcija Create
        public Komentar NoviKomentar { get; set; }

        public virtual ICollection<Komentar> Komentari { get; set; }
    }
}