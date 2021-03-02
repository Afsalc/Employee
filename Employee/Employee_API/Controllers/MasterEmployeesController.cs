using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_API.Models;

namespace Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterEmployeesController : ControllerBase
    {
        private readonly Employee_dbContext _context;

        public MasterEmployeesController(Employee_dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterEmployee>>> GetMasterEmployee()
        {
            return await _context.MasterEmployee.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MasterEmployee>> GetMasterEmployee(int id)
        {
            var masterEmployee = await _context.MasterEmployee.FindAsync(id);

            if (masterEmployee == null)
            {
                return NotFound();
            }

            return masterEmployee;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterEmployee(int id, MasterEmployee masterEmployee)
        {
            if (id != masterEmployee.PkempId)
            {
                return BadRequest();
            }
            List<MasterEmployee> x = new List<MasterEmployee>();
            x = await _context.MasterEmployee
               .Where(uid => uid.EmployeePhonenumber == masterEmployee.EmployeePhonenumber && uid.PkempId!= masterEmployee.PkempId)
               .ToListAsync();
            if (x.Count == 0)
            {
                _context.Entry(masterEmployee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

       
        [HttpPost]
        public async Task<ActionResult<MasterEmployee>> PostMasterEmployee(MasterEmployee masterEmployee)
        {
            try
            {
                _context.MasterEmployee.Add(masterEmployee);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterEmployee>> DeleteMasterEmployee(int id)
        {
            var masterEmployee = await _context.MasterEmployee.FindAsync(id);
            if (masterEmployee == null)
            {
                return NotFound();
            }

            _context.MasterEmployee.Remove(masterEmployee);
            await _context.SaveChangesAsync();
            try
            {
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("Phone/{number}")]
        public async Task<ActionResult<IEnumerable<MasterEmployee>>> GetDDListMasterEmployee(string number)
        {
            return await _context.MasterEmployee
                .Where(uid => uid.EmployeePhonenumber == number)
                .ToListAsync();
        }


        private bool MasterEmployeeExists(int id)
        {
            return _context.MasterEmployee.Any(e => e.PkempId == id);
        }
    }
}
