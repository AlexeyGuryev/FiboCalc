﻿using System;

namespace Core
{
    public class MqConfig
    {
        public static string RabbitUrl = @"rabbitmq://spotted-monkey.rmq.cloudamqp.com/apdkupga";

        public static Uri Host = new Uri(RabbitUrl);
        public static string Username = @"apdkupga";
        public static string Password = @"Ocf8DkD9KTHSIXU9rXi7PjB0WH-EzIsB";
        public static string Queue = @"fibo_calc";

        public const string ApiProcessorUrlTemplate = "api/fibo/{queue}?prev={prev}&cur={cur}";
        public const string ApiProcessorUrl = "http://localhost:63946";
    }
}
