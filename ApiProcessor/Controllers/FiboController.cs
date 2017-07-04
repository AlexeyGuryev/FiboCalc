using System.Threading.Tasks;
using System.Web.Http;
using Core;

namespace ApiProcessor.Controllers
{
    public class FiboController : ApiController
    {
        private readonly Calculator _calculator = new Calculator();

        // GET api/fibo/4?prev=1&cur=2&fin=10
        /// <summary>
        /// Следующее число Фибоначчи
        /// </summary>
        /// <param name="position"></param>
        /// <param name="prev">Предыдущее значение</param>
        /// <param name="cur">Текущее значение</param>
        /// <param name="fin">Индекс последнего рассчитываемого</param>
        /// <returns></returns>
        public async Task Get(int position, int prev, int cur, int fin)
        {
            var fiboNumber = _calculator.Calc(new FiboNumber(position, prev, cur));
            Logger.Log.Info($"Just calculated for step: {fiboNumber.Position} value: {fiboNumber.Current}");

            if (position < fin && fiboNumber.Current >= 0)
            {
                await Send(fiboNumber);
            }
            else
            {
                Logger.Log.Info("Calculation finished!");
            }
        }

        private async Task Send(FiboNumber fiboNumber)
        {
            var endpoint = await WebApiApplication.BusControl.GetSendEndpoint(MqConfig.EndPointUri);
            await endpoint.Send(fiboNumber.GetNextToCalc());
        }
    }
}
