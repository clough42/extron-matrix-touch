using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VidSwitch.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VidSwitch.View
{
    public sealed partial class SettingsDlgOutputs : ContentDialog
    {
        private Settings settings;

        public SettingsDlgOutputs(Settings settings)
        {
            this.DataContext = this.settings = settings;
            this.InitializeComponent();
        }

        private void ContentDialog_SaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string[] values = new string[]
            {
                this.outputBox0.Text,
                this.outputBox1.Text,
                this.outputBox2.Text,
                this.outputBox3.Text,
                this.outputBox4.Text,
                this.outputBox5.Text,
                this.outputBox6.Text
            };

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                if (values[i].Length == 0)
                {
                    values[i] = null;
                }
            }

            this.settings.Outputs = values;
        }
    }
}
