using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication27.Models;

namespace WebApplication27.Controllers
{
    public class PozicijaController : Controller
    {
        private readonly ZAPOSLENI_Firma03Context _context;

        public PozicijaController(ZAPOSLENI_Firma03Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Pozicija.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozicija = await _context.Pozicija
                .FirstOrDefaultAsync(m => m.PozicijaId == id);
            if (pozicija == null)
            {
                return NotFound();
            }

            return View(pozicija);
        }

        // GET: Pozicija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pozicija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PozicijaId,Naziv")] Pozicija pozicija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pozicija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pozicija);
        }

        // GET: Pozicija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozicija = await _context.Pozicija.FindAsync(id);
            if (pozicija == null)
            {
                return NotFound();
            }
            return View(pozicija);
        }

        // POST: Pozicija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PozicijaId,Naziv")] Pozicija pozicija)
        {
            if (id != pozicija.PozicijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pozicija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PozicijaExists(pozicija.PozicijaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pozicija);
        }


        private bool PozicijaExists(int id)
        {
            return _context.Pozicija.Any(e => e.PozicijaId == id);
        }
    }
}
