using QuantityMeasurementAppModelLayer.DTO;

namespace QuantityMeasurementAppBusinessLayer.Interface
{
    public interface  IAuthService
    {
        Task<bool> Register(RegisterDTO registerDTO);    
        Task<string> Login(LoginDTO loginDTO);
    }
}