using AutoMapper;
using GringottsBank.BusinessLogic.Service;
using GringottsBank.Service.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GringottsBank.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger,IMapper mapper)
        {
            _logger = logger;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet("{transactionId:Guid}", Name = "GetTransaction")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTransactionById(Guid? transactionId)
        {
            if (transactionId == null || transactionId == Guid.Empty)
                return BadRequest();
            try
            {
                var transaction=await _transactionService.GetTransactionById(transactionId);
                if (transaction == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                return Ok(_mapper.Map<TransactionResponseDTO>(transaction));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTrnsactionby id");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("[action]/{customerId:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTransactions(int customerId,DateTimeDTO dateTimeDTO)
        {
            if (customerId<100)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ControllerHelperUtility.GetErrorListFromModelState(ModelState));
            try
            {
                var transactions = await _transactionService.GetAllTransactionByTimePeriod(customerId,
                    dateTimeDTO.StartTime,dateTimeDTO.EndTime);
                if (transactions?.Count==0)
                    return StatusCode(StatusCodes.Status204NoContent);
                return Ok(_mapper.Map<List<TransactionResponseDTO>>(transactions));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTrnsactions in a range");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
