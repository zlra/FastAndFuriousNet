﻿using System.Collections.Generic;
using RabbitMQ.Client; 

namespace fnf.Client.Client
{
    public class QueueRegistry: IQueueRegistry
    {
        private Dictionary<string, string> queueNames = new Dictionary<string, string>();

        public string GetOrCreateBindingQueue(string exchange, string routingKey, IModel channel)
        {
            string queueName; 
            if (queueNames.ContainsKey(exchange + "-" + routingKey))
            {
                queueName = queueNames[exchange + "-" + routingKey];
            }
            else
            {
                channel.ExchangeDeclare(exchange: exchange, type: "direct");
                queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);
                queueNames[exchange + "-" + routingKey] = queueName;
            }
            return queueName;
        }
    }
}
