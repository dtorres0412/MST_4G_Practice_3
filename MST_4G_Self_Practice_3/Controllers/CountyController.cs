using Microsoft.AspNetCore.Mvc;
using MST_4G_Self_Practice_3.Services;
using MST_4G_Self_Practice_3.Dtos;

namespace MST_4G_Self_Practice_3.Controllers;

[ApiController]
[Route("[controller]")]
public class CountyController(ICountyService countyService) : ControllerBase
{
  [HttpGet("{countyNo}")]
  public async Task<ActionResult<ReadCountyDto>>GetByCountyNo(string countyNo)
    {
        var County = await countyService.GetByCountyNoAsync(countyNo);

        if (County == null)
            return NotFound($"CountyNo record of '{countyNo}' not found.");

        return Ok(County);
    }
  [HttpPost("create")]
  public async Task<ActionResult<ReadCountyDto>>CreateCounty([FromBody] CreateCountyDto createCountyDto)
    {
        try
        {
            var result = await countyService.CreateCountyAsync(createCountyDto);

            return CreatedAtAction(nameof(GetByCountyNo), new {countyNo = result?.CountyNo}, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<ReadCountyDto>>UpdateCountyAsync([FromBody] UpdateCountyDto updateCountyDto)
    {
        try
        {
            var result = await countyService.UpdateCountyAsync(updateCountyDto);

            if(result == null)
                return NotFound($"County record number '{updateCountyDto.CountyNo}' is not found.");
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
