using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.EF.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly BankDBContext _bankDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, BankDBContext bankDbContext)
        {
            _logger = logger;
            _bankDbContext = bankDbContext;
        }
        
    }
}
