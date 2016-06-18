using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidSwitch.Model;
using Windows.UI.Core;

namespace VidSwitch.Service
{
    [RestController(InstanceCreationType.Singleton)]
    public class PresetsRESTController
    {
        // This totally sucks, but I can't figure out how to inject into the instance
        public static Settings Settings { get; set; }

        public static string AccessKey { get; set; }

        public class PresetsResponse
        {
            public int selectedPreset { get; set; }

            public Preset[] presets { get; set; }
        }

        public class Preset
        {
            public int index { get; set; }
            public string name { get; set; }
        }

        [UriFormat("/presets")]
        public GetResponse GetPresets()
        {
            PresetsResponse response = new PresetsResponse();
            response.selectedPreset = Settings.SelectedPreset;

            List<Preset> presetList = new List<Preset>();
            for (int i = 0; i < Settings.NumPresets; i++)
            {
                string name = Settings.Presets[i];
                if (name != null && name.Length > 0)
                {
                    presetList.Add(new Preset() { index = i + 1, name = Settings.Presets[i] });
                }
            }
            response.presets = presetList.ToArray();

            return new GetResponse(GetResponse.ResponseStatus.OK, response);
        }

        [UriFormat("/presets/select/{index}")]
        public async Task<GetResponse> SelectPreset(int index)
        {
            try
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () => { Settings.SelectedPreset = index; }
                    );

                return GetPresets();
            }
            catch(ArgumentException e)
            {
                return new GetResponse(GetResponse.ResponseStatus.NotFound);
            }
        }


    }
}
