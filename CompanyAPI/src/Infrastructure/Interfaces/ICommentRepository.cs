using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(long id);
        Task<Company> GetByISIN(string isin);
        Task Create(Company company);
        Task Update(Company company);
    }
}
