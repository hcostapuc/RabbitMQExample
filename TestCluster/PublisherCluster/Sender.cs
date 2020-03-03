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
            var factory = new ConnectionFactory { HostName = "localhost", UserName= "mqadmin", Password= "Admin123XX_" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            const string message = "Tutorial RabbitMQ!";

            //O conteúdo da mensagem é uma matriz de bytes, para que você possa codificar o que quiser.
            int count = 0;
            do
            {
                count++;
                var body = Encoding.UTF8.GetBytes(string.Concat(count, " - ", message));

                channel.BasicPublish(exchange: string.Empty,
                routingKey: "RabbitMQTest",
                basicProperties: null,
                body: body);

            } while (true);
            //Aqui iremos adicionar o método para publicar a mensagem

            Console.WriteLine("Message Sent!");

        }
    }
}
