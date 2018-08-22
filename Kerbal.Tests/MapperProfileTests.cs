using AutoMapper;
using KerbalStore.Data;
using KerbalStore.Data.Entities;
using KerbalStore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class MapperProfileTests
{
    [TestInitialize]
    public void Initialise()
    {
        Mapper.Initialize(cfg => cfg.AddProfile(new KerbalStoreMapper()));
        Mapper.AssertConfigurationIsValid();
    }

    //[TestMethod]
    //public void Check_AutoMapper_Config_Is_Valid()
    //{
    //    Mapper.Initialize(cfg => cfg.AddProfile(new KerbalStoreMapper()));
    //    Mapper.AssertConfigurationIsValid();
    //}

    [TestMethod]
    public void Mapping_OrderItems()
    {
        DateTime dateTimeNow = DateTime.Now;
        var expectedOrder = new Order() { Id = 1, OrderCreated = dateTimeNow, OrderReference = "test" };
        var expectedOrderViewModel = new OrderViewModel() { OrderId = 1, OrderRef = "test", OrderDate = dateTimeNow, OrderItems = new List<OrderItemViewModel>() };
        var actualOrderViewModel = Mapper.Map<Order, OrderViewModel>(expectedOrder);
        Assert.AreEqual(expectedOrderViewModel.OrderId, actualOrderViewModel.OrderId);
        Assert.AreEqual(expectedOrderViewModel.OrderDate, actualOrderViewModel.OrderDate);
        Assert.AreEqual(expectedOrderViewModel.OrderRef, actualOrderViewModel.OrderRef);
        CollectionAssert.AreEquivalent(expectedOrderViewModel.OrderItems.ToList(), actualOrderViewModel.OrderItems.ToList());
        /**
         * CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.OrderDate, ex => ex.MapFrom(o => o.OrderCreated))
                .ForMember(o => o.OrderRef, ex => ex.MapFrom(o => o.OrderReference))
                .ReverseMap();
         * */
    }
}