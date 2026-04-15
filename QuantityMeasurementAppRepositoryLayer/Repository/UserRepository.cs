using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppRepositoryLayer.Interface;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppRepositoryLayer.Context;
namespace QuantityMeasurementAppRepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveUser(User user)
        {
            try
            {

                _context.Users.Add(user);

                int rowsAffected = await _context.SaveChangesAsync();

                Console.WriteLine("Rows: " + rowsAffected);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("INNER ERROR: " + ex.InnerException.Message);
                }

                return false;
            }
        }

        public async Task<User?> VerifyUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(int userId)
        {
            return await _context.QuantityMeasurementHistories.Where(h => h.UserId == userId).ToListAsync();
        }
    }
}

