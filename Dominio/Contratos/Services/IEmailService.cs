using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Contratos.Services
{
    public interface IEmailService
    {
        Task RemoveAsync(Guid id);
        Task<bool> AddAsync(Email email);
        Task<bool> AddAsync(IEnumerable<Email> email);
    }
}