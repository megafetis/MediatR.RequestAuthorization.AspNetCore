using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using UTypeExtensions;

namespace MediatR.RequestAuthorization.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRAuthorization(this IServiceCollection services, params Assembly[] assemblies)
        {
            assemblies = assemblies != null && assemblies.Any() ? assemblies : new[] { Assembly.GetEntryAssembly() };

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));

            foreach (var assembly in assemblies)
            {
                var authRules = assembly.GetTypes().Where(p => p.IsClass && !p.IsAbstract && p.InheritsOrImplements(typeof(IAuthorizationRule<>)));
                foreach (var authRule in authRules)
                {
                    var i = authRule.GetInterfaces().First(p => p.InheritsOrImplements(typeof(IAuthorizationRule<>)));
                    services.AddTransient(i, authRule);
                }
            }

            return services;
        }
    }
}
