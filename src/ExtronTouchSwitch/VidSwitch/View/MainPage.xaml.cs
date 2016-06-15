using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VidSwitch.View;
using VidSwitch.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VidSwitch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            this.DataContext = this.viewModel = new MainViewModel();

            this.InitializeComponent();

            new ExclusiveToggleButtonSet(
                this.viewModel.PresetChoice,
                this.presetButton0,
                this.presetButton1,
                this.presetButton2,
                this.presetButton3,
                this.presetButton4,
                this.presetButton5,
                this.presetButton6,
                this.presetButton7,
                this.presetButton8,
                this.presetButton9,
                this.presetButton10,
                this.presetButton11,
                this.presetButton12,
                this.presetButton13,
                this.presetButton14,
                this.presetButton15);

            new ExclusiveToggleButtonSet(
                this.viewModel.PreviewChoice,
                this.previewButton0,
                this.previewButton1,
                this.previewButton2,
                this.previewButton3,
                this.previewButton4,
                this.previewButton5,
                this.previewButton6,
                this.previewButton7
                );
        }

        private void PresetButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
