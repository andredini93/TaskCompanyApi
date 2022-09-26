using Model;
using Repository;

namespace Api.Tests.IntegrationTest.Utils
{
    public class DBUtilities
    {
        public static void InitializeDbForTestsAsync(CompanyContext context)
        {
            //context.Company.RemoveRange(context.Company);

            // Arrange

            var companyUpdate = new Company
            {
                Name = "Name companyUpdate",
                Exchange = "Exchange companyUpdate",
                ISIN = "ISIN companyUpdate",
                Ticker = "Ticker companyUpdate",
                Website = "Website companyUpdate"
            };
            context.Company.Add(companyUpdate);

            context.SaveChanges();


            var companyGetByISIN = new Company
            {
                Name = "Name companyGetByISIN",
                Exchange = "Exchange companyGetByISIN",
                ISIN = "ISIN companyGetByISIN",
                Ticker = "Ticker companyGetByISIN",
                Website = "Website companyGetByISIN"
            };
            context.Company.Add(companyGetByISIN);

            context.SaveChanges();
        }
    }
}
