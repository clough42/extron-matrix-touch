using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VidSwitch.Model;
using VidSwitch.Service;
using VidSwitch.View;
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
        private Settings settings;
        private SwitchController controller;

        public MainPage()
        {
            this.settings = new Settings();
            this.settings.OverridesChanged += new OverridesChangedHandler(settings_OverridesChanged);
            this.controller = new SwitchController(this.settings);

            this.DataContext = this.settings;
            this.InitializeComponent();

            new ExclusiveToggleButtonSet(
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
                this.presetButton15
                ).ItemSelected += new ItemSelectedHandler(preset_ItemSelected);

            new ExclusiveToggleButtonSet(
                this.previewButton0,
                this.previewButton1,
                this.previewButton2,
                this.previewButton3,
                this.previewButton4,
                this.previewButton5,
                this.previewButton6,
                this.previewButton7
                ).ItemSelected += new ItemSelectedHandler(preview_ItemSelected);

        }

        private void preview_ItemSelected(int choice)
        {
            settings.SelectedPreview = choice + 1;
        }

        private void preset_ItemSelected(int choice)
        {
            settings.SelectedPreset = choice + 1;
        }

        private void settings_OverridesChanged(Settings settings)
        {
            
        }

        private async void comPortButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsDlgComPort dlg = new SettingsDlgComPort(this.settings);
            var result = await dlg.ShowAsync();
        }

        private async void inputsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsDlgInputs dlg = new SettingsDlgInputs(this.settings);
            var result = await dlg.ShowAsync();
        }

        private async void outputsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsDlgOutputs dlg = new SettingsDlgOutputs(this.settings);
            var result = await dlg.ShowAsync();
        }

        private async void presetsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsDlgPresets dlg = new SettingsDlgPresets(this.settings);
            var result = await dlg.ShowAsync();
        }

        private async void overridesButton_Click(object sender, RoutedEventArgs e)
        {
            OverridesDlg dlg = new OverridesDlg(this.settings);
            var result = await dlg.ShowAsync();
        }

        private async void helpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpDialog dlg = new HelpDialog();
            await dlg.ShowAsync();
        }
    }
}
