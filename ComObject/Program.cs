using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task atol = new Task(() => GetVersion($"(atol)Поток"));
            Task atolNet = new Task(() => GetVersion($"(atolNet)Поток"));

            atol.Start();
            atolNet.Start();

            Console.ReadLine();
        }

        private static void GetVersion(string threadName)
        {
            EmulatorDevice emulatorDevice = new EmulatorDevice(threadName);
            emulatorDevice.GetVersion();
        }
    }
}
