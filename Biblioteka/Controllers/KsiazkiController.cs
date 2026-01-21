using Biblioteka.Models;
using Biblioteka.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka.Controllers
{
    [Authorize]
    public class KsiazkiController : Controller
    {
        private readonly BibliotekaContext _context;

        public KsiazkiController(BibliotekaContext context)
        {
            _context = context;
        }

        // GET: Ksiazki
        public async Task<IActionResult> Index()
        {
            var bibliotekaContext = _context.Ksiazki.Include(k => k.Autor).Include(k => k.Kategoria);
            return View(await bibliotekaContext.ToListAsync());
        }

        // GET: Ksiazki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ksiazka = await _context.Ksiazki
                .Include(k => k.Autor)
                .Include(k => k.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            return View(ksiazka);
        }

        // GET: Ksiazki/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autorzy, "Id", "ImieNazwisko");
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            return View();
        }

        // POST: Ksiazki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ksiazka ksiazka)
        {
            _context.Ksiazki.Add(ksiazka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));                  
        }

        // GET: Ksiazki/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ksiazka = await _context.Ksiazki.FindAsync(id);
            if (ksiazka == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autorzy, "Id", "ImieNazwisko", ksiazka.AutorId);
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", ksiazka.KategoriaId);
            return View(ksiazka);
        }

        // POST: Ksiazki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Ksiazka ksiazka)
        {
            var ksiazkaId = _context.Ksiazki.Find(id);

            if (ksiazkaId == null)
                return NotFound();

            ksiazkaId.Tytul = ksiazka.Tytul;
            ksiazkaId.RokWydania = ksiazka.RokWydania;
            ksiazkaId.AutorId = ksiazka.AutorId;
            ksiazkaId.KategoriaId = ksiazka.KategoriaId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Ksiazki/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ksiazka = await _context.Ksiazki
                .Include(k => k.Autor)
                .Include(k => k.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            return View(ksiazka);
        }

        // POST: Ksiazki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ksiazka = await _context.Ksiazki.FindAsync(id);
            if (ksiazka != null)
            {
                _context.Ksiazki.Remove(ksiazka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KsiazkaExists(int id)
        {
            return _context.Ksiazki.Any(e => e.Id == id);
        }
    }
}
