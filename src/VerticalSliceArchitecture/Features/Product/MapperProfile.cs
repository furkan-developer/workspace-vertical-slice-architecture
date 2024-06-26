using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Features
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product,GetProduct.Result>();
            CreateMap<CreateProduct.Command,Product>();
        }
    }
}