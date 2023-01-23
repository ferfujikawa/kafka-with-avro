namespace KafkaWithAvro.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; }
        public string Email { get; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
