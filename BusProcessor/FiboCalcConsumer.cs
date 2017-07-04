using System;
using System.Threading.Tasks;
using Core;
using MassTransit;

namespace BusProcessor
{
    public class FiboCalcConsumer : IConsumer<FiboNumber>
    {
        private readonly Calculator _calculator = new Calculator();

        public async Task Consume(ConsumeContext<FiboNumber> context)
        {
            var fiboNumber = context.Message;
            var newNumber = _calculator.Calc(fiboNumber.Previous, fiboNumber.Current);
            if (newNumber >= 0 && !fiboNumber.Finished)
            {
                Console.WriteLine($"{fiboNumber.Queue} value: {newNumber}");

                var apiClient = new ApiClient(MqConfig.ApiProcessorUrl, MqConfig.ApiProcessorUrlTemplate);
                var result = await apiClient.CallApi(fiboNumber.Queue, fiboNumber.Current, newNumber);
                if (result.ErrorException != null)
                {
                    throw result.ErrorException;
                }
            }
            else
            {
                await Console.Out.WriteLineAsync($"{fiboNumber.Queue} Calculation finished!");
            }
        }
    }
}