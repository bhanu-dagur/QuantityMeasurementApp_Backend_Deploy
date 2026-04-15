using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppRepositoryLayer.Interface
{
    public interface IMeasurementRepository
    {
        public Task<bool> SaveHistory(QuantityMeasurementHistoryEntity historyEntity);
    }
}