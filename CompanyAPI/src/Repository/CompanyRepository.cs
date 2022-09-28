using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        protected readonly CompanyContext Db;

        public CompanyRepository(CompanyContext db)
        {
            Db = db;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await Db.Set<Company>().ToListAsync();
        }

        public async Task<Company> GetById(long id)
        {
            return await Db.Set<Company>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Company> GetByISIN(string isin)
        {
            return await Db.Set<Company>().Where(x => x.ISIN == isin).FirstOrDefaultAsync();
        }

        public async Task Create(Company company)
        {
            Db.Add<Company>(company);
            await SaveChanges();
        }

        public async Task Update(Company company)
        {
            Db.Update<Company>(company);
            await SaveChanges();
        }

        public async Task Delete(int id)
        {
            Db.Set<Company>().Remove(await GetById(id));
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}