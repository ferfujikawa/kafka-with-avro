using KafkaWithAvro.Domain.Entities;

namespace KafkaWithAvro.Domain.Commands.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        public async Task HandleAsync(CreateUserCommand request)
        {
            var newUser = new User(request.Name, request.Email);

            await Task.CompletedTask;
        }
    }
}
