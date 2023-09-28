using DPA.Store.DOMAIN.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPA.Store.DOMAIN.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> GetByEmail(string email);

        Task<User> GetByEmailAndPassword(string email, string password); // Nuevo método para obtener un usuario por email y contraseña

        Task<bool> Insert(User user);

        Task<bool> Delete(int id);

        Task<bool> Update(User user);
    }
}
