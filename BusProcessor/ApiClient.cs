using System.Threading.Tasks;
using RestSharp;

namespace BusProcessor
{
    public class ApiClient
    {
        private readonly string _processorUrl;
        private readonly string _urlTemplate;

        public ApiClient(string processUrl, string urlTemplate)
        {
            _processorUrl = processUrl;
            _urlTemplate = urlTemplate;
        }

        public async Task<IRestResponse> CallApi(string queue, int prev, int cur)
        {
            var client = new RestClient(_processorUrl);

            var request = new RestRequest(_urlTemplate);
            request.AddUrlSegment("queue", queue);
            request.AddUrlSegment("prev", prev.ToString());
            request.AddUrlSegment("cur", cur.ToString());

            return await client.ExecuteGetTaskAsync(request);
        }
    }
}