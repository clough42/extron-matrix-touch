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
    public sealed partial class SettingsDlgInputs : ContentDialog
    {
        private Settings settings;

        public SettingsDlgInputs(Settings settings)
        {
            this.DataContext = this.settings = settings;
            this.InitializeComponent();
        }

        private void ContentDialog_SaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string[] values = new string[]
            {
                this.inputBox0.Text,
                this.inputBox1.Text,
                this.inputBox2.Text,
                this.inputBox3.Text,
                this.inputBox4.Text,
                this.inputBox5.Text,
                this.inputBox6.Text,
                this.inputBox7.Text
            };

            for( int i=0; i < values.Length; i++ )
            {
                values[i] = values[i].Trim();
                if( values[i].Length == 0 )
                {
                    values[i] = null;
                }
            }

            this.settings.Inputs = values;
        }

    }
}
