﻿using log4net;
using log4net.Config;

namespace ApiProcessor
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");

        public static void Init()
        {
            XmlConfigurator.Configure();
        }
    }
}