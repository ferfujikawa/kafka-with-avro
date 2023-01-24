using KafkaWithAvro.Domain.Entities;
using KafkaWithAvro.Domain.MessageBus.Producers;

namespace KafkaWithAvro.Domain.Commands.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IProducer<User> _producer;

        public CreateUserHandler(IProducer<User> producer)
        {
            _producer = producer;
        }

        public async Task HandleAsync(CreateUserCommand request)
        {
            var newUser = new User(request.Name, request.Email);

            //Execução de rotinas de cadastro

            await _producer.ProduceAsync("created-user", newUser);

            await Task.CompletedTask;
        }
    }
}
