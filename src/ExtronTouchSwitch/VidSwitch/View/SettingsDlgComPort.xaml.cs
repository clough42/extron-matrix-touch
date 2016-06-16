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
    public sealed partial class SettingsDlgComPort : ContentDialog
    {
        private Settings settings;

        public SettingsDlgComPort(Settings settings)
        {
            this.DataContext = this.settings = settings;

            this.InitializeComponent();
        }

        private void ContentDialog_SaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string selection = this.portComboBox.SelectedItem as string;
            if( selection != null && selection.Length > 0 )
            {
                this.settings.ComPort = selection;
            }
        }

    }
}
