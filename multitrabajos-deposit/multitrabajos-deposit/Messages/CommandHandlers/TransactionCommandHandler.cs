using Cordillera.Distribuidas.Event.Bus;
using MediatR;
using multitrabajos_deposit.Messages.Commands;
using multitrabajos_deposit.Messages.Events;

namespace multitrabajos_deposit.Messages.CommandHandlers
{
    public class TransactionCommandHandler : IRequestHandler<TransactionCreateCommand, bool>
    {
        private readonly IEventBus _bus;
        public TransactionCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(TransactionCreateCommand request, CancellationToken cancellation)
        {
            _bus.Publish(new TransactionCreatedEvent(request.IdTransaction, request.Amount, request.Type, request.CreationDate, request.AccountId));
            return Task.FromResult(true);
        }

    }
}
