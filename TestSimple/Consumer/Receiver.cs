using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Receiver
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

            //Agora adicionamos a parte do consumidor, que irá pegar as mensagens da fila:
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "RabbitMQTest", autoAck: true, consumer: consumer);
            Console.WriteLine("Consumer Working!");
            //Usamos um console ReadLine, para manter a aplicação rodando. 
            //Nosso consumer vai estar pegando toda mensagem que chega na fila RabbitMQTest e nos mostrando o que ela contém.
            Console.ReadLine();

        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        }
    }
}
