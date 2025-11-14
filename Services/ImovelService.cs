using Microsoft.EntityFrameworkCore;
using PropEase.API.Models;
using PropEase.API.Repositories;

namespace PropEase.API.Services
{
    public class ImovelService
    {
        private readonly IUnitOfWork _uow;

        public ImovelService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Imovel>> GetAllAsync()
        {
            return await _uow.Repository<Imovel>().Query().Include(i => i.Proprietario).ToListAsync();
        }

        public async Task<Imovel?> GetByIdAsync(int id)
        {
            return await _uow.Repository<Imovel>().Query().Include(i => i.Proprietario).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Imovel> AddAsync(Imovel imovel)
        {
            await _uow.Repository<Imovel>().AddAsync(imovel);
            await _uow.SaveChangesAsync();
            return imovel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var imovel = await _uow.Repository<Imovel>().GetByIdAsync(id);
            if (imovel == null) return false;

            _uow.Repository<Imovel>().Remove(imovel);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AlugarAsync(int id)
        {
            var imovel = await _uow.Repository<Imovel>().GetByIdAsync(id);
            if (imovel == null) return false;

            imovel.Alugar();
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisponibilizarAsync(int id)
        {
            var imovel = await _uow.Repository<Imovel>().GetByIdAsync(id);
            if (imovel == null) return false;

            imovel.Disponibilizar();
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
