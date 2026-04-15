using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppModelLayer.DTO;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppBusinessLayer.Interface;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;


namespace QuantityMeasurementAPI.Controller;

[Route("api/")]
[ApiController]
public class QuantityMangementController : ControllerBase
{
    private readonly IMeasurementService _measurementService;
    public QuantityMangementController(IMeasurementService measurementService)
    {
        this._measurementService = measurementService;
    }
    [HttpPost("Conversion/{toUnit}")]
     public async Task<IActionResult> QuantityMeasurementConversion([FromBody]  QuantityDTO newProduct,[FromRoute] string toUnit)
    {
        string? userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        QuantityDTO result = await _measurementService.PerformConversion(newProduct, toUnit.ToUpper(), userId??"");
        return Ok(new
        {
            resultValue=result.Value,
            resultUnit=result.Unit  
        });
    }

    [HttpPost("addition")]
    public async Task<IActionResult> QuantityMeasurementAddition([FromBody]  AirthmeticDTO newEntity,[FromQuery] string toUnit)
    {
        string ? userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        QuantityDTO result = await _measurementService.PerformAddition(newEntity.Quantity1, newEntity.Quantity2, toUnit.ToUpper(), userId);
        return Ok(new
        {
            resultValue = result.Value,
            resultUnit = result.Unit
        });
        
    }

    [HttpPost("Subtraction")]
    public async Task<IActionResult> QuantityMeasurementSubtraction([FromBody] AirthmeticDTO newEntity, [FromQuery] string toUnit)
    {
        string? userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        QuantityDTO result = await _measurementService.PerformSubtraction(newEntity.Quantity1, newEntity.Quantity2, toUnit.ToUpper(),userId);
        return Ok(new
        {
            resultValue = result.Value,
            resultUnit =  result.Unit 
        });
    }

    [HttpPost("Division")]
    public async Task<IActionResult> QuantityMeasurementDivision([FromBody] AirthmeticDTO newEntity, [FromQuery] string toUnit)
    {
        string? userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        QuantityDTO result = await _measurementService.PerformDivision(newEntity.Quantity1, newEntity.Quantity2, toUnit.ToUpper(),userId);
        return Ok(new
        {
            resultValue = result.Value,
            resultUnit = result.Unit
        }); 
    
    }

    [HttpPost ("CheckEquaity")]
    public async Task<IActionResult> CheckEquaity([FromBody] AirthmeticDTO newEntity)
    {
        string? userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _measurementService.CheckEquality(newEntity.Quantity1, newEntity.Quantity2,userId);
        return Ok(new
        {
            result=result
        });
    }
    
}