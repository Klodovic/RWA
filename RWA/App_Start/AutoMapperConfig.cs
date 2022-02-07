using AutoMapper;
using RWA.Models.Business_domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void Init()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>());

            Mapper = config.CreateMapper();
        }
    }
} 