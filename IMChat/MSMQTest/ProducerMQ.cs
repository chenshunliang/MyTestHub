using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;

namespace MSMQTest
{
    public class ProducerMq
    {
        public static void SingleToSingle(string msg)
        {
            string quene_name = "single";
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "127.0.01";
            factory.Port = 5672;
            factory.UserName = "chen";
            factory.Password = "1qazxsw2";
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.QueueDeclare("SingleToSingle", false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish("", "SingleToSingle", null, body);
                }
            }
        }
    }
}
