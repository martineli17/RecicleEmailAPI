using Aplicacao.DTO;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao.Mappers
{
    public class EmailMapper : Profile
    {
        public EmailMapper()
        {
            CreateMap<EmailDTO, Email>();
        }
    }
}