using Microsoft.AspNetCore.Mvc;
using SCP.Common.Models;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Services;
using System.Net;

namespace SCP.Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var result = await _transactionService.GetTransaction(id);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpGet("all/{sessionId}")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetTransactionsForSession(Guid sessionId)
        {
            var result = await _transactionService.GetTransactionsForSession(sessionId);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("start")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> StartTransaction([FromBody] StartTransactionModel startTaModel)
        {
            var result = await _transactionService.StartTransaction(startTaModel.Sessionid);
            return Ok(result);
        }

        [HttpPost("finish/{id}")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FinishTransaction(Guid id)
        {
            var result = await _transactionService.FinishTransaction(id);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("article/{id}")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddArticles(Guid id, [FromBody] List<ArticleDataModel> articles)
        {
            var result = await _transactionService.AddArticles(id, articles);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("payments/{id}")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddPayments(Guid id, [FromBody] List<PaymentModel> payments)
        {
            var result = await _transactionService.AddPayments(id, payments);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("total/{id}")]
        [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Total(Guid id)
        {
            var result = await _transactionService.Total(id);
            return result != null ? Ok(result) : NotFound(null);
        }
    }
}
