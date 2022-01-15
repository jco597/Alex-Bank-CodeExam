using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime? TransactionDate { get; set; }

        public Customer Owner { get; set; }
    }
}
