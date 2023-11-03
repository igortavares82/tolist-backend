using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Standard.ToList.Infrastructure;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Api.Configuration;

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
        builder.AddToLystLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}
