using MongoDB.Driver;
using multitrabajos_history.DTOs;
using multitrabajos_history.Models;
using multitrabajos_history.Repositories;

namespace multitrabajos_history.Services
{
    public class ServiceHistory : IServiceHistory
    {
        private readonly IMongoBookDBContext _context;
        protected IMongoCollection<HistoryTransaction> _dbCollection;

        public ServiceHistory(IMongoBookDBContext context)
        {
            _context = context;
            _dbCollection = _context.GetCollection<HistoryTransaction>(typeof(HistoryTransaction).Name);
        }

        public async Task<bool> Add(HistoryTransaction historyTransaction)
        {
            //Permite guardar json en mongo
            await _dbCollection.InsertOneAsync(historyTransaction);
            return true;
        }

        public async Task<IEnumerable<HistoryResponse>> GetAll()
        {
            //Consulta a la coleccion de mongo
            var data = await _dbCollection.Find(_ => true).ToListAsync();
            var response = new List<HistoryResponse>();
            foreach (var item in data)
            {
                response.Add(new HistoryResponse()
                {
                    AccountId = item.AccountId,
                    Amount = item.Amount,
                    CreationDate = item.CreationDate,
                    IdTransaction = item.IdTransaction,
                    Type = item.Type,
                });
            }
            return response;
        }
    }
}
