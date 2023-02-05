using AutoMapper;
using NicasourceChallenge.Core.Dtos;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Web.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Core.Entities.File, FileDto>()
                .AfterMap((src, dest, context) =>
                {
                    dest.Size = $"{decimal.Round(src.Size / 1024m)} kb";
                    dest.Created = src.Created.ToString("MM/dd/yyyy");
                    dest.Updated = src.Updated.Year == 1 ? "-" : src.Updated.ToString("MM/dd/yyyy");
                });
        }
    }
}
