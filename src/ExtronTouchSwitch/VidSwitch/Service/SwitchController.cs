using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VidSwitch.Model;
using VidSwitch.View;

namespace VidSwitch.Service
{
    class SwitchController
    {
        private SerialPort port = null;

        public SwitchController(Settings settings)
        {
            settings.ComPortChanged += new ComPortChangedHandler(this.settings_ComPortChanged);
            settings.SelectedPresetChanged += new SelectedPresetChangedHandler(this.settings_SelectedPresetChanged);
            settings.SelectedPreviewChanged += new SelectedPreviewChangedHandler(this.settings_SelectedPreviewChanged);
            settings.OverridesChanged += new OverridesChangedHandler(this.settings_OverridesChanged);
            OpenComPort(settings.ComPort);
        }

        private void settings_ComPortChanged(Settings settings)
        {
            OpenComPort(settings.ComPort);
        }

        private void settings_SelectedPresetChanged(Settings settings)
        {
            SwitchPreset(settings.SelectedPreset, settings.SelectedPreview, settings.Overrides);
        }

        private void settings_SelectedPreviewChanged(Settings settings)
        {
            SwitchPreview(settings.SelectedPreview);
        }

        private void settings_OverridesChanged(Settings settings)
        {
            SwitchPreset(settings.SelectedPreset, settings.SelectedPreview, settings.Overrides);
        }

        /// <summary>
        /// Returns a list of valid com ports on this system.
        /// </summary>
        //public static string[] ValidComPorts
        //{
        //    get
        //    {
        //        return SerialPort.GetPortNames();
        //    }
        //}

        private void OpenComPort(string comPort)
        {
            try
            {
                if (this.port != null)
                {
                    this.port.Close();
                    this.port = null;
                }

                if (comPort != null && comPort.Length > 0)
                {
                    port = new SerialPort(comPort);
                    port.BaudRate = 9600;
                    //port.DataBits = 8;
                    //port.StopBits = StopBits.One;
                    //port.Parity = Parity.None;
                    //port.Handshake = Handshake.None;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR creating serial port\r\n" + e.ToString());
            }
        }

        private void SwitchPreview(int preview)
        {
            string command = String.Format("{0}*8!\r\n", preview);

            SendCommand(command);
        }

        // selects the preset and re-selects the preview in one command
        private void SwitchPreset(int preset, int preview, int[] overrides)
        {
            StringBuilder command = new StringBuilder();
            command.AppendFormat("{0}.", preset);

            // add in any overrides
            for (int i = 0; i < overrides.Length; i++)
            {
                if (overrides[i] > 0)
                {
                    command.AppendFormat("{0}*{1}!", overrides[i], i + 1);
                }
            }

            // add the preview monitor control
            command.AppendFormat("{0}*8!\r\n", preview);
            SendCommand(command.ToString());
        }

        private void SendCommand(string command)
        {
            if (port == null)
            {
                MessageBox.Show("A COM port has not been configured");
            }
            else
            {
                port.Open();
                port.Write(command);
                port.Close();
            }
        }


    }
}
