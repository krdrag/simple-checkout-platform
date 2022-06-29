using Microsoft.AspNetCore.Mvc;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Services;

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
        // TODO: add generate response type
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var result = await _transactionService.GetTransaction(id);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("start")]
        // TODO: add generate response type
        public async Task<IActionResult> StartTransaction([FromBody] WorkstationDataModel wsModel)
        {
            var result = await _transactionService.StartTransaction(wsModel);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("finish/{id}")]
        // TODO: add generate response type
        public async Task<IActionResult> FinishTransaction(Guid id)
        {
            var result = await _transactionService.FinishTransaction(id);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("article/{id}")]
        // TODO: add generate response type
        public async Task<IActionResult> AddArticles(Guid id, [FromBody] List<ArticleDataModel> articles)
        {
            var result = await _transactionService.AddArticles(id, articles);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("payments/{id}")]
        // TODO: add generate response type
        public async Task<IActionResult> AddPayments(Guid id, [FromBody] List<PaymentModel> payments)
        {
            var result = await _transactionService.AddPayments(id, payments);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("total/{id}")]
        // TODO: add generate response type
        public async Task<IActionResult> Total(Guid id)
        {
            var result = await _transactionService.Total(id);
            return result != null ? Ok(result) : NotFound(null);
        }
    }
}
