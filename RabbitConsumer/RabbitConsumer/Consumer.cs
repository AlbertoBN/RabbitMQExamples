using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitConsumer
{
    public class Consumer
    {
        public void ConsumeTopic()
        {
            IConnection connection = GetFactory().CreateConnection();
            var channel = connection.CreateModel();

            const string exchange = "fruits";
            const string orangeQueue = "oranges";
            const string appleQueue = "apples";

            channel.ExchangeDeclare(exchange, "topic", true, false, null);
            channel.QueueDeclare(orangeQueue, true, false, false, null);
            channel.QueueDeclare(appleQueue, true, false, false, null);

            channel.QueueBind(orangeQueue, exchange, "trees.oranges.*", null);
            channel.QueueBind(appleQueue, exchange, "trees.apples.#.emergency", null);

            var orangesConsumer = new EventingBasicConsumer(channel);
            var applesConsumer = new EventingBasicConsumer(channel);

            orangesConsumer.Received += OrangesConsumer_Received;
            applesConsumer.Received += ApplesConsumer_Received;
            channel.BasicConsume(orangeQueue, true, orangesConsumer);
            channel.BasicConsume(appleQueue, true, applesConsumer);

            channel.Close();
            channel.Dispose();

            connection.Close();
            connection.Dispose();
        }

        private void ApplesConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"Apples Consumer received: {Encoding.UTF8.GetString(e.Body)}, {e.RoutingKey}");
        }

        private void OrangesConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"Oranges Consumer received: {Encoding.UTF8.GetString(e.Body)}, {e.RoutingKey}");
        }

        public void ConsumeHeader()
        {

        }

        private IConnectionFactory GetFactory()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://xubthrvo:mL0njyiVtp_oxeztzhJwqnauYQJT9-WE@hound.rmq.cloudamqp.com/xubthrvo")
            };

            return factory;
        }

    }
}
