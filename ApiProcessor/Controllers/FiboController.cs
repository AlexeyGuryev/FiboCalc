using System;
using System.Threading.Tasks;
using System.Web.Http;
using Core;

namespace ApiProcessor.Controllers
{
    public class FiboController : ApiController
    {
        private readonly Calculator _calculator = new Calculator();

        // GET api/fibo/queue?prev=1&cur=2
        /// <summary>
        /// Следующее число Фибоначчи
        /// </summary>
        /// <param name="queue">Очередь (поток)</param>
        /// <param name="prev">Предыдущее значение</param>
        /// <param name="cur">Текущее значение</param>
        /// <returns></returns>
        public async Task Get(string queue, int prev, int cur)
        {
            var newNumber = _calculator.Calc(prev, cur);
            if (newNumber >= 0)
            {
                Logger.Log.Info($"{queue} value: {newNumber}");
                await SendToBus(new FiboNumber(queue, cur, newNumber));
            }
            else
            {
                Logger.Log.Info($"{queue} Calculation finished!");
                await SendToBus(new FiboNumber(queue, cur, newNumber, true));
            }
        }

        private async Task SendToBus(FiboNumber fiboNumber)
        {
            var endPointUri = new Uri($"{MqConfig.RabbitUrl}/{fiboNumber.Queue}");
            var endpoint = await WebApiApplication.BusControl.GetSendEndpoint(endPointUri);
            await endpoint.Send(fiboNumber);
        }
    }
}
