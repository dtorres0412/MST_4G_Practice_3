using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MST_4G_Self_Practice_3.Data;
using MST_4G_Self_Practice_3.Dtos;
using MST_4G_Self_Practice_3.Models;

namespace MST_4G_Self_Practice_3.Services;

public class CountyService(AppDbContext context) : ICountyService
{
    public async Task<ReadCountyDto?>GetByCountyNoAsync(string countyNo)
    {
        return await context.County
            .AsNoTracking()
            .Where(c => c.CountyNo == countyNo.Trim())
            .Select(c => new ReadCountyDto
            {
                CountyNo = c.CountyNo,
                CountyName = c.CountyName
            })
            .FirstOrDefaultAsync();
    }
    public async Task<ReadCountyDto>CreateCountyAsync(CreateCountyDto createCountyDto)
    {
        string countyNo = createCountyDto.CountyNo.Trim();
        string countyName = createCountyDto.CountyName.Trim();

        var existingCounty = await context.County
            .FirstOrDefaultAsync(c => c.CountyNo == countyNo);

        if(existingCounty != null)
        {
            throw new ArgumentException($"County number '{countyNo}' already exists.");
        }

        var newCounty = new County
        {
            CountyNo = countyNo,
            CountyName = countyName
        };

        context.County.Add(newCounty);
        await context.SaveChangesAsync();

        return new ReadCountyDto
        {
            CountyId = newCounty.CountyId,
            CountyNo = newCounty.CountyNo,
            CountyName = newCounty.CountyName
        };
    }
}