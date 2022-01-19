using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlexBankExam.Persistence.Domain;
using AlexBankExam.Persistence;

namespace AlexBankExam.API.Services.DataTxn
{
    public class DataEdit
    {
        public class Command : IRequest
        {
            public Transaction Txn { get; set; }
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
                    var txn = await _context.Transactions.FindAsync(request.Txn.Id);
                    if (txn == null)
                        throw new Exception("Record does not exist.");

                    txn.Description = request.Txn.Description ?? txn.Description;
                    txn.FromAccount = request.Txn.FromAccount ?? txn.FromAccount;
                    txn.ToAccount = request.Txn.ToAccount ?? txn.ToAccount;
                    txn.TransactionDate = request.Txn.TransactionDate ?? txn.TransactionDate;
                    //txn.OwnerId = request.Txn.OwnerId ?? txn.OwnerId;

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
