﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Consumer consumer = new Consumer();
            consumer.ConsumeTopic();

            Console.ReadKey();
        }
    }
}
