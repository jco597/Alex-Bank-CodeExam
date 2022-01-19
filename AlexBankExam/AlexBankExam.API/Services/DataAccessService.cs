using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AlexBankExam.Persistence;
using AlexBankExam.Persistence.Domain;
using MediatR;

namespace AlexBankExam.API.Services
{
    public class DataAccessService : IDataAccessService
    {
        private readonly DataContext _context;
        private readonly ILogger<DataAccessService> _logger;

        public DataAccessService(ILogger<DataAccessService> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {

            try {
                var data = await _context.Transactions.ToListAsync();
                if (data == null)
                    return null;
                return data;
            }
            catch (Exception ex) {
                _logger.LogError($"DataAccessService::GetTransactions::Message = {ex.Message}");
                throw new Exception(ex.Message, ex);
            }
            finally {
                await _context.DisposeAsync();
            }
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction txn)
        {
            _context.Database.BeginTransaction();
            var result = _context.Transactions.Add(txn);

            try {
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return result.Entity;
            }
            catch (Exception ex) {
                await _context.Database.RollbackTransactionAsync();
                _logger.LogError($"DataAccessService::CreateTransaction::Message = {ex.Message}");
                throw new Exception(ex.Message, ex);
            }
            finally {
                await _context.DisposeAsync();
            }
        }
        public async Task<Transaction> UpdateTransactionAsync(Transaction txn)
        {
            try {
                var result = _context.Entry(txn);
                result.State = EntityState.Modified;

                _context.Entry(txn).Property("Id").IsModified = false;
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateException e) {
                _logger.LogError(e.Message, e);
                throw e.InnerException;
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
                throw new Exception(ex.Message, ex);
            }
            finally {
                await _context.DisposeAsync();
            }
        }

    }
}
