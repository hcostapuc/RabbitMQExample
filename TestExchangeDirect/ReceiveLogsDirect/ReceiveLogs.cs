using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class ReceiveLogsDirect
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        //declara nosso exchange 
        channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");

        //cria uma queue com nome randomico e temporaria
        var queueName = channel.QueueDeclare().QueueName;

        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                    Environment.GetCommandLineArgs()[0]);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            Environment.ExitCode = 1;
            return;
        }

        //para cada severidade(reoutingKey) ele ira apontar para a mesma fila
        foreach (var severity in args)
        {
            channel.QueueBind(queue: queueName,
                              exchange: "direct_logs",
                              routingKey: severity);
        }

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            Console.WriteLine(" [x] Received '{0}':'{1}'",
                              routingKey, message);
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}