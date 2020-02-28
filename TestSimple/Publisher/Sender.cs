using RabbitMQ.Client;
using System;
using System.Text;

namespace Publisher
{
    class Sender
    {
        static void Main()
        {
            //Aqui nos conectamos a uma máquina local, daí o localhost.
            //Se quisermos nos conectar uma máquina diferente, simplesmente especificaremos seu nome ou endereço IP.
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //Aqui declaramos a fila. A mesma só será criada se já não existir.
            channel.QueueDeclare(queue: "RabbitMQTest",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            const string message = "Tutorial RabbitMQ!";

            //O conteúdo da mensagem é uma matriz de bytes, para que você possa codificar o que quiser.
            var body = Encoding.UTF8.GetBytes(message);

            //Aqui iremos adicionar o método para publicar a mensagem
            channel.BasicPublish(exchange: string.Empty,
                routingKey: "RabbitMQTest",
                basicProperties: null,
                body: body);
            Console.WriteLine("Message Sent!");

        }
    }
}
