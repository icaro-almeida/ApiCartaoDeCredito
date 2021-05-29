using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCartaoDeCredito.Data
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }

        [Required]
        [MaxLength(19)]
        public string Number { get; set; }

        [Required]
        [MaxLength(4)]
        public string CVV { get; set; }

        [Required]
        [MaxLength(5)]
        public string ExpiryDate { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Person Person { get; set; }               
    }

}
