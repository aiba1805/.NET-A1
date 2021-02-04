using BrainstormSessions.Infrastructure;
using BrainstormSessions.ViewModels;
using log4net.Appender;
using log4net.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainstormSessions.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("summary")]
        public LogSummaryDTO Summary()
        {
            
            var errors = _context.Logs.Where(x => x.Level == "Error").ToList();
            var result = new LogSummaryDTO
            {
                ErrorCount = errors.Count(),
                WarnCount = _context.Logs.Where(x => x.Level == "Warn").Count(),
                DebugCount = _context.Logs.Where(x => x.Level == "Debug").Count(),
                InfoCount = _context.Logs.Where(x => x.Level == "Info").Count(),
                Errors = errors
            };
            return result;
        }
    }
}
