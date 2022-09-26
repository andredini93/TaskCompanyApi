using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICompanyUseCase
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(long id);
        Task<Company> GetByISIN(string isin);
        Task<bool> Create(Company company);
        Task Update(Company company);
    }
}
