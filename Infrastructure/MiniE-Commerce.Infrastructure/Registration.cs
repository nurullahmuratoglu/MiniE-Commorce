using Microsoft.Extensions.DependencyInjection;
using MiniE_Commerce.Infrastructure.Services.Token;
using MiniE_Commorce.Application.Interfaces.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure
{

    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
