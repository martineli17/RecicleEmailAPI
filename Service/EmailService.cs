using Dominio.Contratos.Repositorios;
using Dominio.Contratos.Services;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService : BaseService, IEmailService
    {
        private readonly IEmailRepositorio _repositorio;
        public EmailService(Injector injector, IEmailRepositorio repositorio) : base(injector)
        {
            _repositorio = repositorio;
        }
        public async Task<bool> AddAsync(Email email)
        {
            await _repositorio.AddAsync(email);
            return await base.Injector.UnitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Guid id) => await _repositorio.RemoveAsync(id);
        public async Task<bool> AddAsync(IEnumerable<Email> email)
        {
            await _repositorio.AddAsync(email);
            return await base.Injector.UnitOfWork.CommitAsync();
        }
    }
}