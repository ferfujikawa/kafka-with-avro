namespace KafkaWithAvro.Domain.Commands
{
    public class CreateUserCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public CreateUserCommand() { }

        public CreateUserCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
