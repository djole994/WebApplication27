using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication27.Models;
using WebApplication27.ViewModels;

namespace WebApplication27.Controllers
{
    public class HomeController : Controller
    {
        private readonly ZAPOSLENI_Firma03Context _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ZAPOSLENI_Firma03Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var zaposlenici = await _context.Radnici
                .Include(z => z.Pozicija)
                .ToListAsync();

            return View(zaposlenici);
        }


        public IActionResult Radnik()
        {
            var zaposViewModels = _context.Radnici
                .Include(r => r.Bonus)
                .Include(r => r.Odbici)
                .Include(r => r.Odmor)
                .Include(r => r.Pozicija)
                .Include(r => r.Nadredjeni)
                .Select(r => new ZaposViewModel
                {
                    RadniciId = r.RadniciId,
                    Ime = r.Ime,
                    Prezime = r.Prezime,
                    PozicijaId = r.PozicijaId,
                    Pozicija = r.Pozicija,
                    PeriodPozicija = r.PeriodPozicija,
                    NadredjeniId = r.NadredjeniId,
                    Nadredjeni = r.Nadredjeni != null ? r.Nadredjeni.Ime + " " + r.Nadredjeni.Prezime : "Nema nadređenog",
                    DatumZaposlenja = r.DatumZaposlenja ?? DateTime.MinValue,
                    Bonusi = r.Bonus.ToList(),
                    Odbici = r.Odbici.ToList(),
                    Odmor = r.Odmor.ToList()
                })
                .ToList();

            return View(zaposViewModels);
        }




        public async Task<IActionResult> IstorijaPromjenePlate(int id)
        {
            var istorijaPromjena = await _context.Plata
                .Include(p => p.Radnici)
                .Where(p => p.RadniciId == id && p.Iznos != 0)
                .OrderBy(p => p.DatumPromjene)
                .ToListAsync();

            var trenutnaPlata = await _context.Plata
                .Where(p => p.RadniciId == id && p.Iznos != 0)
                .OrderByDescending(p => p.DatumPromjene)
                .FirstOrDefaultAsync();

            ViewBag.ImePrezime = istorijaPromjena.FirstOrDefault()?.Radnici?.Ime + " " + istorijaPromjena.FirstOrDefault()?.Radnici?.Prezime;
            ViewBag.RadnikId = id;
            ViewBag.TrenutnaPlata = trenutnaPlata?.Iznos;

            return View(istorijaPromjena);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Pozicije = _context.Pozicija.ToList();
            ViewBag.Radnici = _context.Radnici.ToList();

            return View();
        }
     
        [HttpPost]
        public IActionResult Create(Radnici radnik, Plata plata)
        {
            if (ModelState.IsValid)
            {
                _context.Radnici.Add(radnik);
                _context.SaveChanges();

                plata.RadniciId = radnik.RadniciId;
                _context.Plata.Add(plata);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(radnik);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
