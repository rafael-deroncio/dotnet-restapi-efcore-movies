using AutoMapper;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Domain.Requests;

namespace MovieMania.Core.Configurations.Mapper.Profiles;

public class EntityToRequestProfile : Profile
{
    public EntityToRequestProfile()
    {
        CreateMap<CountryEntity, CountryRequest>().ReverseMap();
        CreateMap<DepartmentEntity, DepartmentRequest>().ReverseMap();
        CreateMap<GenderEntity, GenderRequest>().ReverseMap();
        CreateMap<GenreEntity, GenreRequest>().ReverseMap();
        CreateMap<KeywordEntity, KeywordRequest>().ReverseMap();
        CreateMap<LanguageEntity, LanguageRequest>().ReverseMap();
        CreateMap<LanguageRoleEntity, LanguageRoleRequest>().ReverseMap();
        CreateMap<ProductionCompanyEntity, ProductionCompanyRequest>().ReverseMap();
    }
}