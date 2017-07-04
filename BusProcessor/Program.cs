using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using MassTransit;

namespace BusProcessor
{
    class Program
    {
        static IBusControl ConnectAndSubscribeToMq(List<string> queues)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(MqConfig.Host, h =>
                {
                    h.Username(MqConfig.Username);
                    h.Password(MqConfig.Password);
                });

                foreach (var queue in queues)
                {
                    cfg.ReceiveEndpoint(host, queue, e => e.Consumer<FiboCalcConsumer>());
                }
            });
        }

        static void Main(string[] args)
        {
            var queues = GetQueuesFromArgs(args);

            var bus = ConnectAndSubscribeToMq(queues);
            bus.Start();

            StartCalcQueues(queues);

            Console.ReadKey();
            bus.Stop();
        }

        private static void StartCalcQueues(List<string> queues)
        {
            var apiClient = new ApiClient(MqConfig.ApiProcessorUrl, MqConfig.ApiProcessorUrlTemplate);
            foreach (var queue in queues)
            {
                apiClient.CallApi(queue, 0, 0);
            }
        }

        private static List<string> GetQueuesFromArgs(string[] args)
        {
            int numberOfQueues = 1;
            if (args.Length >= 0 && int.TryParse(args[0], out numberOfQueues))
            {
                numberOfQueues = numberOfQueues < 0 || numberOfQueues > 100 ? 1 : numberOfQueues;
            }
            return Enumerable.Range(1, numberOfQueues)
                .Select(c => $"{MqConfig.Queue}_{c}")
                .ToList();
        }
    }
}
