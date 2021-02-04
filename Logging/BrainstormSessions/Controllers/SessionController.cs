using System;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILog _logger;

        public SessionController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
            _logger = LogManager.GetLogger(typeof(SessionController));
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            {
                _logger.Warn($"{DateTime.Now} - SessionController Index method: id parameter is null");
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }
            _logger.Debug($"{DateTime.Now} - SessionController Index method: id parameter is {id}");
            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if (session == null)
            {
                _logger.Warn($"{DateTime.Now} - SessionRepository GetByIdAsync method return value is null");
                return Content("Session not found.");
            }
            _logger.Debug($"{DateTime.Now} - SessionRepository GetByIdAsync method return value is {session}");
            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };

            return View(viewModel);
        }
    }
}
