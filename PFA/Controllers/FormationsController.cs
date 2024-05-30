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
    public class FormationsController : Controller
    {
        private readonly LearnHubDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FormationsController(LearnHubDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Formations

        public async Task<IActionResult> Index()
        {
            var learnHubDbContext = _context.Formations.Include(f => f.Categorie).Include(f => f.Formateur).Include(f => f.Modules);
            return View(await learnHubDbContext.ToListAsync());
        }

        // GET: Formations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .Include(f => f.Categorie)
                .Include(f => f.Formateur)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // GET: Formations/Create
        [Authorize(Roles = "Admin, Formateur")]
        public IActionResult Create()
        {
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "DescriptionCat");
            ViewData["IdFormateur"] = new SelectList(_context.Formateurs, "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Create([Bind("IdFormation,NomFormation,Duree,Description,Prix,ImgFormationPath,IdCategorie,IdFormateur")] Formation formation)
        {
            if (ModelState.IsValid)
            {
                _context.Formations.Add(formation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "DescriptionCat", formation.IdCategorie);
            ViewData["IdFormateur"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.IdFormateur);
            return View(formation);
        }


        // GET: Formations/Edit/5
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "DescriptionCat", formation.IdCategorie);
            ViewData["IdFormateur"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.IdFormateur);
            return View(formation);
        }

        // POST: Formations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Edit(int id, [Bind("IdFormation,NomFormation,Duree,Description,Prix,ImgFormationPath,IdCategorie,IdFormateur")] Formation formation)
        {
            if (id != formation.IdFormation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formation.IdFormation))
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
            ViewData["IdCategorie"] = new SelectList(_context.Categories, "IdCategorie", "DescriptionCat", formation.IdCategorie);
            ViewData["IdFormateur"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.IdFormateur);
            return View(formation);
        }

        // GET: Formations/Delete/5
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formations == null)
            {
                return NotFound();
            }

            var formation = await _context.Formations
                .Include(f => f.Categorie)
                .Include(f => f.Formateur)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        // POST: Formations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formations == null)
            {
                return Problem("Entity set 'LearnHubDbContext.Formations'  is null.");
            }
            var formation = await _context.Formations.FindAsync(id);
            if (formation != null)
            {
                _context.Formations.Remove(formation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationExists(int id)
        {
          return (_context.Formations?.Any(e => e.IdFormation == id)).GetValueOrDefault();
        }
    }
}
