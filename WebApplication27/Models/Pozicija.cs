// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApplication27.Models
{
    public partial class Pozicija
    {
        public Pozicija()
        {
            Radnici = new HashSet<Radnici>();
        }

        public int PozicijaId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Radnici> Radnici { get; set; }
    }
}