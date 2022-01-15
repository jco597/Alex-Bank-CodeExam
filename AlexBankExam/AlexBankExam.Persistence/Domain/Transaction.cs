using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexBankExam.Persistence.Domain
{    
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string FromAccount { get; set; }

        [Required]
        [StringLength(20)]
        public string ToAccount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public Customer Owner { get; set; }

    }
}
