using Microsoft.EntityFrameworkCore;
using multitrabajos_rc_notificacion.Models;
using multitrabajos_rc_notificacion.Repositories;


namespace multitrabajos_rc_notificacion.Services
{
    public class ServiceNotification : IServiceNotification
    {
        private readonly ContextDatabase _contextDatabase;
        public ServiceNotification(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }
        public async Task<IEnumerable<Notification>> GetAll()
        {
            try
            {
                var result = await _contextDatabase.Notification.ToListAsync();
                if (result == null)
                {
                    throw new Exception("No hay informacion registradas");
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<Notification> GetbyID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"El valor de id:{id} es incorrecto.");
                }
                else
                {
                    var result = await _contextDatabase.Notification.Where(x => x.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception($"No existen datos con la id:{id} ingresada.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<Notification> Notification(Notification notification)
        {
            try
            {
                _contextDatabase.Notification.Add(notification);
                await _contextDatabase.SaveChangesAsync();
                return notification;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en registrar la notificacion {ex.Message}");
                throw;
            }
        }
    }
}