using ComObject.Devices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task atol = new Task(() => Print(new Atol("(Аtol)Поток")));
            //Task atolNet = new Task(() => Print(new Atol("(АtolNet)Поток")));
            //Task shtrih = new Task(() => Print(new Shtrih("(Shtrih---)Поток")));
            Task shtrih1 = new Task(() => Print(new Shtrih("(Shtrih1)Поток")));
            Task shtrih2 = new Task(() => Print(new Shtrih("(Shtrih2---)Поток")));

            //atol.Start();
            //atolNet.Start();
            //shtrih.Start();
            shtrih1.Start();
            shtrih2.Start();

            Console.ReadLine();
        }

        private static void Print(IDevice device)
        {
            device.Print(100, 1);
            if (device.DisposeDriver())
            {
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: Драйвер в потоке {device.ThreadName} выгружен.");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: Драйвер в потоке {device.ThreadName} не удалось выгрузить.");
            }
        }
    }
}
