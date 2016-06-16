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
    public sealed partial class OverridesDlg : ContentDialog
    {
        private Settings settings;

        public OverridesDlg(Settings settings)
        {
            this.DataContext = this.settings = settings;
            this.InitializeComponent();
        }

        private void ContentDialog_SaveButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int[] values = new int[]
            {
                overrideBox0.SelectedIndex,
                overrideBox1.SelectedIndex,
                overrideBox2.SelectedIndex,
                overrideBox3.SelectedIndex,
                overrideBox4.SelectedIndex,
                overrideBox5.SelectedIndex,
                overrideBox6.SelectedIndex
            };

            settings.Overrides = values;
        }

        private void clearAllButton_Click(object sender, RoutedEventArgs e)
        {
            overrideBox0.SelectedIndex =
                overrideBox1.SelectedIndex =
                overrideBox2.SelectedIndex =
                overrideBox3.SelectedIndex =
                overrideBox4.SelectedIndex =
                overrideBox5.SelectedIndex =
                overrideBox6.SelectedIndex = 0;
        }
    }
}
