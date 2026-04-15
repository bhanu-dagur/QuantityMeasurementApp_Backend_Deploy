using QuantityMeasurementAppModelLayer.Entity;
namespace QuantityMeasurementAppRepositoryLayer.Interface
{
    public interface IMeasurementHistoryRepository
    {
        public void SaveHistory(QuantityMeasurementHistoryEntity history);
        public void SaveUser(User user);
        public User VerifyUser(string email);
    }
}