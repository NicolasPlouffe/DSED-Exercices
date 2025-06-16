using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(
        exchange: "information_animaux",
        type: "topic",
        durable: true,
        autoDelete: false
        );
