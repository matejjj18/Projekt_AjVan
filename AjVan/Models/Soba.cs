using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjVan.Models
{
    public class Soba
    {

        public long Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime Pocetak { get; set; }
        public TimeSpan Trajanje { get; set; }

        public string AdminId { get; set; }

        [ForeignKey("AdminId")]
        public virtual Korisnik Admin { get; set; }

        [Display(Name = "Sport")]
        public long SportId { get; set; }
        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }

        [Required(ErrorMessage = "Potrebno odabrati teren")]
        public long TerenId { get; set; }

        [ForeignKey("TerenId")]
        public virtual Teren Teren { get; set; }

        public ICollection<Korisnik> Igraci { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Broj igrača mora biti 1 ili više")]
        [Required(ErrorMessage = "Potrebno upisati maksimalni broj igrača")]
        [Display(Name = "Max. igrača")]
        public int MaksimalniBrojIgraca { get; set; }
    }
}