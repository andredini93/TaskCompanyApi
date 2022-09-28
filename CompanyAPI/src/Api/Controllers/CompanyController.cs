using Api.ViewModels;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : MainController
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyUseCase _companyUseCase;
        private readonly IMapper _mapper;

        /// <summary>
        /// Company Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="companyUseCase"></param>
        /// <param name="notifier"></param>
        public CompanyController(ILogger<CompanyController> logger,
                IMapper mapper,
                ICompanyUseCase companyUseCase,
                INotifier notifier) : base(notifier)
        {
            _logger = logger;
            _companyUseCase = companyUseCase;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve a collection of all Companies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyViewModel>>> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<CompanyViewModel>>(await _companyUseCase.GetAll()));
        }

        /// <summary>
        /// Retrieve an existing Company by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id:long}")]
        public async Task<ActionResult<CompanyViewModel>> GetById([FromRoute] long id)
        {
            var company = _mapper.Map<CompanyViewModel>(await _companyUseCase.GetById(id));

            if (company == null) return NotFound();

            return CustomResponse(company);
        }

        /// <summary>
        /// Retrieve an existing Company by ISIN
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        [HttpGet("GetByISIN/{isin}")]
        public async Task<ActionResult<CompanyViewModel>> GetByISIN([FromRoute] string isin)
        {
            var company = _mapper.Map<CompanyViewModel>(await _companyUseCase.GetByISIN(isin));

            if (company == null) return NotFound();

            return CustomResponse(company);
        }

        /// <summary>
        /// Creates a Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CompanyViewModel>> Create([FromBody] CompanyViewModel company)
        {
            if (!company.CheckISIN() || !company.CheckObrigatoryFields())
            {
                NotifyError("Validation inconsistence. Please verify the fields and try again.");
                return CustomResponse(company);
            }

            await _companyUseCase.Create(_mapper.Map<Company>(company));

            return CustomResponse(company);
        }

        /// <summary>
        /// Update a Company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CompanyViewModel company)
        {
            if (id != company.Id)
            {
                NotifyError("The Id provided is not the same as the one passed in the query");
                return CustomResponse(company);
            }

            if (!company.CheckISIN() || !company.CheckObrigatoryFields())
            {
                NotifyError("Validation inconsistence. Please verify the fields and try again.");
                return CustomResponse(company);
            }

            await _companyUseCase.Update(_mapper.Map<Company>(company));

            return CustomResponse(company);
        }
    }
}