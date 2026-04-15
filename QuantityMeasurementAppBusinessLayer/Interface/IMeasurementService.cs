using QuantityMeasurementAppModelLayer.Core;
using QuantityMeasurementAppModelLayer.DTO;

namespace QuantityMeasurementAppBusinessLayer.Interface
{
    public interface IMeasurementService
    {
        Task<QuantityDTO> PerformAddition(QuantityDTO q1, QuantityDTO q2, string targetUnit,string userId);
        Task<QuantityDTO> PerformConversion(QuantityDTO q ,string toUnit,string userId);
        Task<QuantityDTO> PerformSubtraction(QuantityDTO q1, QuantityDTO q2, string targetUnit,string userId);
        Task<QuantityDTO> PerformDivision(QuantityDTO q1,QuantityDTO q2, string targetUnit,string userId);
        Task <bool> CheckEquality(QuantityDTO q1, QuantityDTO q2,string UserId);
    }
}
    