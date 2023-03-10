// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApplication27.Models
{
    public partial class Radnici
    {
        public Radnici()
        {
            Bonus = new HashSet<Bonus>();
            InverseNadredjeni = new HashSet<Radnici>();
            Odbici = new HashSet<Odbici>();
            Odmor = new HashSet<Odmor>();
            Plata = new HashSet<Plata>();
        }

        public int RadniciId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int PozicijaId { get; set; }
        public DateTime PeriodPozicija { get; set; }
        public int? NadredjeniId { get; set; }
        public DateTime? DatumZaposlenja { get; set; }

        public virtual Radnici Nadredjeni { get; set; }
        public virtual Pozicija Pozicija { get; set; }
        public virtual ICollection<Bonus> Bonus { get; set; }
        public virtual ICollection<Radnici> InverseNadredjeni { get; set; }
        public virtual ICollection<Odbici> Odbici { get; set; }
        public virtual ICollection<Odmor> Odmor { get; set; }
        public virtual ICollection<Plata> Plata { get; set; }
    }
}