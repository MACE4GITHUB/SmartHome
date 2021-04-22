using AutoMapper;
using SmartHome.Configuration.Abstractions.Models;
using SmartHome.Configuration.Infrastructure.Models;

namespace SmartHome.Configuration.Api.Profiles
{
    public class SensorProfile : Profile
    {
        public SensorProfile()
        {
            CreateMap<SensorDb, Sensor>()
                .ForMember(d => d.MinValue,
                    o => o.MapFrom(s => s.SensorType.MinValue))
                .ForMember(d => d.MinNormalValue,
                    o => o.MapFrom(s => s.SensorType.MinNormalValue))
                .ForMember(d => d.MaxValue,
                    o => o.MapFrom(s => s.SensorType.MaxValue))
                .ForMember(d => d.MaxNormalValue,
                    o => o.MapFrom(s => s.SensorType.MaxNormalValue));
        }
    }
}
