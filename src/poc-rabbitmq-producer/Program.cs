using System;
using System.Threading;

namespace poc_rabbitmq_producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****Sending Messages");
            var rabbitMqClient = new RabbitMqClient();

            for (int i = 0; i < 100000; i++)
            {
                var message = string.Concat($"Olá, mundo! nº {i} - {DateTime.Now}");
                rabbitMqClient.Publish(message);
                Console.WriteLine("Message Sended with success");
                Thread.Sleep(10000);
            }

            Console.ReadKey();
        }
    }
}