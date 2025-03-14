using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductsMicroService.Core.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration configuration;
    private readonly IConnection connection;
    private readonly IModel channel;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        this.configuration = configuration;

        var hostname = this.configuration["RabbitMQ_HostName"]!;
        var userName = this.configuration["RabbitMQ_UserName"]!;
        var password = this.configuration["RabbitMQ_Password"]!;
        var port = this.configuration["RabbitMQ_Port"]!;

        var connectionFactory = new ConnectionFactory()
        {
            HostName = hostname,
            UserName = userName,
            Password = password,
            Port = int.Parse(port)
        };

        connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
    }

    public void Publish<T>(string routingKey, T message)
    {
        string messageJson = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);

        string exchangeName = "products.exchange";
        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true);

        channel.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: null,
            body: messageBytes);
    }

    public void Dispose()
    {
        channel.Dispose();
        connection.Dispose();
    }
}