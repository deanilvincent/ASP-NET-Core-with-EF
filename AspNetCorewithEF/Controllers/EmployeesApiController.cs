using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCorewithEF.Models;

namespace AspNetCorewithEF.Controllers
{
    [Produces("application/json")]
    [Route("api/EmployeesApi")]
    public class EmployeesApiController : Controller
    {
        private readonly AspNetCoreDbContext _context;

        public EmployeesApiController(AspNetCoreDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeesApi
        [HttpGet]
        public IEnumerable<Employees> GetEmployees()
        {
            return _context.Employees;
        }

        // GET: api/EmployeesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployees([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // PUT: api/EmployeesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees([FromRoute] int id, [FromBody] Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employees.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeesApi
        [HttpPost]
        public async Task<IActionResult> PostEmployees([FromBody] Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployees", new { id = employees.EmployeeId }, employees);
        }

        // DELETE: api/EmployeesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployees([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();

            return Ok(employees);
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}