using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee.Models;

namespace Employee.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        /*       public async Task<IActionResult> Details(int? id)
               {
                   if (id == null)
                   {
                       return NotFound();
                   }

                   var employees = await _context.Employees
                       .FirstOrDefaultAsync(m => m.EmployeeId == id);
                   if (employees == null)
                   {
                       return NotFound();
                   }

                   return View(employees);
               }
               */
        public IActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employees());
            else
                return View(_context.Employees.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                if (employees.EmployeeId == 0)
                    _context.Add(employees);
                else
                    _context.Update(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employees);
        }

        // GET: Employees/Edit/5
        /*       public async Task<IActionResult> Edit(int? id)
               {
                   if (id == null)
                   {
                       return NotFound();
                   }

                   var employees = await _context.Employees.FindAsync(id);
                   if (employees == null)
                   {
                       return NotFound();
                   }
                   return View(employees);
               }
               [HttpPost]
               [ValidateAntiForgeryToken]
               public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] Employees employees)
               {
                   if (id != employees.EmployeeId)
                   {
                       return NotFound();
                   }

                   if (ModelState.IsValid)
                   {
                       try
                       {
                           _context.Update(employees);
                           await _context.SaveChangesAsync();
                       }
                       catch (DbUpdateConcurrencyException)
                       {
                           if (!EmployeesExists(employees.EmployeeId))
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
                   return View(employees);
               }
               */
        public async Task<IActionResult> Delete(int? id)
        {
            /*
            if (id == null)
            {
                return NotFound();
            }
            */

            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
/*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
    */
}
