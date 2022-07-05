using Newtonsoft.Json;
using SCP.Transaction.Domain.Interfaces;
using StackExchange.Redis;

namespace SCP.Transaction.Infrastructure.Repositories
{
    public class TransactionListCacheRepository : ITransactionListCacheRepository
    {
        private const string CacheKey = "TransactionLists";

        private readonly IConnectionMultiplexer _redis;

        public TransactionListCacheRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<IEnumerable<string>> GetAllTransactionsForSession(Guid sessionId)
        {
            var db = _redis.GetDatabase();

            var result = await db.SetMembersAsync($"{CacheKey}:{sessionId}");

            if (result == null)
                return Enumerable.Empty<string>();

            return result.Select(x => x.ToString());
        }

        public async Task RegisterTransaction(Guid sessionId, Guid transactionId)
        {
            var db = _redis.GetDatabase();

            await db.SetAddAsync($"{CacheKey}:{sessionId}", transactionId.ToString());
        }

        public async Task RemoveTransaction(Guid sessionId, Guid transactionId)
        {
            var db = _redis.GetDatabase();

            await db.SetRemoveAsync($"{CacheKey}:{sessionId}", transactionId.ToString());
        }

        public async Task ClearTransactionList(Guid sessionId)
        {
            var db = _redis.GetDatabase();

            var transactions = await GetAllTransactionsForSession(sessionId);

            foreach (var transaction in transactions)
            {
                await db.SetRemoveAsync($"{CacheKey}:{sessionId}", transaction);
            }
        }
    }
}
