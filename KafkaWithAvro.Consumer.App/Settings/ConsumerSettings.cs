namespace KafkaWithAvro.Consumer.App.Settings
{
    public class ConsumerSettings
    {
        public string Topic { get; set; }
        public string Group { get; set; }

        public ConsumerSettings()
        {
        }
    }
}
