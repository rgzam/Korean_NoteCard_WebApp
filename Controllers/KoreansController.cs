using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Korean_NoteCard_WebApp.Data;
using Korean_NoteCard_WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Korean_NoteCard_WebApp.Controllers
{
    public class KoreansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoreansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Koreans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Korean.ToListAsync());
        }

        // GET: Koreans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korean = await _context.Korean
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korean == null)
            {
                return NotFound();
            }

            return View(korean);
        }
        [Authorize]
        // GET: Koreans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Koreans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Korean korean)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korean);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korean);
        }

        // GET: Koreans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korean = await _context.Korean.FindAsync(id);
            if (korean == null)
            {
                return NotFound();
            }
            return View(korean);
        }

        // POST: Koreans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Korean korean)
        {
            if (id != korean.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korean);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KoreanExists(korean.Id))
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
            return View(korean);
        }

        // GET: Koreans/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korean = await _context.Korean
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korean == null)
            {
                return NotFound();
            }

            return View(korean);
        }

        // POST: Koreans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korean = await _context.Korean.FindAsync(id);
            if (korean != null)
            {
                _context.Korean.Remove(korean);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KoreanExists(int id)
        {
            return _context.Korean.Any(e => e.Id == id);
        }
    }
}
