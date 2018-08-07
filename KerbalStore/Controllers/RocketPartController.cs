using KerbalStore.Data;
using KerbalStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Controllers
{
    [Route("api/[Controller]")]
    public class RocketPartController : Controller
    {
        private readonly IKerbalStoreRepository kerbalStoreRepository;
        private readonly ILogger<RocketPartController> logger;

        public RocketPartController(IKerbalStoreRepository kerbalStoreRepository, ILogger<RocketPartController> logger)
        {
            this.kerbalStoreRepository = kerbalStoreRepository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(kerbalStoreRepository.GetAllRocketParts().ToList());
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }

        [HttpGet("query")]
        public IActionResult Get(string name)
        {
            try
            {
                return Ok(kerbalStoreRepository.GetRocketPartsByName(name).ToList());
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to get rocket parts matching '{name}', exception: {ex}");
                return BadRequest("Failed to get any rocket parts matching '{name}'");
            }
        }
    }
}
