﻿using Microsoft.Extensions.DependencyInjection;
using MiniE_Commorce.Application.BaseAppSettings;
using MiniE_Commorce.Application.Interfaces.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application
{
    public static class Registration
    {
        public static void AddAplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(assembly));
            services.AddAutoMapper(assembly);




        }
    }
}
