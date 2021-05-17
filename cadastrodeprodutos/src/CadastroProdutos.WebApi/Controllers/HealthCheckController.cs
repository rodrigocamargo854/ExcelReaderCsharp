using System;
using System.Net;
using Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CadastroProdutos.WebApi.Controllers
{
    [Route("/")]
    public class HealthCheckController : Controller
    {
        private readonly IEventManager _eventManager;
        private readonly IMongoDatabase _database;
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(IEventManager eventManager, IMongoDatabase database, ILogger<HealthCheckController> logger)
        {
            _eventManager = eventManager;
            _database = database;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("")]
        public IActionResult HealthCheck()
        {
            if (!IsMongoAlive())
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            if (!_eventManager.IsAlive())
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return StatusCode((int)HttpStatusCode.OK);
        }

        private bool IsMongoAlive()
        {
            try
            {
                _database.RunCommand((Command<BsonDocument>)"{ping:1}");
                return true;
            }
            catch (Exception excecao)
            {
                _logger.LogError(excecao,
                    $"DataBase connection is down");
                return false;
            }
        }
    }
}