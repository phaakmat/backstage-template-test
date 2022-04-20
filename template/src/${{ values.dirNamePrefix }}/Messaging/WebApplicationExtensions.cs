using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.Messaging.InMemory;
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
            .AddDatadogTracing()
            .ConfigureEndpoint("Commands", endpoint =>
            {
                endpoint
                    .ConfigurePubSub<MessagingCommand, MessagingCommandHandler>()
                    .ConfigureInMemoryQueue(options =>
                    {
                        options.MaximumNumberOfMessagesToDequeue = 10;
                    })
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
