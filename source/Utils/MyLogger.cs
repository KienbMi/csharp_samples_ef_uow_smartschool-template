using System.Diagnostics.Tracing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using LiveCoding.Util;

namespace Utils
{
    public class MyLogger
    {
        public static SQLCommandLogObserver SqlCommandLogObserver { get; set; }

        public static void InitializeLogger()
        {
            SqlCommandLogObserver = new SQLCommandLogObserver();

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .WriteTo.Console()
              .WriteTo.File("SqlCommands.log")
              .WriteTo.Observers(events => events.Subscribe(SqlCommandLogObserver))
              .CreateLogger();
        }
    }
}
