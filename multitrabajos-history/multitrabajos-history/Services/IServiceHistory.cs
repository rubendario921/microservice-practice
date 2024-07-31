using multitrabajos_history.DTOs;
using multitrabajos_history.Models;

namespace multitrabajos_history.Services
{
    public interface IServiceHistory
    {
        Task<IEnumerable<HistoryResponse>> GetAll();
        Task<bool> Add(HistoryTransaction historyTransaction);
    }
}
