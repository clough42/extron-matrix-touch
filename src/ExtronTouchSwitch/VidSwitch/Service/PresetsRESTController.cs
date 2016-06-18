using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidSwitch.Model;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Core;

namespace VidSwitch.Service
{
    [RestController(InstanceCreationType.Singleton)]
    public class PresetsRESTController
    {
        // This totally sucks, but I can't figure out how to inject into the instance
        public static Settings Settings { get; set; }

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

        [UriFormat("/presets/select/{index}?hash={hash}&timestamp={timestamp}")]
        public async Task<GetResponse> SelectPresetX(int index, int timestamp, string hash)
        {
            return await SelectPreset(index, timestamp, hash);
        }

        [UriFormat("/presets/select/{index}?timestamp={timestamp}&hash={hash}")]
        public async Task<GetResponse> SelectPreset(int index, int timestamp, string hash)
        {
            // first, authorize the request
            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int timeDiff = Math.Abs(timestamp - unixTimestamp);
            
            string authStr = String.Format("/api/presets/select/{0} {1} {2}", index, timestamp, Settings.AccessKey);
            string hashStr = Hash(authStr);

            if( hashStr != hash || timeDiff > 300 /* allow 5 minutes clock misalignment */)
            {
                return new GetResponse(GetResponse.ResponseStatus.NotFound);
            }

            // now, carry it out
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

        private string Hash(string inStr)
        {
            // Convert the message string to binary data.
            IBuffer buffUtf8Msg = CryptographicBuffer.ConvertStringToBinary(inStr, BinaryStringEncoding.Utf8);

            // Create a HashAlgorithmProvider object.
            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);

            // Hash the message.
            IBuffer buffHash = objAlgProv.HashData(buffUtf8Msg);

            // Convert the hash to a string (for display).
            String strHashBase64 = CryptographicBuffer.EncodeToHexString(buffHash);

            // Return the encoded string
            return strHashBase64;
        }
    }
}
