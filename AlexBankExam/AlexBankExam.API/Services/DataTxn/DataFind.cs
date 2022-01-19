using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlexBankExam.Persistence.Domain;
using AlexBankExam.Persistence;

namespace AlexBankExam.API.Services.DataTxn
{
    public class DataFind
    {
        public class Query : IRequest<Transaction>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Transaction>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Transaction> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Transactions.FindAsync(request.Id);
            }

        }

    }
}
