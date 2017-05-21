using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjVan.Models
{
    public class Komentar
    {

        public long Id { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Vrijeme { get; set; }

        public string KorisnikId { get; set; }
        [ForeignKey("KorisnikId")]
        public virtual Korisnik Korisnik { get; set; }

        public long SobaId { get; set; }
        [ForeignKey("SobaId")]
        public virtual Soba Soba { get; set; }
    }
}