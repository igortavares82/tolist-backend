using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Standard.ToLyst.Infrastructure;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Api.Configuration;

public static class LoggerConfiguration
{
    public static ILoggingBuilder AddToLystLogger(this ILoggingBuilder builder) 
    {
        builder.AddConfiguration();
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ToLystLoggerProvider>());
        LoggerProviderOptions.RegisterProviderOptions<LoggerOptions, ToLystLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddToLystLogger(this ILoggingBuilder builder, Action<LoggerOptions> configure)
    {
        builder.AddConsole()
               .AddToLystLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}
