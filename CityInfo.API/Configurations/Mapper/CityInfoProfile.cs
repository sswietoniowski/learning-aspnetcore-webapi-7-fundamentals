using AutoMapper;
using CityInfo.API.DataAccess.Entities;
using CityInfo.API.DTOs;

namespace CityInfo.API.Configurations.Mapper
{
    public class CityInfoProfile : Profile
    {
        public CityInfoProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<PointOfInterestDto, PointOfInterest>().ReverseMap();
            CreateMap<PointOfInterestForCreationDto, PointOfInterest>().ReverseMap();
            CreateMap<PointOfInterestForUpdateDto, PointOfInterest>().ReverseMap();
        }
    }
}
