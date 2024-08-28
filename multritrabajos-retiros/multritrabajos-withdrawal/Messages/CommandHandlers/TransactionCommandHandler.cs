using Cordillera.Distribuidas.Event.Bus;
using MediatR;
using multritrabajos_withdrawal.Messages.Commands;
using multritrabajos_withdrawal.Messages.Events;

namespace multritrabajos_withdrawal.Messages.CommandHandlers
{
    public class TransactionCommandHandler: IRequestHandler<TransactionCreateCommand, bool>
    {
        private readonly IEventBus _bus;
        public TransactionCommandHandler(IEventBus eventBus) {
            _bus = eventBus;
        }
        public Task<bool> Handle(TransactionCreateCommand request, CancellationToken cancellation)
        {
            _bus.Publish(new TransactionCreatedEvent(request.IdTransaction, request.Amount, request.Type, request.CreationDate, request.AccountId));
            return Task.FromResult(true);
        }

    }
}
