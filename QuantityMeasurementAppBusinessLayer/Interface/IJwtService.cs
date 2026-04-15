using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppBusinessLayer.Interface
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}