using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitPublisher
{
    class Program
    {
        static void Main(string[] args)
        {

            Publisher publisher = new Publisher();
            publisher.PublishByTopic();

        }
    }
}
