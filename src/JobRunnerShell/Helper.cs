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

            LogErrorMessage(msg1, writer);
            LogErrorMessage(msg2, writer);

        }

        public static void LogMessage(string message, TextWriter writer)
        {
            var msg1 = string.Format("[INFO] {0}", message);

            Console.WriteLine(msg1);
            Trace.TraceInformation(msg1);

            if (null != writer)
                writer.WriteLine(msg1);
        }

        public static void LogErrorMessage(string message, TextWriter writer)
        {
            var msg = "[ERROR] " + message;

            Trace.TraceError(msg);
            Console.Error.WriteLine(msg);
            Console.WriteLine(msg);

            if (null != writer)
                writer.WriteLine(msg);

        }
    }


}
