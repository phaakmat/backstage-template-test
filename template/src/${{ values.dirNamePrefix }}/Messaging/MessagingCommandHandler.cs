namespace ${{ values.namespacePrefix }}.Messaging;

public class MessagingCommandHandler : CommandHandler<MessagingCommand>
{
    protected override Task HandleAsync(MessagingCommand command, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
