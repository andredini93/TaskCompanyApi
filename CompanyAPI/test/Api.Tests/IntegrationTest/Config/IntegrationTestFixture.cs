using Api.Authentication;
using Api.Tests.IntegrationTest.Config2;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.IntegrationTest.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Startup>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public string UserEmail;
        public string UserPassword;

        public string UserToken;

        public readonly CompanyApiFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new CompanyApiFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void GenerateUserCredentials()
        {
            var faker = new Faker("pt_BR");
            UserEmail = faker.Internet.Email().ToLower();
            UserPassword = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public async Task RequestLoginApi()
        {
            GenerateUserCredentials();
            var userData = new User
            {
                email = UserEmail,
                password = UserPassword
            };

            // Recreating the client to avoid web configurations
            Client = Factory.CreateClient();

            var response = await Client.PostAsJsonAsync("api/login", userData);
            response.EnsureSuccessStatusCode();
            UserToken = await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
