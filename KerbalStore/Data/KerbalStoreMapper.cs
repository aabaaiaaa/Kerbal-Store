using AutoMapper;
using KerbalStore.Data.Entities;
using KerbalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data
{
    public class KerbalStoreMapper : Profile
    {
        public KerbalStoreMapper()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.OrderDate, ex => ex.MapFrom(o => o.OrderCreated))
                .ForMember(o => o.OrderRef, ex => ex.MapFrom(o => o.OrderReference))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
