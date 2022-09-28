using Api.Controllers;
using Api.ViewModels;
using Application.Interfaces;
using Application.Notifier;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using NSubstitute;
using System.Collections.Generic;
using Xunit;
using static Api.Tests.IntegrationTest.TestData.CompanyTestData;

namespace Api.Tests.UnitTest
{
    public class CompanyControllerTest
    {
        public readonly ILogger<CompanyController> _logger = Substitute.For<ILogger<CompanyController>>();
        public readonly IMapper _mapper = Substitute.For<IMapper>();
        public readonly ICompanyUseCase _companyUseCase = Substitute.For<ICompanyUseCase>();
        public readonly INotifier _notifier = Substitute.For<INotifier>();
        public readonly Fixture _fix = new Fixture();

        [Fact]
        public void GetAll_Companies()
        {
            //Arrange
            _companyUseCase.GetAll().Returns(_fix.Create<List<Company>>());
            var controller = new CompanyController(_logger, _mapper, _companyUseCase, _notifier);

            //Act
            var result = controller.GetAll();

            //Assert
            _companyUseCase.Received(1).GetAll();
            result.Result.Should().BeOfType<ActionResult<IEnumerable<CompanyViewModel>>>();
        }

        [Fact]
        public void GetById_Company()
        {
            //Arrange
            _companyUseCase.GetById(1).Returns(_fix.Create<Company>());
            var controller = new CompanyController(_logger, _mapper, _companyUseCase, _notifier);

            //Act
            var result = controller.GetById(1);

            //Assert
            _companyUseCase.Received(1).GetById(1);
            result.Result.Should().BeOfType<ActionResult<CompanyViewModel>>();
        }

        [Fact]
        public void GetByISIN_Company()
        {
            //Arrange
            _companyUseCase.GetByISIN("AB123").Returns(_fix.Create<Company>());
            var controller = new CompanyController(_logger, _mapper, _companyUseCase, _notifier);

            //Act
            var result = controller.GetByISIN("AB123");

            //Assert
            _companyUseCase.Received(1).GetByISIN("AB123");
            result.Result.Should().BeOfType<ActionResult<CompanyViewModel>>();
        }

        [Theory]
        [ClassData(typeof(CreateCompanySuccessData))]
        public void Create_Company_withSuccess(CompanyViewModel company)
        {
            //Arrange
            _companyUseCase.Create(Arg.Any<Company>()).Returns(true);
            var controller = new CompanyController(_logger, _mapper, _companyUseCase, _notifier);

            //Act
            var result = controller.Create(company);

            //Assert
            _notifier.DidNotReceive().Handle(Arg.Any<Notification>());
        }

        [Theory]
        [ClassData(typeof(CreateCompanyErrorData))]
        public void Create_Company_withNoSuccess(CompanyViewModel company)
        {
            //Arrange
            _companyUseCase.Create(Arg.Any<Company>()).Returns(false);
            var controller = new CompanyController(_logger, _mapper, _companyUseCase, _notifier);

            //Act
            var result = controller.Create(company);

            //Assert
            _notifier.Received().Handle(Arg.Any<Notification>());
        }
    }
}
