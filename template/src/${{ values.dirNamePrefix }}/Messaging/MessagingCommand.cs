using Funda.Extensions.Messaging;
using Funda.Extensions.Messaging.CQRS;

namespace ${{ values.namespacePrefix }}.Messaging;

public class MessagingCommand : ICommand
{
    public Guid Id { get; set; }

    public bool Equals(IMessage? other)
    {
        if (other == null)
        {
            return false;
        }

        return other.Id == Id;
    }
}
