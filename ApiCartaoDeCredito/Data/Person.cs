using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCartaoDeCredito.Data
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        public string Email { get; set; }
    }

}
