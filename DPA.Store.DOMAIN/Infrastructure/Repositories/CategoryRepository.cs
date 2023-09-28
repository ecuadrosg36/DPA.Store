using DPA.Store.DOMAIN.Core.Entities;
using DPA.Store.DOMAIN.Core.Interfaces;
using DPA.Store.DOMAIN.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DPA.Store.DOMAIN.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDbContext _dbContext;

        public UserRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext
                .User
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext
                .User
                .Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            // NOTA: ¡Esto es solo un ejemplo simplificado!
            // Debes hashear y verificar las contraseñas de forma segura.
            return await _dbContext
                .User
                .Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(User user)
        {
            await _dbContext.User.AddAsync(user);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(User user)
        {
            _dbContext.User.Update(user);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _dbContext
                .User
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return false;

            _dbContext.User.Remove(user);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
