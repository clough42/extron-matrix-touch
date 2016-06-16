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
    public sealed partial class SettingsDlgPresets : ContentDialog
    {
        private Settings settings;

        public SettingsDlgPresets(Settings settings)
        {
            this.DataContext = this.settings = settings;
            this.InitializeComponent();
        }

        private void ContentDialog_SaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string[] values = new string[]
{
                this.presetBox0.Text,
                this.presetBox1.Text,
                this.presetBox2.Text,
                this.presetBox3.Text,
                this.presetBox4.Text,
                this.presetBox5.Text,
                this.presetBox6.Text,
                this.presetBox7.Text,
                this.presetBox8.Text,
                this.presetBox9.Text,
                this.presetBox10.Text,
                this.presetBox11.Text,
                this.presetBox12.Text,
                this.presetBox13.Text,
                this.presetBox14.Text,
                this.presetBox15.Text,
};

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                if (values[i].Length == 0)
                {
                    values[i] = null;
                }
            }

            this.settings.Presets = values;
        }

    }
}
