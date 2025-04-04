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
    [Authorize(Roles = "Administrador de Recursos Humanos,Gestor de Personal,Jefe de Departamento,Empleado")]
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Empleados.Include(e => e.Departamento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleadosModel = await _context.Empleados
                .Include(e => e.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleadosModel == null)
            {
                return NotFound();
            }

            return View(empleadosModel);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Puesto,DepartamentoId,Salario")] EmpleadosModel empleadosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleadosModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nombre", empleadosModel.DepartamentoId);
            return View(empleadosModel);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleadosModel = await _context.Empleados.FindAsync(id);
            if (empleadosModel == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nombre", empleadosModel.DepartamentoId);
            return View(empleadosModel);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Puesto,DepartamentoId,Salario")] EmpleadosModel empleadosModel)
        {
            if (id != empleadosModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleadosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadosModelExists(empleadosModel.Id))
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
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Nombre", empleadosModel.DepartamentoId);
            return View(empleadosModel);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleadosModel = await _context.Empleados
                .Include(e => e.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleadosModel == null)
            {
                return NotFound();
            }

            return View(empleadosModel);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleadosModel = await _context.Empleados.FindAsync(id);
            if (empleadosModel != null)
            {
                _context.Empleados.Remove(empleadosModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadosModelExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
