using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Data.SqlClient;
using AlexBankExam.Persistence.Domain;
using AlexBankExam.Persistence;

namespace AlexBankExam.API.Services.DataTxn
{
    public class DataCreate
    {
        public class Command : IRequest
        {
            public Transaction Transaction { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                try {
                    Transaction newTransaction = new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = request.Transaction.Amount,
                        Description = request.Transaction.Description,
                        FromAccount = request.Transaction.FromAccount,
                        ToAccount = request.Transaction.ToAccount,
                        TransactionDate = request.Transaction.TransactionDate.Value
                    };
                    newTransaction.Owner = new Customer() { Id = request.Transaction.Owner.Id };

                    _context.Database.ExecuteSqlRaw($"EXECUTE [dbo].[CreateTransaction] '{newTransaction.Id}', {newTransaction.Amount}, '{newTransaction.Description}', " +
                        $"'{newTransaction.FromAccount}', '{newTransaction.ToAccount}'," +
                        $" '{newTransaction.TransactionDate.Value.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}', " +
                        $"'{newTransaction.Owner.Id}'");

                    await _context.SaveChangesAsync();
                    return Unit.Value;
                }
                catch (Exception ex) {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
