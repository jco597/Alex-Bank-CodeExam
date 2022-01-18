using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexBankExam.Persistence.Domain
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [NotMapped]
        public ICollection<Transaction> Transactions { get; set; }
    }
}
