using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppRepositoryLayer.Context;
using QuantityMeasurementAppModelLayer.Util;
using QuantityMeasurementAppRepositoryLayer.Interface;

namespace QuantityMeasurementAppRepositoryLayer.Repository;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly AppDbContext _dbContext;
    public MeasurementRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> SaveHistory(QuantityMeasurementHistoryEntity historyEntity)
    {
        _dbContext.QuantityMeasurementHistories.Add(historyEntity);
        int rowsAffected = await _dbContext.SaveChangesAsync() ;

        return rowsAffected > 0;
    }
}