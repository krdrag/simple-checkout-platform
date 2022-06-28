using Microsoft.AspNetCore.Mvc;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Services;

namespace SCP.Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        // TODO: add generate response type
        public async Task<TransactionModel> GetTransaction(Guid id)
        {
            return await _transactionService.GetTransaction(id);
        }

        [HttpPost("start")]
        // TODO: add generate response type
        public async Task<TransactionModel> StartTransaction([FromBody] WorkstationDataModel wsModel)
        {
            return await _transactionService.StartTransaction(wsModel);
        }

        [HttpPost("finish/{id}")]
        // TODO: add generate response type
        public async Task<TransactionModel> FinishTransaction(Guid id)
        {
            return await _transactionService.FinishTransaction(id);
        }

        [HttpPost("article/{id}")]
        // TODO: add generate response type
        public async Task<TransactionModel> AddArticles(Guid id, [FromBody] List<ArticleDataModel> articles)
        {
            return await _transactionService.AddArticles(id, articles);
        }

        [HttpPost("payments/{id}")]
        // TODO: add generate response type
        public async Task<TransactionModel> AddPayments(Guid id, [FromBody] List<PaymentModel> payments)
        {
            return await _transactionService.AddPayments(id, payments);
        }

        [HttpPost("total/{id}")]
        // TODO: add generate response type
        public async Task<TransactionModel> Total(Guid id)
        {
            return await _transactionService.Total(id);
        }
    }
}
