using Api.ViewModels;
using Model;
using Xunit;

namespace Api.Tests.IntegrationTest.TestData
{
    internal sealed class CompanyTestData
    {
        internal sealed class CreateCompanySuccessData : TheoryData<CompanyViewModel>
        {
            public CreateCompanySuccessData()
            {
                Add(new CompanyViewModel { Name = "Company Success A", Exchange = "Exchange Success A", ISIN = "ISIN Success A", Ticker = "Ticker Success A", Website = "Website Success A" });
            }
        }

        internal sealed class CreateCompanyErrorData : TheoryData<CompanyViewModel>
        {
            public CreateCompanyErrorData()
            {
                Add(new CompanyViewModel { Name = null, Exchange = "Exchange Fail A", ISIN = "ISIN Fail A", Ticker = "Ticker Fail A", Website = "Website Fail A" });
                Add(new CompanyViewModel { Name = "Company Fail B", Exchange = null, ISIN = "ISIN Fail B", Ticker = "Ticker Fail B", Website = "Website Fail B" });
                Add(new CompanyViewModel { Name = "Company Fail C", Exchange = "Exchange Fail C", ISIN = null, Ticker = "Ticker Fail C", Website = "Website Fail C" });
                Add(new CompanyViewModel { Name = "Company Fail D", Exchange = "Exchange Fail D", ISIN = "1223fddf", Ticker = "Ticker Fail D", Website = "Website Fail D" });
                Add(new CompanyViewModel { Name = "Company Fail E", Exchange = "Exchange Fail E", ISIN = "ISIN Fail E", Ticker = null, Website = "Website Fail E" });
            }
        }

        internal sealed class UpdateCompanySuccessData : TheoryData<CompanyViewModel, long>
        {
            public UpdateCompanySuccessData()
            {
                Add(new CompanyViewModel { Id = 1, Name = "Company Update Success A", Exchange = "Exchange Update Success A", ISIN = "ISIN Update Success A", Ticker = "Ticker Update Success A", Website = "Website Update Success A" }, 1);
            }
        }

        internal sealed class UpdateCompanyErrorData : TheoryData<CompanyViewModel, long>
        {
            public UpdateCompanyErrorData()
            {
                Add(new CompanyViewModel { Id = 10, Name = "Name Fail F", Exchange = "Exchange Fail F", ISIN = "ISIN Fail F", Ticker = "Ticker Fail A", Website = "Website Fail F" }, 2);
                Add(new CompanyViewModel { Name = null, Exchange = "Exchange Fail G", ISIN = "ISIN Fail G", Ticker = "Ticker Fail G", Website = "Website Fail G" }, 2);
                Add(new CompanyViewModel { Name = "Company Fail H", Exchange = null, ISIN = "ISIN Fail H", Ticker = "Ticker Fail H", Website = "Website Fail H" }, 2);
                Add(new CompanyViewModel { Name = "Company Fail I", Exchange = "Exchange Fail I", ISIN = null, Ticker = "Ticker Fail I", Website = "Website Fail I" }, 2);
                Add(new CompanyViewModel { Name = "Company Fail J", Exchange = "Exchange Fail J", ISIN = "1223fddf", Ticker = "Ticker Fail J", Website = "Website Fail J" }, 2);
                Add(new CompanyViewModel { Name = "Company Fail K", Exchange = "Exchange Fail K", ISIN = "ISIN Success K", Ticker = null, Website = "Website Fail K" }, 2);
            }
        }
    }
}
