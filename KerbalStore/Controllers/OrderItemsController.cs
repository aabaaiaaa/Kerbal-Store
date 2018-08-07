using AutoMapper;
using KerbalStore.Data;
using KerbalStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Controllers
{
    [Route("api/orders/{orderId}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IKerbalStoreRepository kerbalStoreRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OrderItemsController(IKerbalStoreRepository kerbalStoreRepository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            this.kerbalStoreRepository = kerbalStoreRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            try
            {
                var order = kerbalStoreRepository.GetOrder(orderId);
                if (order != null) return Ok(mapper.Map<IEnumerable<OrderItemViewModel>>(order.OrderItems));
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get order item details: {ex}");
                return BadRequest("Failed to get order item details");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                var order = kerbalStoreRepository.GetOrder(orderId);
                if (order != null)
                {
                    var orderDetail = order.OrderItems.Where(oi => oi.Id == id).FirstOrDefault();
                    if (orderDetail != null)
                    {
                        return Ok(mapper.Map<OrderItemViewModel>(orderDetail));
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get order item detail: {ex}");
                return BadRequest("Failed to get order item detail");
            }
        }
    }
}