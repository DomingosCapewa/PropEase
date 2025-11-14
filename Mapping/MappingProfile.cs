using AutoMapper;
using PropEase.API.Models;
using PropEase.API.DTOs;

namespace PropEase.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Proprietario, ProprietarioDto>();
            CreateMap<CreateProprietarioDto, Proprietario>();

            CreateMap<Imovel, ImovelDto>()
                .ForMember(dest => dest.ProprietarioNome, opt => opt.MapFrom(src => src.Proprietario != null ? src.Proprietario.Nome : string.Empty))
                .ForMember(dest => dest.ProprietarioTelefone, opt => opt.MapFrom(src => src.Proprietario != null ? src.Proprietario.Telefone : string.Empty));

            CreateMap<CreateImovelDto, Imovel>().ConvertUsing<CreateImovelDtoToImovelConverter>();
        }
    }
}
