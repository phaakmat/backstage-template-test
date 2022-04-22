namespace ${{ values.namespacePrefix }}.Messaging;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder AddFundaMessaging(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddFundaDateTimeProvider()
            .AddFundaMessaging()
            .AddFundaMessagingInMemory()
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
