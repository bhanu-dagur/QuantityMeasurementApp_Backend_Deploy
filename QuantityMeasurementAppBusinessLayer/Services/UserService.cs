using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppRepositoryLayer.Interface;

namespace QuantityMeasurementAppBusinessLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(string userId)
    {
        int id=int.Parse(userId);
        return await _userRepository.GetHistory (id);
    }
}