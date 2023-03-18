using Fptr10Lib;
using System;
using System.Threading;

namespace ComObject
{
    public class EmulatorDevice
    {
        public readonly string ThreadName;

        private IFptr _com;

        public EmulatorDevice(string threadName) 
        { 
            ThreadName = threadName;
            InitDriver();
        }

        public void GetVersion()
        {
            if(_com == null)
            {
                Console.WriteLine("Не удалось получить версию - com объкт не загружен.");
            }
            string version = _com.version();


            if(DisposeDriver() == false)
            {
                throw new Exception("Com объект не удалось уничтожить!");
            }

            // Эмуляция работы принтера
            Console.WriteLine($"Начало работы принтера {ThreadName}");
            for(int i = 0; i < 500; i++)
            {
                Console.WriteLine($"{ThreadName} -> строка - {i}");
                //Thread.Sleep(1);
            }
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")} - {ThreadName} - {version}. Печать закончена.");
        }

        private bool InitDriver()
        {
            Type t = Type.GetTypeFromProgID("AddIn.Fptr10");
            if (t == null)
            {
                return false;
            }
            try
            {
                _com = (IFptr)Activator.CreateInstance(t);
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
                _com?.close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool DisposeDriver()
        {
            try
            {
                if (_com != null)
                {
                    Disconnect();
                    _com?.destroy();
                    _com = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
