using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recursos_Humanos.Data;
using Recursos_Humanos.Models;

namespace Recursos_Humanos.Controllers
{
    [Authorize(Roles = "Administrador de Recursos Humanos")]
    public class EvaluadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evaluadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Evaluadores.ToListAsync());
        }

        // GET: Evaluadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluadoresModel = await _context.Evaluadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluadoresModel == null)
            {
                return NotFound();
            }

            return View(evaluadoresModel);
        }

        // GET: Evaluadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evaluadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cargo,Email")] EvaluadoresModel evaluadoresModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluadoresModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluadoresModel);
        }

        // GET: Evaluadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluadoresModel = await _context.Evaluadores.FindAsync(id);
            if (evaluadoresModel == null)
            {
                return NotFound();
            }
            return View(evaluadoresModel);
        }

        // POST: Evaluadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cargo,Email")] EvaluadoresModel evaluadoresModel)
        {
            if (id != evaluadoresModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluadoresModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluadoresModelExists(evaluadoresModel.Id))
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
            return View(evaluadoresModel);
        }

        // GET: Evaluadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluadoresModel = await _context.Evaluadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluadoresModel == null)
            {
                return NotFound();
            }

            return View(evaluadoresModel);
        }

        // POST: Evaluadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluadoresModel = await _context.Evaluadores.FindAsync(id);
            if (evaluadoresModel != null)
            {
                _context.Evaluadores.Remove(evaluadoresModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluadoresModelExists(int id)
        {
            return _context.Evaluadores.Any(e => e.Id == id);
        }
    }
}
