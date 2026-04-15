using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppRepositoryLayer.Interface;

public interface IUserRepository 
{
    public Task<bool> SaveUser(User user); 
    public Task<User?> VerifyUser(string email); 
    public Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(int userId);
}