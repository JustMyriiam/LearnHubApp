using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Controllers
{
    public class FormateursController : Controller
    {
        private readonly LearnHubDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FormateursController(LearnHubDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]

        // GET: Formateurs
        public async Task<IActionResult> Index()
        {
              return _context.Formateurs != null ? 
                          View(await _context.Formateurs.ToListAsync()) :
                          Problem("Entity set 'LearnHubDbContext.Formateurs'  is null.");
        }

        // GET: Formateurs/Details/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formateurs == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formateur == null)
            {
                return NotFound();
            }

            return View(formateur);
        }

        // GET: Formateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,DateNaissance,NumTel,Email,Biographie")] Formateur formateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formateur);
        }

        [Authorize(Roles = "Admin")]

        // GET: Formateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formateurs == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null)
            {
                return NotFound();
            }
            return View(formateur);
        }

        // POST: Formateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,DateNaissance,NumTel,Email,Biographie")] Formateur formateur)
        {
            if (id != formateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormateurExists(formateur.Id))
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
            return View(formateur);
        }

        // GET: Formateurs/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formateurs == null)
            {
                return NotFound();
            }

            var formateur = await _context.Formateurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formateur == null)
            {
                return NotFound();
            }

            return View(formateur);
        }

        // POST: Formateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formateurs == null)
            {
                return Problem("Entity set 'LearnHubDbContext.Formateurs'  is null.");
            }
            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur != null)
            {
                _context.Formateurs.Remove(formateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormateurExists(int id)
        {
          return (_context.Formateurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
