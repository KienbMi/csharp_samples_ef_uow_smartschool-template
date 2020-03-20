using System;
using System.Linq;
using Serilog.Events;

namespace LiveCoding.Util
{
    public class SQLCommandLogObserver : IObserver<LogEvent>
    {
        public string LogText { get; private set; }


        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(LogEvent value)
        {
            foreach (var property in value.Properties.Where(_ => _.Key == "commandText"))
            {
                LogText += $"{property.Value}\n";
            }
        }
    }
}