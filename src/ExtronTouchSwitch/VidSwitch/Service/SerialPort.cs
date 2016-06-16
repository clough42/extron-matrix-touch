using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace VidSwitch.Service
{
    class SerialPort
    {
        public int BaudRate { get; set; }

        public SerialPort(String portName) { }

        public void Open() { }

        public void Write(String content) { }

        public void Close() { }

        public static async Task<string[]> GetPortNames()
        {
            List<string> idList = new List<string>();

            var selector = SerialDevice.GetDeviceSelector();
            var devices = await DeviceInformation.FindAllAsync(selector);
            foreach (var device in devices)
            {
                idList.Add(device.Id);
            }

            return idList.ToArray();
        }
    }
}
