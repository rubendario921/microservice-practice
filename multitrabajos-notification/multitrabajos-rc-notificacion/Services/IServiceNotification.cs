using multitrabajos_rc_notificacion.Models;

namespace multitrabajos_rc_notificacion.Services
{
    public interface IServiceNotification
    {
        Task<IEnumerable<Models.Notification>> GetAll();
        Task<Models.Notification> GetbyID(int id);
        Task<Notification> Notification(Models.Notification notification);
    }
}
