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

        [HttpPost]
        // TODO: add generate response type
        public async Task<TransactionModel> StartTransaction([FromBody] WorkstationDataModel wsModel)
        {
            return await _transactionService.StartTransaction(wsModel);
        }
    }
}
