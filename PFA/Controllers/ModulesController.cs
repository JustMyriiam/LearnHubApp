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
    public class ModulesController : Controller
    {
        private readonly LearnHubDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ModulesController(LearnHubDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Modules
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Index()
        {
            var learnHubDbContext = _context.Modules.Include(m => m.Formation);
            return View(await learnHubDbContext.ToListAsync());
        }

        // GET: Modules/Details/5
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules
                .Include(m => m.Formation)
                .FirstOrDefaultAsync(m => m.IdModule == id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // GET: Modules/Create
        [Authorize(Roles = "Admin, Formateur")]

        public IActionResult Create()
        {
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "Description");
            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Create([Bind("IdModule,NomModule,DescriptionMod,IdFormation")] Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(module);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, etc.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "Description", module.IdFormation);
            return View(module);
        }

        // GET: Modules/Edit/5
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules.FindAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "Description", module.IdFormation);
            return View(module);
        }

        // POST: Modules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Edit(int id, [Bind("IdModule,NomModule,DescriptionMod,IdFormation")] Module module)
        {
            if (id != module.IdModule)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.IdModule))
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
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "Description", module.IdFormation);
            return View(module);
        }

        // GET: Modules/Delete/5
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules
                .Include(m => m.Formation)
                .FirstOrDefaultAsync(m => m.IdModule == id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Formateur")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module != null)
            {
                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.IdModule == id);
        }
    }
}
