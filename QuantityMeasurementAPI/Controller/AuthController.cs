using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppModelLayer.DTO;

namespace QuantityMeasurementAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        
        try{await _authService.Register(registerDTO);
        return Ok(new {message="User Registered Successfully"});}
        catch(Exception ex)
        {
            return StatusCode(500,new {mesage=ex.Message,
            inner=ex.InnerException?.Message});
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try{var token = await _authService.Login(loginDTO);

        return Ok(new
        {
            token = token 
        });}
        catch(Exception ex)
        {
            return StatusCode(500,new {mesage=ex.Message,
            inner=ex.InnerException?.Message});
        }
    }
}