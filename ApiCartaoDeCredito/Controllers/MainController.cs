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

            List<CreditCard> ccList = await _context.CreditCards.Where(cc => cc.Person.Email == email).OrderBy(cc => cc.DateOfCreation).ToListAsync();

            return ccList;
        }

    }
}
