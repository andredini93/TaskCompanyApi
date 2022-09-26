using Application.UseCase;
using AutoFixture;
using Model;
using Model.Interfaces;
using NSubstitute;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.UnitTest
{
    public class CompanyUseCaseTest
    {
        public readonly INotifier _notifier = Substitute.For<INotifier>();
        public readonly ICompanyRepository _companyRepository = Substitute.For<ICompanyRepository>();
        public readonly Fixture _fix = new Fixture();

        [Fact]
        public void Create_Company_WithSuccess()
        {
            //Arrange
            var company = new Company() { ISIN = "AB123" };
            _companyRepository.Create(company).Returns(Task.FromResult(true));
            //_companyRepository.GetByISIN(company.ISIN).Returns(null);
            var companyUseCase = new CompanyUseCase(_companyRepository, _notifier);

            //Act
            var result = companyUseCase.Create(company);

            //Assert
            _companyRepository.Received(1).Create(Arg.Any<Company>());
            _companyRepository.Received(1).GetByISIN(Arg.Any<string>());
            Assert.True(result.Result);
            _notifier.DidNotReceive().Handle(Arg.Any<Notification>());
        }

        [Fact]
        public void Create_Company_WithNoSuccess()
        {
            //Arrange
            var companyISIN = new Company() { ISIN = "AB123" };
            _companyRepository.GetByISIN(companyISIN.ISIN).Returns(companyISIN);
            var companyUseCase = new CompanyUseCase(_companyRepository, _notifier);

            //Act
            var result = companyUseCase.Create(companyISIN);

            //Assert
            _companyRepository.DidNotReceive().Create(Arg.Any<Company>());
            _companyRepository.Received(1).GetByISIN(Arg.Any<string>());
            Assert.False(result.Result);
            _notifier.Received(1).Handle(Arg.Any<Notification>());
        }

        [Fact]
        public void GetAll_Company()
        {
            //Arrange
            _companyRepository.GetAll().Returns(new List<Company>());
            var companyUseCase = new CompanyUseCase(_companyRepository, _notifier);

            //Act
            var result = companyUseCase.GetAll();

            //Assert
            _companyRepository.Received(1).GetAll();
        }

        [Fact]
        public void GetById_Company()
        {
            //Arrange
            _companyRepository.GetById(Arg.Any<long>()).Returns(new Company());
            var companyUseCase = new CompanyUseCase(_companyRepository, _notifier);

            //Act
            var result = companyUseCase.GetById(1111);

            //Assert
            _companyRepository.Received(1).GetById(Arg.Any<long>());
        }

        [Fact]
        public void GetByISIN_Company()
        {
            //Arrange
            _companyRepository.GetByISIN(Arg.Any<string>()).Returns(new Company());
            var companyUseCase = new CompanyUseCase(_companyRepository, _notifier);

            //Act
            var result = companyUseCase.GetByISIN("AB123");

            //Assert
            _companyRepository.Received(1).GetByISIN(Arg.Any<string>());
        }

        [Fact]
        public void Update_Company()
        {
            //Arrange
            _companyRepository.Update(Arg.Any<Company>()).Returns(Task.CompletedTask);
            var companyUseCase = new CompanyUseCase(_companyRepository, _notifier);

            //Act
            var result = companyUseCase.Update(new Company());

            //Assert
            _companyRepository.Received(1).Update(Arg.Any<Company>());
        }
    }
}
