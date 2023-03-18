using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task atol = new Task(() => GetVersion($"(Аtol)Поток"));
            Task atolNet = new Task(() => GetVersion($"(АtolNet)Поток"));

            atol.Start();
            atolNet.Start();

            Console.ReadLine();
        }

        private static void GetVersion(string threadName)
        {
            EmulatorDevice emulatorDevice = new EmulatorDevice(threadName);
            emulatorDevice.Print();
            if (emulatorDevice.DisposeDriver())
            {
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: Драйвер в потоке {threadName} выгружен.");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: Драйвер в потоке {threadName} не удалось выгрузить.");
            }
        }
    }
}
