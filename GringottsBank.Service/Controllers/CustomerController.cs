using AutoMapper;
using GringottBank.DataAccess.EF.DataModels;
using GringottsBank.BusinessLogic.Service;
using GringottsBank.Service.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GringottsBank.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService,
            IMapper mapper)
        {
            _logger = logger;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAccount(CustomerCreationDTO customerCreationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ControllerHelperUtility.GetErrorListFromModelState(ModelState));
            try
            {
                var customer = _mapper.Map<Customer>(customerCreationDto);
                var id = await _customerService.CreateCustomer(customer);
                return CreatedAtAction(nameof(CustomerController.GetCustomerById), new { customerId = id },
                    new { customerId = id });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error in Create customer Account");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{customerId:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            if (customerId < 100)
                return BadRequest();

            try
            {
                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                return Ok(_mapper.Map<CustomerResponseDTO>(customer));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in get customer By ID");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


    }
}
