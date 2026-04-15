using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppBusinessLayer.Interface
{
    public interface IUserService
    {
        Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(string userId);
    }
}