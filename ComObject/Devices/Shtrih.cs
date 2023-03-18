using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DrvFRLib;
using Fptr10Lib;

namespace ComObject.Devices
{
    public class Shtrih : IDevice
    {
        public string ThreadName { get; private set; }

        private DrvFR _com;

        public Shtrih(string threadName)
        {
            ThreadName = threadName;
            InitDriver();
        }

        public bool DisposeDriver()
        {
            if (Disconnect())
            {
                _com = null;
                return true;
            }

            return false;
        }

        public void GetVersion(string numberString)
        {
            if (_com == null)
            {
                Console.WriteLine("Не удалось получить версию - com объект не загружен.");
                return;
            }

            string version = _com.DriverVersion;
            Console.WriteLine($"{ThreadName} {numberString} {version}");
        }

        public void Print(int numberString, int threadSleep)
        {
            if (_com == null)
            {
                Console.WriteLine($"Драйвер не загружен.");
                return;
            }

            // Эмуляция работы принтера
            Console.WriteLine($"Начало работы принтера {ThreadName}");
            for (int i = 0; i < numberString; i++)
            {
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: {ThreadName} -> строка - {i}");
                Thread.Sleep(threadSleep);
            }
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: {ThreadName} - Печать закончена.");
        }

        private bool InitDriver()
        {
            Type t = Type.GetTypeFromProgID("AddIn.Drvfr");
            if (t == null)
            {
                return false;
            }
            try
            {
                _com = (DrvFR)Activator.CreateInstance(t);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool Disconnect()
        {
            try
            {
                return _com.Disconnect() == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
