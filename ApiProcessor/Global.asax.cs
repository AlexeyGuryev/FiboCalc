using System.Web.Http;
using Core;
using MassTransit;

namespace ApiProcessor
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static IBusControl _busControl;

        public static IBus BusControl => _busControl;

        protected void Application_Start()
        {
            Logger.Init();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            _busControl = ConfigureBus();
        }

        IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(MqConfig.Host, h =>
                {
                    h.Username(MqConfig.Username);
                    h.Password(MqConfig.Password);
                });
            });
        }
    }
}
