using AjVan.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjVan.Models
{
    public class Teren
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public VrstaTerena VrstaTerena { get; set; }
        public decimal? Cijena { get; set; }

        public long KvartId { get; set; }
        [ForeignKey("KvartId")]
        [JsonIgnore]
        public virtual Kvart Kvart { get; set; }

        public double? GeoSirina { get; set; }
        public double? GeoDuzina { get; set; }
    }
}