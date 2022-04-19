using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.Messaging.Azure;
using Funda.Extensions.Messaging.Configuration;
using Funda.Extensions.Messaging.DatadogTracing;
using Funda.Extensions.Messaging.Metrics;

namespace ${{ values.namespacePrefix }}.Messaging;
public static class WebApplicationExtensions
{
    public static WebApplicationBuilder AddFundaMessaging(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddFundaDateTimeProvider()
            .AddFundaMessaging()
            .AddFundaMessagingAzureServiceBus()
            .AddDatadogTracing()
            .ConfigureEndpoint("Commands", endpoint =>
            {
                endpoint
                    .ConfigurePubSub<MessagingCommand, MessagingCommandHandler>()
                    .ConfigureAzureServiceBusQueue("${{ values.applicationName }}", options =>
                        builder.Configuration.GetSection("AzureServiceBus").Bind(options))
                    .ConfigureAzureServiceBusWorker()
                    .ConfigurePipeline(pipeline =>
                    {
                        pipeline
                            .UsePublishMetrics()
                            .UseRetryMessageExecution()
                            .UseDatadogTracing()
                            .UseHandleMessage();
                    });
            });

        return builder;
    }
}
