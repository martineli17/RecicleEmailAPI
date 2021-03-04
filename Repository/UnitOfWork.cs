using Dominio.Contratos.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextEmail _context;
        public UnitOfWork(ContextEmail context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var changes = await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    return false;
                }
                finally
                {
                    //usar em banco de dados relacional e que nao seja em memoria
                    //_contextEntity.Database.GetDbConnection().Close();
                }
            }
        }
    }
}
