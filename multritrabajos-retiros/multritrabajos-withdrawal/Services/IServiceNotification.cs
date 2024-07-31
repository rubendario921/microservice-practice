using multritrabajos_withdrawal.DTOs;
using multritrabajos_withdrawal.Models;

namespace multritrabajos_withdrawal.Services
{
    public interface IServiceNotification
    {
        Task<bool> NotificationAccount(NotificationRequest request);
        bool Execute(WithDrawal request);
    }
}
