using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComObject.Devices
{
    internal interface IDevice
    {
        string ThreadName { get; }
        void Print(int numberString, int threadSleep);
        void GetVersion(string numberString);
        bool DisposeDriver();
    }
}
