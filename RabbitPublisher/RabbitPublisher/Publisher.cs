using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitPublisher
{
    public class Publisher
    {
        public void PublishByTopic()
        {
            IConnection connection =  GetFactory().CreateConnection();
            var channel = connection.CreateModel();

            const string exchange = "fruits";
            const string orangeQueue = "oranges";
            const string appleQueue = "apples";

            channel.ExchangeDeclare(exchange, "topic", true, false, null);
            channel.QueueDeclare(orangeQueue, true, false, false, null);
            channel.QueueDeclare(appleQueue, true, false, false, null);

            channel.QueueBind(orangeQueue, exchange, "trees.oranges.*", null);
            channel.QueueBind(appleQueue, exchange, "trees.apples.#.emergency", null);

            channel.BasicPublish(exchange, "trees.oranges.blossomed", null, Encoding.UTF8.GetBytes("The oranges blossomed this morning"));

            channel.BasicPublish(exchange, "trees.apples.rott.emergency", null, Encoding.UTF8.GetBytes("The apples are rotten to the core"));

            channel.Close();
            channel.Dispose();

            connection.Close();
            connection.Dispose();
        }

        public void PublishByHeader()
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
