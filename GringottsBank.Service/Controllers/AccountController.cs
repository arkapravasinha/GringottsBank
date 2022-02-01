using AutoMapper;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using GringottsBank.Service.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GringottsBank.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IGringottBankUnitOfWork _gringottBankUnitOfWork;
        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger, IGringottBankUnitOfWork gringottBankUnitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _gringottBankUnitOfWork = gringottBankUnitOfWork;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts=await _gringottBankUnitOfWork.AccountRepository.All();
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAccount(AccountCreationDTO accountCreationDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ControllerHelperUtility.GetErrorListFromModelState(ModelState));
            try
            {
                var account = _mapper.Map<Account>(accountCreationDTO);
                await _gringottBankUnitOfWork.Database.BeginTransactionAsync();
                await _gringottBankUnitOfWork.AccountRepository.Add(account);
                await _gringottBankUnitOfWork.SaveChangesAsync("testUserID");
                await _gringottBankUnitOfWork.Database.CommitTransactionAsync();
                return CreatedAtAction(nameof(GetAccountById),
                    new { accountId = account.AccountID },
                    _mapper.Map<AccountResponseDTO>(account));
            }
            catch (Exception exception)
            {
                await _gringottBankUnitOfWork.Database.RollbackTransactionAsync();
                _logger.LogError(exception, "Error in CreateAccount");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            if(accountId<100)
                return BadRequest();
            var accounts = await _gringottBankUnitOfWork.AccountRepository
                .Find(x => x.AccountID == accountId);
            var account = accounts?.FirstOrDefault();
        }
    }
}
