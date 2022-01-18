using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlexBankExam.Persistence.Domain;

namespace AlexBankExam.API.Services
{
    interface IDataAccessService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        
        Task<Transaction> CreateTransactionAsync(Transaction transaction);

        Task<Transaction> UpdateTransactionAsync(Transaction transaction);

    }
}
