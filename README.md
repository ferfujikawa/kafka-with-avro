# Kafka With Avro

Solução para prática de conceitos de Kafka utilizando serializador Avro.

## 1 Servidor Kafka e dependências (Docker)

Para inicializar o container, acesse o diretório `kafka-with-avro-docker` e execute o comando abaixo:

```bash
docker-compose up -d
```

### 1.1 Acesso ao Control Center

Para acessar o Control Center do Kafka, acesso o endereço http://localhost:9021.

### 1.2 Criação do tópico com validação de schema

#### 1.2.1 Utilizando a interface gráfica do Control Center

- Acesse o menu *Topics*;
- Clique no botão *Create a topic*;
- Informe o nome do tópico como **created-user** e clique no botão *Create with defaults*;
- Acesse o menu *Configuration*;
- Clique no botão *Edit settings* e depois em *Switch to expert mode*;
- Altere a opção *confluent.value.schema.validation* para *true*;
- Clique no botão *Save changes*.

#### 1.2.2 Executando comando no console

Acesso o console de comando do *Broker*, através do Docker e digite o seguinte comando:

```bash
kafka-topics --bootstrap-server localhost:9092 --create --topic created-user --partitions 1 --replication-factor 1 --config max.message.bytes=64000 --config flush.messages=1 --config confluent.value.schema.validation=true
```

## 2 Criação de Avros

Com os arquivos de template (.avsc) criados, execute o seguinte comando:

```bash
avrogen -s .\SchemaRegistry\Avros\Templates\NOME_DA_CLASSE.avsc .\SchemaRegistry\Avros\Models\ --skip-directories
```

## 3 Execução da API Producer

A API possui documentação *Swagger* portanto pode ser testada facilmente após inicialização através da CLI do .NET.

```bash
cd KafkaWithAvro.Producer.Api
dotnet watch run
```

## 4 Execução do Console App Consumer

Este projeto é um aplicativo console que faz o consumo das mensagens do Kafka. Para iniciá-lo, execute:

```bash
cd KafkaWithAvro.Consumer.App
dotnet run
```