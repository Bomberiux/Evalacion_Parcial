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
    [Authorize(Roles = "Jefe de Departamento,Empleado")]
    public class EvaluacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evaluaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Evaluaciones.Include(e => e.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Evaluaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionesModel = await _context.Evaluaciones
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluacionesModel == null)
            {
                return NotFound();
            }

            return View(evaluacionesModel);
        }

        // GET: Evaluaciones/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: Evaluaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,EmpleadoId,EvaluadorId,Calificacion,Comentarios,Periodo")] EvaluacionesModel evaluacionesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluacionesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", evaluacionesModel.EmpleadoId);
            return View(evaluacionesModel);
        }

        // GET: Evaluaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionesModel = await _context.Evaluaciones.FindAsync(id);
            if (evaluacionesModel == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", evaluacionesModel.EmpleadoId);
            return View(evaluacionesModel);
        }

        // POST: Evaluaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,EmpleadoId,EvaluadorId,Calificacion,Comentarios,Periodo")] EvaluacionesModel evaluacionesModel)
        {
            if (id != evaluacionesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluacionesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluacionesModelExists(evaluacionesModel.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", evaluacionesModel.EmpleadoId);
            return View(evaluacionesModel);
        }

        // GET: Evaluaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionesModel = await _context.Evaluaciones
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluacionesModel == null)
            {
                return NotFound();
            }

            return View(evaluacionesModel);
        }

        // POST: Evaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluacionesModel = await _context.Evaluaciones.FindAsync(id);
            if (evaluacionesModel != null)
            {
                _context.Evaluaciones.Remove(evaluacionesModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluacionesModelExists(int id)
        {
            return _context.Evaluaciones.Any(e => e.Id == id);
        }
    }
}
