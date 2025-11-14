using Microsoft.EntityFrameworkCore;
using PropEase.API.Models;
using PropEase.API.Repositories;

namespace PropEase.API.Services
{
    public class ProprietarioService
    {
        private readonly IUnitOfWork _uow;
        public ProprietarioService(IUnitOfWork uow) { _uow = uow; }

        public async Task<List<Proprietario>> GetAllAsync() => await _uow.Repository<Proprietario>().Query().ToListAsync();
        public async Task<Proprietario?> GetByIdAsync(int id) => await _uow.Repository<Proprietario>().GetByIdAsync(id);
        public async Task<Proprietario> AddAsync(Proprietario p)
        {
            await _uow.Repository<Proprietario>().AddAsync(p);
            await _uow.SaveChangesAsync();
            return p;
        }
        public async Task<bool> UpdateAsync(int id, Proprietario p)
        {
            var existing = await _uow.Repository<Proprietario>().GetByIdAsync(id);
            if (existing is null) return false;
            existing.Nome = p.Nome;
            existing.Telefone = p.Telefone;
            existing.CPF = p.CPF;
            await _uow.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _uow.Repository<Proprietario>().GetByIdAsync(id);
            if (p is null) return false;
            _uow.Repository<Proprietario>().Remove(p);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
