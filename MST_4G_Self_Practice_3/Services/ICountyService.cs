using MST_4G_Self_Practice_3.Dtos;

namespace MST_4G_Self_Practice_3.Services;

public interface ICountyService
{
    Task<ReadCountyDto>CreateCountyAsync(CreateCountyDto createCountyDto);
    Task<ReadCountyDto>GetByCountyNoAsync(string CountyNo);
    Task<ReadCountyDto>UpdateCountyAsync(UpdateCountyDto updateCountyDto);
    Task<bool> DeleteCountyAsync(int countyId);
}