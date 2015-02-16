using Dx.Ted.Azure;

namespace TcpPing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var method = typeof(Job).GetMethod("RunCalls");

            JobRunnerShellProgram.Run(method);
        }


    }
}
