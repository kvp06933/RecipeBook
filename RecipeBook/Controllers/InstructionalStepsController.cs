using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    public class InstructionalStepsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructionalStepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstructionalSteps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InstructionalSteps.Include(i => i.Recipe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InstructionalSteps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructionalStep = await _context.InstructionalSteps
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructionalStep == null)
            {
                return NotFound();
            }

            return View(instructionalStep);
        }

        // GET: InstructionalSteps/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Content");
            return View();
        }

        // POST: InstructionalSteps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecipeId,StepNumber,Content")] InstructionalStep instructionalStep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructionalStep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Content", instructionalStep.RecipeId);
            return View(instructionalStep);
        }

        // GET: InstructionalSteps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructionalStep = await _context.InstructionalSteps.FindAsync(id);
            if (instructionalStep == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Content", instructionalStep.RecipeId);
            return View(instructionalStep);
        }

        // POST: InstructionalSteps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecipeId,StepNumber,Content")] InstructionalStep instructionalStep)
        {
            if (id != instructionalStep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructionalStep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructionalStepExists(instructionalStep.Id))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Content", instructionalStep.RecipeId);
            return View(instructionalStep);
        }

        // GET: InstructionalSteps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructionalStep = await _context.InstructionalSteps
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructionalStep == null)
            {
                return NotFound();
            }

            return View(instructionalStep);
        }

        // POST: InstructionalSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructionalStep = await _context.InstructionalSteps.FindAsync(id);
            _context.InstructionalSteps.Remove(instructionalStep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructionalStepExists(int id)
        {
            return _context.InstructionalSteps.Any(e => e.Id == id);
        }
    }
}
