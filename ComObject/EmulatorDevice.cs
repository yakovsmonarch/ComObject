using Fptr10Lib;
using System;
using System.Threading;

namespace ComObject
{
    /// <summary>
    /// Эмуляция работы драйвера Атола как COM объекта.
    /// </summary>
    public class EmulatorDevice
    {
        public readonly string ThreadName;

        private IFptr _com;

        /// <summary>
        /// Получает имя потока, инициализирует com объект.
        /// </summary>
        /// <param name="threadName"></param>
        public EmulatorDevice(string threadName) 
        { 
            ThreadName = threadName;
            InitDriver();
        }

        /// <summary>
        /// Эмуляция работы принтера путем вывода строк на консоль.
        /// </summary>
        public void Print()
        {
            if(_com == null)
            {
                Console.WriteLine($"Драйвер не загружен.");
                return;
            }

            // Эмуляция работы принтера
            Console.WriteLine($"Начало работы принтера {ThreadName}");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{ThreadName} -> строка - {i}");
            }
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:FFFFFFF")}: {ThreadName} - Печать закончена.");
        }

        /// <summary>
        /// Получить версию драйвера.
        /// </summary>
        public void GetVersion()
        {
            if(_com == null)
            {
                Console.WriteLine("Не удалось получить версию - com объект не загружен.");
                return;
            }

            string version = _com.version();
            Console.WriteLine(version);
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

    }
}
