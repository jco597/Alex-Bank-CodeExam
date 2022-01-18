using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using AlexBankExam.Persistence;
using AlexBankExam.Persistence.Domain;
using AlexBankExam.API.Services;

namespace AlexBankExam.API.Controllers
{
    public class TransactionsController : BaseApiController
    {       
        private readonly ILogger<DataAccessService> _logger;
        private readonly DataContext _context;
        private readonly DataAccessService _service;

        public TransactionsController(ILogger<DataAccessService> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
            _service = new DataAccessService(_logger, _context);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBankTransactions()
        {
            var result = await _service.GetTransactionsAsync();

            if (result == null)
                return NotFound("Error in fetching Datasets.");
            
            return Ok(result);
        }

        //[Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> CreateBankTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null) return BadRequest();

            try {
                var newTransactionItem = await _service.CreateTransactionAsync(transaction);

                if (newTransactionItem == null)
                    throw new Exception("Failed to add new Transaction");

                return CreatedAtAction(nameof(GetBankTransactions), new { id = newTransactionItem.Id }, newTransactionItem);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //[Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBankTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null) return BadRequest();

            try {
                var updatedItem = await _service.UpdateTransactionAsync(transaction);
                return Ok(updatedItem);
            }
            catch (Exception ex) {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}
