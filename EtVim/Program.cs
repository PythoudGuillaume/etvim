using System;
using System.Threading;

namespace EtVim
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, Guid.NewGuid().ToString());
            new EtVim(waitHandle);
            Console.In.Read();
            waitHandle.Set();
        }
    }
}
