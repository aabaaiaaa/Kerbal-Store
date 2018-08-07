using AutoMapper;
using KerbalStore.Data;
using KerbalStore.Data.Entities;
using KerbalStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IKerbalStoreRepository kerbalStoreRepository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public OrdersController(IKerbalStoreRepository kerbalStoreRepository, ILogger<OrdersController> logger, IMapper mapper)
        {
            this.kerbalStoreRepository = kerbalStoreRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<OrderViewModel>>(kerbalStoreRepository.GetAllOrders()));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = kerbalStoreRepository.GetOrder(id);
                if (order != null) return Ok(mapper.Map<OrderViewModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newModel = mapper.Map<OrderViewModel, Order>(model);

                kerbalStoreRepository.Add(newModel);
                if (kerbalStoreRepository.SaveAll())
                {
                    model = mapper.Map<Order, OrderViewModel>(newModel);
                    return Created($"api/[Controller]/{model.OrderId}", model);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to store order: {ex}");
            }

            return BadRequest("Failed to save order");
        }
    }
}
