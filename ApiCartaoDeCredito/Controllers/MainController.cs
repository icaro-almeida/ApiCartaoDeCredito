using ApiCartaoDeCredito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCartaoDeCredito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public MainController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Main
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CreditCard>>> GetCreditCards(string email)
        {
            if (!DataHelper.IsValidEmail(email))
                return BadRequest();

            List<CreditCard> ccList = await _context.CreditCards.Where(cc => cc.Person.Email == email).OrderBy(cc => cc.DateOfCreation).ToListAsync();

            return ccList;
        }

        // POST: api/Main
        [HttpPost("{email}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreditCard>> PostCreditCard(string email)
        {
            if (!DataHelper.IsValidEmail(email))
                return BadRequest();

            Person person = await _context.People.FirstOrDefaultAsync(i => i.Email == email);

            if (person == null)
            {
                person = new Person
                {
                    Email = email
                };
                _context.People.Add(person);
                await _context.SaveChangesAsync(); //Adds person to db and gets auto incremented PersonId                
            }

            var newCC = DataHelper.GenerateCreditCard(person.PersonId, expYears: 10);

            _context.CreditCards.Add(newCC);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCreditCards), new { email = person.Email }, newCC);
        }



    }
}
