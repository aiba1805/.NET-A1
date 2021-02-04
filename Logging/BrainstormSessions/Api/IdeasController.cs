using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.ClientModels;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormSessions.Api
{
    public class IdeasController : ControllerBase
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILog _logger;

        public IdeasController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
            _logger = LogManager.GetLogger(typeof(IdeasController));
        }

        #region snippet_ForSessionAndCreate
        [HttpGet("forsession/{sessionId}")]
        public async Task<IActionResult> ForSession(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                _logger.Warn($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is null");
                return NotFound(sessionId);
            }
            _logger.Debug($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is {session}");

            var result = session.Ideas.Select(idea => new IdeaDTO()
            {
                Id = idea.Id,
                Name = idea.Name,
                Description = idea.Description,
                DateCreated = idea.DateCreated
            }).ToList();

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warn($"{DateTime.Now} - IdeasController Create method: ModelState is invalid");
                return BadRequest(ModelState);
            }

            var session = await _sessionRepository.GetByIdAsync(model.SessionId);
            if (session == null)
            {
                _logger.Warn($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is null");
                return NotFound(model.SessionId);
            }
            _logger.Debug($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is {session}");

            var idea = new Idea()
            {
                DateCreated = DateTimeOffset.Now,
                Description = model.Description,
                Name = model.Name
            };
            session.AddIdea(idea);
            try
            {
                await _sessionRepository.UpdateAsync(session);
            }
            catch(Exception ex)
            {
                _logger.Error($"{DateTime.Now} - SessionRepository UpdateAsync method error occured: {ex.Message}");
            }

            return Ok(session);
        }
        #endregion

        #region snippet_ForSessionActionResult
        [HttpGet("forsessionactionresult/{sessionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<IdeaDTO>>> ForSessionActionResult(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);

            if (session == null)
            {
                _logger.Warn($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is null");
                return NotFound(sessionId);
            }
            _logger.Debug($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is {session}");

            var result = session.Ideas.Select(idea => new IdeaDTO()
            {
                Id = idea.Id,
                Name = idea.Name,
                Description = idea.Description,
                DateCreated = idea.DateCreated
            }).ToList();

            return result;
        }
        #endregion

        #region snippet_CreateActionResult
        [HttpPost("createactionresult")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BrainstormSession>> CreateActionResult([FromBody]NewIdeaModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"{DateTime.Now} - IdeasController CreateActionResult method: ModelState is invalid");
                return BadRequest(ModelState);
            }

            var session = await _sessionRepository.GetByIdAsync(model.SessionId);

            if (session == null)
            {
                _logger.Warn($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is null");
                return NotFound(model.SessionId);
            }
            _logger.Debug($"{DateTime.Now} - SessionRepository GetByIdAsync method: return value is {session}");

            try
            {
                var idea = new Idea()
                {
                    DateCreated = DateTimeOffset.Now,
                    Description = model.Description,
                    Name = model.Name
                };
                session.AddIdea(idea);
            }
            catch (Exception ex)
            {
                _logger.Error($"{DateTime.Now} - Sesion AddIdea method error occured: {ex.Message}");
            }

            try
            {
                await _sessionRepository.UpdateAsync(session);
            }
            catch (Exception ex)
            {
                _logger.Error($"{DateTime.Now} - SessionRepository UpdateAsync method error occured: {ex.Message}");
            }

            return CreatedAtAction(nameof(CreateActionResult), new { id = session.Id }, session);
        }
        #endregion
    }
}
