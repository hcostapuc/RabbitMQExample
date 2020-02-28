using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

internal class EmitLogDirect
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "direct_logs",
                                type: "direct");

        
        //recupera as severidades que serão os nossos reoutingKeys
        var severity = (args.Length > 0) ? args[0] : "info";
        
        
        var message = (args.Length > 1)
                      ? string.Join(" ", args.Skip(1).ToArray())
                      : "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        //TODO: deixar o exemplo mais pratico pois aparenta estar executando como o fanout publicar 3 mensagens uma pra cada severidade
        channel.BasicPublish(exchange: "direct_logs",
                             routingKey: severity,
                             basicProperties: null,
                             body: body);
        Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
