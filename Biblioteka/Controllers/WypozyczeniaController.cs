using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;
using Biblioteka.Models.Entities;

namespace Biblioteka.Controllers
{
    public class WypozyczeniaController : Controller
    {
        private readonly BibliotekaContext _context;

        public WypozyczeniaController(BibliotekaContext context)
        {
            _context = context;
        }

        // GET: Wypozyczenia
        public async Task<IActionResult> Index()
        {
            var bibliotekaContext = _context.Wypozyczenia.Include(w => w.Ksiazka);
            return View(await bibliotekaContext.ToListAsync());
        }

        // GET: Wypozyczenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.Wypozyczenia
                .Include(w => w.Ksiazka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }

            return View(wypozyczenie);
        }

        // GET: Wypozyczenia/Create
        public IActionResult Create()
        {
            ViewData["KsiazkaId"] = new SelectList(_context.Ksiazki, "Id", "Id");
            return View();
        }

        // POST: Wypozyczenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KsiazkaId,DataWypozyczenia,DataZwrotu")] Wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wypozyczenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KsiazkaId"] = new SelectList(_context.Ksiazki, "Id", "Id", wypozyczenie.KsiazkaId);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.Wypozyczenia.FindAsync(id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }
            ViewData["KsiazkaId"] = new SelectList(_context.Ksiazki, "Id", "Id", wypozyczenie.KsiazkaId);
            return View(wypozyczenie);
        }

        // POST: Wypozyczenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KsiazkaId,DataWypozyczenia,DataZwrotu")] Wypozyczenie wypozyczenie)
        {
            if (id != wypozyczenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wypozyczenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WypozyczenieExists(wypozyczenie.Id))
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
            ViewData["KsiazkaId"] = new SelectList(_context.Ksiazki, "Id", "Id", wypozyczenie.KsiazkaId);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.Wypozyczenia
                .Include(w => w.Ksiazka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }

            return View(wypozyczenie);
        }

        // POST: Wypozyczenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wypozyczenie = await _context.Wypozyczenia.FindAsync(id);
            if (wypozyczenie != null)
            {
                _context.Wypozyczenia.Remove(wypozyczenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WypozyczenieExists(int id)
        {
            return _context.Wypozyczenia.Any(e => e.Id == id);
        }
    }
}
