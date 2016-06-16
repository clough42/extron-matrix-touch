using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace VidSwitch.Service
{
    class SerialPort
    {
        private string portName;
        private SerialDevice serialDevice;
        private DataWriter writer;

        public uint BaudRate { get; set; }

        public SerialPort(String portName) {
            this.portName = portName;
        }

        public async void Open()
        {
            this.serialDevice = await SerialDevice.FromIdAsync(this.portName);
            this.serialDevice.BaudRate = this.BaudRate;
            this.serialDevice.DataBits = 8;
            this.serialDevice.StopBits = SerialStopBitCount.One;
            this.serialDevice.Parity = SerialParity.None;
            this.serialDevice.Handshake = SerialHandshake.None;

            this.writer = new DataWriter(serialDevice.OutputStream);
        }

        public async void Write(String content)
        {
            this.writer.WriteString(content);
            await this.writer.StoreAsync();
        }
            

        public void Close() {
            if (this.serialDevice != null)
            {
                this.serialDevice.Dispose();
                this.serialDevice = null;
            }
        }

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
