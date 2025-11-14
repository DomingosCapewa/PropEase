using AutoMapper;
using PropEase.API.DTOs;
using PropEase.API.Models;

namespace PropEase.API.Mapping
{
    public class CreateImovelDtoToImovelConverter : ITypeConverter<CreateImovelDto, Imovel>
    {
        public Imovel? Convert(CreateImovelDto source, Imovel? destination, ResolutionContext context)
        {
            if (source is null) return null;

            Imovel? imovel = source.Tipo?.Trim().ToLowerInvariant() switch
            {
                "casa" => new Casa(),
                "apartamento" => new Apartamento(),
                _ => null
            };

            if (imovel is null) return null;

            imovel.Apply(source.ProprietarioId, source.Endereco, source.Numero, source.Alugado);
            return imovel;
        }
    }
}
