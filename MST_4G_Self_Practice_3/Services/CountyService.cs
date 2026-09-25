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

    public async Task<ReadCountyDto>UpdateCountyAsync(UpdateCountyDto updateCountyDto)
    {
        try
        {
            var existingCounty = await context.County
                .FirstOrDefaultAsync(c => c.CountyId == updateCountyDto.CountyId);

            if (existingCounty == null)
            {
                return null;
            }

            existingCounty.CountyNo = updateCountyDto.CountyNo.Trim();
            existingCounty.CountyName = updateCountyDto.CountyName.Trim();

            await context.SaveChangesAsync();

            return new ReadCountyDto
            {
                CountyId = existingCounty.CountyId,
                CountyNo = existingCounty.CountyNo,
                CountyName = existingCounty.CountyName
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in UpdateCountyAsync: " + ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteCountyAsync(int countyId)
    {
        var countyRecord = await context.County.FindAsync(countyId);

        if (countyRecord == null)
        {
            return false;
        }

        context.County.Remove(countyRecord);
        await context.SaveChangesAsync();

        return true;
    }
}