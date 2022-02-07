using AutoMapper;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using GringottsBank.BusinessLogic.Service;
using GringottsBank.Service.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GringottsBank.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService,
            IMapper mapper)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts=await _accountService.GetAllAccounts();
                if (accounts?.Count == 0)
                    return StatusCode(StatusCodes.Status204NoContent);
                var responseDTO=_mapper.Map<IList<AccountResponseDTO>>(accounts);
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in GetAllAccount");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAccount(AccountCreationDTO accountCreationDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ControllerHelperUtility.GetErrorListFromModelState(ModelState));
            try
            {
                var account = _mapper.Map<Account>(accountCreationDTO);
                var id=await _accountService.CreateAccount(account);
                return CreatedAtAction(nameof(GetAccountById),
                    new { accountId = id }, new { accounId = id });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error in CreateAccount");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("[action]/{accountId:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            if(accountId<10000)
                return BadRequest();

            try
            {
                var account = await _accountService.GetAccountById(accountId);
                if (account == null)
                    return StatusCode(StatusCodes.Status204NoContent);
                return Ok(_mapper.Map<AccountResponseDTO>(account));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAccount By ID");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{customerId:int}/allaccounts")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllAccountsByCustomerId(int customerId)
        {
            if(customerId<100)
                return BadRequest();
            try
            {
                var accounts = await _accountService.GetAccountsByCustomerId(customerId);
                if (accounts?.Count == 0)
                    return StatusCode(StatusCodes.Status204NoContent);
                return Ok(_mapper.Map<IList<AccountResponseDTO>>(accounts));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Accounts by CustomerID");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{accounID:int}/alltransactions")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllTransactionInAccount(int accounID)
        {
            if (accounID < 10000)
                return BadRequest();
            try
            {
                var transactions = await _accountService.GetTransactionsByAccountId(accounID);
                if (transactions?.Count == 0)
                    return StatusCode(StatusCodes.Status204NoContent);
                return Ok(_mapper.Map<IList<TransactionResponseDTO>>(transactions));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Accounts by CustomerID");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("Deposit")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AccountDeposit(
            TransactionCreationDTO transactionCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ControllerHelperUtility.GetErrorListFromModelState(ModelState));
            try
            {
                var transaction = _mapper.Map<Transaction>(transactionCreationDTO);
                var transactionId = await _accountService.AccounDeposit(transaction);
                return CreatedAtAction(nameof(TransactionController.GetTransactionById), new { controller = "transaction", transactionId = transactionId },
                    new { transactionId = transactionId });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured in AccountDeposit");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("Withdrawl")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AccountWithdrwal(
           TransactionCreationDTO transactionCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ControllerHelperUtility.GetErrorListFromModelState(ModelState));
            try
            {
                var transaction = _mapper.Map<Transaction>(transactionCreationDTO);
                var transactionId = await _accountService.AccountWithdrwal(transaction);
                return CreatedAtAction(nameof(TransactionController.GetTransactionById), new { controller = "transaction" , transactionId = transactionId },
                    new { transactionId = transactionId });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured in AccountDeposit");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}