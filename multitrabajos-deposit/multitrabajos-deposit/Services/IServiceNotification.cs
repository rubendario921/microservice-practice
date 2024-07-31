using multitrabajos_deposit.DTOs;
using multitrabajos_deposit.Models;

namespace multitrabajos_deposit.Services
{
    public interface IServiceNotification
    {
        Task<bool> NotificationAccount(NotificationRequest request);
        bool Execute(Transaction request);
    }
}
