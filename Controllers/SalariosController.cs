using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recursos_Humanos.Data;
using Recursos_Humanos.Models;

namespace Recursos_Humanos.Controllers
{
    public class SalariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salarios.Include(s => s.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Salarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salariosModel = await _context.Salarios
                .Include(s => s.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salariosModel == null)
            {
                return NotFound();
            }

            return View(salariosModel);
        }

        // GET: Salarios/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: Salarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoId,Monto,FechaVigencia")] SalariosModel salariosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salariosModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", salariosModel.EmpleadoId);
            return View(salariosModel);
        }

        // GET: Salarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salariosModel = await _context.Salarios.FindAsync(id);
            if (salariosModel == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", salariosModel.EmpleadoId);
            return View(salariosModel);
        }

        // POST: Salarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoId,Monto,FechaVigencia")] SalariosModel salariosModel)
        {
            if (id != salariosModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salariosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalariosModelExists(salariosModel.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", salariosModel.EmpleadoId);
            return View(salariosModel);
        }

        // GET: Salarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salariosModel = await _context.Salarios
                .Include(s => s.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salariosModel == null)
            {
                return NotFound();
            }

            return View(salariosModel);
        }

        // POST: Salarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salariosModel = await _context.Salarios.FindAsync(id);
            if (salariosModel != null)
            {
                _context.Salarios.Remove(salariosModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalariosModelExists(int id)
        {
            return _context.Salarios.Any(e => e.Id == id);
        }
    }
}
