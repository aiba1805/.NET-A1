using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using BrainstormSessions.ViewModels;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILog _logger;

        public HomeController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
            _logger = LogManager.GetLogger(typeof(HomeController));
        }

        public async Task<IActionResult> Index()
        {
            var sessionList = await _sessionRepository.ListAsync();
            _logger.Info($"{DateTime.Now} - ListAsync Method returned {sessionList}");

            var model = sessionList.Select(session => new StormSessionViewModel()
            {
                Id = session.Id,
                DateCreated = session.DateCreated,
                Name = session.Name,
                IdeaCount = session.Ideas.Count
            });

            return View(model);
        }

        public class NewSessionModel
        {
            [Required]
            public string SessionName { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewSessionModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warn($"{DateTime.Now} - ModelState is not valid {ModelState}");
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    await _sessionRepository.AddAsync(new BrainstormSession()
                    {
                        DateCreated = DateTimeOffset.Now,
                        Name = model.SessionName
                    });
                }
                catch(Exception ex)
                {
                    _logger.Error($"{DateTime.Now} - Error occured in AddAsync of SessionRepository: {ex.Message}");
                }
                finally
                {
                    _logger.Debug("{DateTime.Now} - SesionRepository AddAsync method executed");
                }
            }

            return RedirectToAction(actionName: nameof(Index));
        }
    }
}
