using Application.Interfaces;
using Application.Notifier;
using Model;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CompanyUseCase : BaseService, ICompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyUseCase(ICompanyRepository companyRepository, INotifier notifier)
            : base(notifier)
        {
            _companyRepository = companyRepository;
        }

        public async Task<bool> Create(Company company)
        {
            var companyISIN = await GetByISIN(company.ISIN);
            if (companyISIN != null)
            {
                Notifier("Not Allowed to complete the request. The ISIN informed belongs to another company.");
                return false;
            }

            await _companyRepository.Create(company);
            return true;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }

        public async Task<Company> GetById(long id)
        {
            return await _companyRepository.GetById(id);
        }

        public async Task<Company> GetByISIN(string isin)
        {
            return await _companyRepository.GetByISIN(isin);
        }

        public async Task Update(Company company)
        {
            await _companyRepository.Update(company);
        }
    }
}
