using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppBusinessLayer.Interface;
using System.Security.Claims;

namespace QuantityMeasurementAPI.Controller;

[ApiController]
[Route("api/User")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var historyResult= await _userService.GetHistory(userId??"");
        return Ok(new
        {
            history=historyResult
        });
    }

}