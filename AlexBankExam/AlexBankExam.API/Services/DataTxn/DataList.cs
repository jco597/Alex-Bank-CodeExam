using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AlexBankExam.Persistence.Domain;
using AlexBankExam.Persistence;

namespace AlexBankExam.API.Services.DataTxn
{
    public class DataList
    {
        public class Query : IRequest<List<Transaction>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Transaction>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public Task<List<Transaction>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _context.Transactions.ToListAsync();
            }
        }
    }
}
