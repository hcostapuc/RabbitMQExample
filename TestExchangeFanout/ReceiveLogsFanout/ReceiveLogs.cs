using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ReceiveLogsFanout
{
    internal class ReceiveLogs
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);


            //aqui criamos uma queue que será temporaria e de nome aleatório, assim que a aplicação fechar
            //a queue é apagada do server
            var queueName = channel.QueueDeclare().QueueName;

            //baindamos com o exchange criado no EmitLog com a nossa queue
            //para o fanout o parametro routingKey é irrelevante, por trabalhar como um broadcast ele sempre irá
            //enviar para as filas que baindarem o exchange criado, assim o deixa em desuso.
            channel.QueueBind(queue: queueName,
                              exchange: "logs",
                              routingKey: "");

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
