using System;

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
                Console.WindowLine("Message Sended with success");
            }

            Console.ReadKey();
        }
    }
}