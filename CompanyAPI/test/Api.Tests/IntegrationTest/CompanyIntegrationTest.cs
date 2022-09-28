using Api.Tests.IntegrationTest.Config;
using Api.Tests.IntegrationTest.Config2;
using Api.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Api.Tests.IntegrationTest.TestData.CompanyTestData;

namespace Api.Tests.IntegrationTest
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class CompanyIntegrationTest : IClassFixture<CompanyApiFactory<Startup>>
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public CompanyIntegrationTest(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Get All Companies")]
        public async Task GetAllCompanies_ShouldReturnWithSuccess()
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            //Act
            var response = await _testsFixture.Client.GetAsync("api/company");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var CompaniesResponse = JsonConvert.DeserializeObject<IEnumerable<CompanyViewModel>>(GetResultFromJson(responseString));
            Assert.True(CompaniesResponse.Count() > 0);
        }


        [Fact(DisplayName = "Get Company By Id with Success")]
        public async Task GetCompanyById_ShouldReturnWithSuccess()
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            //Act
            var response = await _testsFixture.Client.GetAsync("api/company/GetById/1");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var CompaniesResponse = JsonConvert.DeserializeObject<CompanyViewModel>(GetResultFromJson(responseString));
            Assert.True(CompaniesResponse.Id == 1);
        }


        [Fact(DisplayName = "Get Company By Id with no Success")]
        public async Task GetCompanyById_ShouldReturnWithNoSuccess()
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            //Act
            var response = await _testsFixture.Client.GetAsync("api/company/GetById/200");

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, (int)response.StatusCode);
        }


        [Fact(DisplayName = "Get Company By ISIN with Success")]
        public async Task GetCompanyByISIN_ShouldReturnWithSuccess()
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            //Act
            var response = await _testsFixture.Client.GetAsync("api/company/GetByISIN/ISIN companyGetByISIN");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var CompaniesResponse = JsonConvert.DeserializeObject<CompanyViewModel>(GetResultFromJson(responseString));
            Assert.True(CompaniesResponse.Name == "Name companyGetByISIN");
        }


        [Fact(DisplayName = "Get Company By ISIN with no Success")]
        public async Task GetCompanyByISIN_ShouldReturnWithNoSuccess()
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            //Act
            var response = await _testsFixture.Client.GetAsync("api/company/GetByISIN/ISINNOTFOUND");

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, (int)response.StatusCode);
        }


        [Theory(DisplayName = "Create Company with Success")]
        [ClassData(typeof(CreateCompanySuccessData))]
        public async Task CreateCompany_ShouldReturnWithSuccess(CompanyViewModel company)
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/company")
            {
                Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await _testsFixture.Client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var CompaniesResponse = JsonConvert.DeserializeObject<CompanyViewModel>(GetResultFromJson(responseString));
            Assert.True(CompaniesResponse.Name == company.Name);
        }


        [Theory(DisplayName = "Create Company with No Success")]
        [ClassData(typeof(CreateCompanyErrorData))]
        public async Task CreateCompany_ShouldReturnWithNoSuccess(CompanyViewModel company)
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/company")
            {
                Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await _testsFixture.Client.SendAsync(request);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
        }


        [Theory(DisplayName = "Update Company with Success")]
        [ClassData(typeof(UpdateCompanySuccessData))]
        public async Task UpdateCompany_ShouldReturnWithSuccess(CompanyViewModel company, long id)
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            var request = new HttpRequestMessage(HttpMethod.Put, "api/company/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await _testsFixture.Client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
        }


        [Theory(DisplayName = "Update Company with No Success")]
        [ClassData(typeof(UpdateCompanyErrorData))]
        public async Task UpdateCompany_ShouldReturnWithNoSuccess(CompanyViewModel company, long id)
        {
            //Arrange
            await _testsFixture.RequestLoginApi();
            _testsFixture.Client.AssignToken(_testsFixture.UserToken);

            var request = new HttpRequestMessage(HttpMethod.Put, "api/company/" + id)
            {
                Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await _testsFixture.Client.SendAsync(request);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)response.StatusCode);
        }


        private string GetResultFromJson(string responseString)
        {
            return JObject.Parse(responseString)["data"].ToString();
        }
    }
}
