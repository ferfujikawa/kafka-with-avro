namespace KafkaWithAvro.Infra.Kafka.Configurations
{
    public class KafkaConfig
    {
        public string BootstrapServer { get; set; }
        public string SchemaServer { get; set; }

        public KafkaConfig()
        {
        }

        public KafkaConfig(string bootstrapServer, string schemaServer)
        {
            BootstrapServer = bootstrapServer;
            SchemaServer = schemaServer;
        }
    }
}
