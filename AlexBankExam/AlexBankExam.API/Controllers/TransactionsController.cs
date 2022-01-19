using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AlexBankExam.Persistence;
using AlexBankExam.Persistence.Domain;
using AlexBankExam.API.Services;
using AlexBankExam.API.Services.DataTxn;

namespace AlexBankExam.API.Controllers
{
    public class TransactionsController : BaseApiController
    {
        private readonly ILogger<DataAccessService> _logger;

        public TransactionsController(ILogger<DataAccessService> logger, IMediator mediator)
        {
            _logger = logger;
        }

        //[Authorize]        
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetBankTransaction(Guid id) => await Mediator.Send(new DataFind.Query { Id = id });

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetBankTransactionList() => await Mediator.Send(new DataList.Query());


        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBankTransaction(Transaction transaction)
        {
            if (transaction == null) return BadRequest();

            try {
                return Ok(await Mediator.Send(new DataCreate.Command { Transaction = transaction }));
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBankTransaction(Guid id, Transaction transaction)
        {
            if (transaction == null) return BadRequest();

            try {
                transaction.Id = id;
                return Ok(await Mediator.Send(new DataEdit.Command { Txn = transaction }));
            }
            catch (Exception ex) {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
