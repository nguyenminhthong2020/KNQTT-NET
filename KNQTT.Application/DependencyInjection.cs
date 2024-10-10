using KNQTT.Application.Behaviors;
using KNQTT.Application.Common;
using KNQTT.Infrastructure;
using KNQTT.Infrastructure.Database;
using FluentValidation;
using Infrastructure.Core.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KNQTT.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // system config
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // more logic config here
            services.AddSingleton<IQuery, SqlServer>();

            services.AddTransient<IQueueProcess, QueueProcess>();
            services.AddSingleton<ICoreHttpClientCustomize, CoreHttpClientCustomize>();
            services.AddTransient<ICommonProcess, CommonProcess>();
            services.AddTransient<ISendSMS, SendSMS>();

            return services;
        }
    }
}
