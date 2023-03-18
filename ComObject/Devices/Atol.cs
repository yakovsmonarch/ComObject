using System;
using System.Threading;
using Fptr10Lib;

namespace ComObject.Devices
{
    /// <summary>
    /// Эмуляция работы драйвера Атола как COM объекта.
    /// </summary>
    public class Atol : IDevice
    {
        public string ThreadName { get; private set; }

        private Fptr _com;

        /// <summary>
        /// Получает имя потока, инициализирует com объект.
        /// </summary>
        /// <param name="threadName"></param>
        public Atol(string threadName) 
        { 
            ThreadName = threadName;
            InitDriver();
        }

        /// <summary>
        /// Эмуляция работы принтера путем вывода строк на консоль.
        /// </summary>
        public void Print(int numberString, int threadSleep)
        {
            if(_com == null)
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

        /// <summary>
        /// Получить версию драйвера.
        /// </summary>
        public void GetVersion(string numberString)
        {
            if(_com == null)
            {
                Console.WriteLine("Не удалось получить версию - com объект не загружен.");
                return;
            }

            string version = _com.version();
            Console.WriteLine($"{ThreadName} {numberString} {version}");
        }

        public bool DisposeDriver()
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

        private bool InitDriver()
        {
            Type t = Type.GetTypeFromProgID("AddIn.Fptr10");
            if (t == null)
            {
                return false;
            }
            try
            {
                _com = (Fptr)Activator.CreateInstance(t);
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
                return _com?.close() == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
