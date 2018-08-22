using AutoMapper;
using KerbalStore.Controllers;
using KerbalStore.Data;
using KerbalStore.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kerbal.Tests
{
    [TestClass]
    public class OrdersControllerTests
    {
        private Mock<IKerbalStoreRepository> kerbalStoreRepositoryMock;
        private Mock<ILogger<OrdersController>> loggerMock;
        private Mock<IMapper> mapperMock;
        private OrdersController ordersController;

        [TestInitialize]
        public void Initialise()
        {
            kerbalStoreRepositoryMock = new Mock<IKerbalStoreRepository>();
            kerbalStoreRepositoryMock.Setup(x => x.GetAllOrders()).Returns(new[] { new Order() });
            loggerMock = new Mock<ILogger<OrdersController>>();
            mapperMock = new Mock<IMapper>();
            ordersController = new OrdersController(kerbalStoreRepositoryMock.Object, loggerMock.Object, mapperMock.Object);
            Debug.WriteLine("test initialised");
        }

        [TestMethod]
        public void Repository_GetAllOrders_is_called_once_for_GET_request()
        {
            ordersController.Get();
            kerbalStoreRepositoryMock.Verify(x => x.GetAllOrders(), Times.Exactly(1));
        }

        [TestMethod]
        public void GET_request_exception_logs_error_message()
        {
            kerbalStoreRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new Exception("issue getting orders"));
            ordersController.Get();
            loggerMock.Verify(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<FormattedLogValues>(v => v.ToString().Contains("issue getting orders")),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()), Times.Exactly(1));
        }
    }
}
