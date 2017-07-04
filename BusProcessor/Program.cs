using System;
using System.Threading.Tasks;
using Core;
using MassTransit;
using RestSharp;

namespace BusProcessor
{
    class Program
    {
        public const int LastNumberIndex = 100;

        private const string ApiProcessorUrlTemplate = "api/fibo/{position}?prev={prev}&cur={cur}&fin={fin}";
        private const string ApiProcessorUrl = "http://localhost:63946";

        public static void CallApi(FiboNumber number)
        {
            var client = new RestClient(ApiProcessorUrl);

            var request = new RestRequest(ApiProcessorUrlTemplate);
            request.AddUrlSegment("position", number.Position.ToString());
            request.AddUrlSegment("prev", number.Previous.ToString());
            request.AddUrlSegment("cur", number.Current.ToString());
            request.AddUrlSegment("fin", LastNumberIndex.ToString());

            client.Execute(request);
        }

        static IBusControl ConnectAndSubscribeToMq()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(MqConfig.Host, h =>
                {
                    h.Username(MqConfig.Username);
                    h.Password(MqConfig.Password);
                });

                cfg.ReceiveEndpoint(host, MqConfig.Queue, e => e.Consumer<FiboCalcConsumer>());
            });
        }

        static void Main(string[] args)
        {
            var bus = ConnectAndSubscribeToMq();
            bus.Start();

            var startNumber = new FiboNumber(2, 0, 1);
            CallApi(startNumber);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }

    public class FiboCalcConsumer : IConsumer<FiboNumber>
    {
        public async Task Consume(ConsumeContext<FiboNumber> context)
        {
            var calculator = new Calculator();
            var calculated = calculator.Calc(context.Message);
            await Console.Out.WriteLineAsync($"Just calculated for step: {calculated.Position} value: {calculated.Current}");

            if (calculated.Position < Program.LastNumberIndex && calculated.Current >= 0)
            {
                Program.CallApi(calculated.GetNextToCalc());
            }
            else
            {
                Console.Write("Calculation finished!");
            }
        }
    }
}
