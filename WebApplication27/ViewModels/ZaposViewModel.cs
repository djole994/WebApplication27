using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication27.Models;

namespace WebApplication27.ViewModels
{
    public class ZaposViewModel
    {
        public int RadniciId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int PozicijaId { get; set; }
        public Pozicija Pozicija { get; set; }
        public DateTime PeriodPozicija { get; set; }
        public int? NadredjeniId { get; set; }
        public string Nadredjeni { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public decimal IznosPlate { get; set; }
        public DateTime DatumPromjenePlate { get; set; }

        public List<Bonus> Bonusi { get; set; }
        public List<Odbici> Odbici { get; set; }
        public List<Odmor> Odmor { get; set; }
    }

}
