using System;
using System.Diagnostics;
using System.IO;

namespace Dx.Ted.Azure
{
    public static class HelperExtensions
    {
        public static void LogException(this Exception ex, TextWriter writer)
        {
            Helper.LogException(ex, writer);
        }

    }
    public class Helper
    {

        public static void LogException(Exception ex, TextWriter writer)
        {
            if (ex is AggregateException)
                ((AggregateException)ex).Flatten();

            var msg1 = string.Format("Exception Occured: {0}", ex.Message);
            var msg2 = string.Format("EX type: {0}", ex.GetType().ToString());

            Console.Error.WriteLine("ERROR: " + msg1);
            Console.Error.WriteLine("ERROR: " + msg2);
            Console.WriteLine("ERROR: " + msg1);
            Console.WriteLine("ERROR: " + msg2);
            Trace.TraceError(msg1);
            Trace.TraceError(msg2);

            LogMessage(msg1, writer);
        }

        //public static void LogMessage(string message)
        //{
        //    LogMessage(message, null);
        //}

        public static void LogMessage(string message, TextWriter writer)
        {
            var msg1 = string.Format("NOT AN ERROR: {0}", message);

            Console.WriteLine(msg1);
            Trace.TraceError(msg1);

            if (null != writer)
                writer.WriteLine(msg1);
        }
    }


}
