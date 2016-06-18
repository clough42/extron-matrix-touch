using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using VidSwitch.Service;

namespace VidSwitch.Model
{

    public delegate void ComPortChangedHandler(Settings settings);
    public delegate void PresetsChangedHandler(Settings settings);
    public delegate void InputsChangedHandler(Settings settings);
    public delegate void OutputsChangedHandler(Settings settings);
    public delegate void SelectedPresetChangedHandler(Settings settings);
    public delegate void SelectedPreviewChangedHandler(Settings settings);
    public delegate void OverridesChangedHandler(Settings settings);

    /// <summary>
    /// The MatrixControlBar settings object.  This class persists the settings
    /// for the matrix control bar, and it has events so other parts of the system
    /// can be notified when settings change.
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        const int NUM_PRESETS = 16;
        const int NUM_INPUTS = 8;
        const int NUM_OUTPUTS = 7;

        private string comPort = null;
        private string[] presets = null;
        private string[] inputs = null;
        private string[] outputs = null;
        private int selectedPreset;
        private int selectedPreview;
        private int[] overrides;

        /// <summary>
        /// Create a new instance of the Settings object.
        /// </summary>
        public Settings()
        {
            this.comPort = null;
            this.presets = null;
            this.inputs = null;
            this.outputs = null;
            this.selectedPreset = 0;
            this.selectedPreview = 0;

            this.ComPortChanged = null;
            this.InputsChanged = null;
            this.OutputsChanged = null;
            this.PresetsChanged = null;
            this.SelectedPresetChanged = null;
            this.SelectedPreviewChanged = null;
            this.OverridesChanged = null;

            LoadSettings();

            LoadValidComPorts();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The com port where the matrix switch is connected
        /// </summary>
        public string ComPort
        {
            get { return this.comPort; }
            set
            {
                this.comPort = value;
                if (this.ComPortChanged != null)
                {
                    this.ComPortChanged(this);
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ComPort"));
                }
                SaveSettings();
            }
        }

        /// <summary>
        /// Event: triggered whenever the com port is changed
        /// </summary>
        public ComPortChangedHandler ComPortChanged;

        public string[] ValidComPorts { get; private set; }

        private async void LoadValidComPorts()
        {
            // check to see if there's only one, and if so, use it
            this.ValidComPorts = await SerialPort.GetPortNames();   
        }

        /// <summary>
        /// The number of presets on the switch
        /// </summary>
        public int NumPresets
        {
            get { return NUM_PRESETS; }
        }

        /// <summary>
        /// The number of inputs on the switch
        /// </summary>
        public int NumInputs
        {
            get { return NUM_INPUTS; }
        }

        /// <summary>
        /// The number of outputs on the switch
        /// </summary>
        public int NumOutputs
        {
            get { return NUM_OUTPUTS; }
        }


        /// <summary>
        /// The array of presets.  This is a zero-indexed array (0:n-1) and represents
        /// the presets 1-n.  Null values indicate that the preset is not used.  Non-null
        /// values are the name of the corresponding preset.  Presets[n-1] contains the name
        /// of preset n.
        /// </summary>
        public string[] Presets
        {
            get
            {
                if (this.presets == null)
                {
                    this.presets = new string[NUM_PRESETS];
                }
                return this.presets;
            }
            set
            {
                if (value.Length != NUM_PRESETS)
                {
                    throw new ArgumentException("Presets array is the wrong length");
                }
                bool changed = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (this.presets[i] != value[i])
                    {
                        changed = true;
                    }
                }
                if (changed)
                {
                    this.presets = value;
                    if (this.PresetsChanged != null)
                    {
                        this.PresetsChanged(this);
                    }
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Presets"));
                    }
                    SaveSettings();
                }
            }

        }

        /// <summary>
        /// Event: triggered when the list of presets is changed
        /// </summary>
        public PresetsChangedHandler PresetsChanged;


        /// <summary>
        /// The array of inputs.  This is a zero-indexed array (0:n-1) and represents
        /// the inputs 1-n.  Null values indicate that the input is not used.  Non-null
        /// values are the name of the corresponding input.  Inputs[n-1] contains the name
        /// of input n.
        /// </summary>
        public string[] Inputs
        {
            get
            {
                if (this.inputs == null)
                {
                    this.inputs = new string[NUM_INPUTS];
                }
                return this.inputs;
            }
            set
            {
                if (value.Length != NUM_INPUTS)
                {
                    throw new ArgumentException("Inputs array is the wrong length");
                }
                bool changed = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (this.inputs[i] != value[i])
                    {
                        changed = true;
                    }
                }
                if (changed)
                {
                    this.inputs = value;
                    if (this.InputsChanged != null)
                    {
                        this.InputsChanged(this);
                    }
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Inputs"));
                    }
                    SaveSettings();
                }
            }
        }

        public InputsChangedHandler InputsChanged;

        /// <summary>
        /// The array of outputs.  This is a zero-indexed array (0:n-1) and represents
        /// the outputs 1-n.  Null values indicate that the output is not used.  Non-null
        /// values are the name of the corresponding outputs.  Outputs[n-1] contains the name
        /// of output n.
        /// </summary>
        public string[] Outputs
        {
            get
            {
                if (this.outputs == null)
                {
                    this.outputs = new string[NUM_OUTPUTS];
                }
                return this.outputs;
            }
            set
            {
                if (value.Length != NUM_OUTPUTS)
                {
                    throw new ArgumentException("Outputs array is the wrong length");
                }
                bool changed = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (this.outputs[i] != value[i])
                    {
                        changed = true;
                    }
                }
                if (changed)
                {
                    this.outputs = value;
                    if (this.OutputsChanged != null)
                    {
                        this.OutputsChanged(this);
                    }
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Outputs"));
                    }
                    SaveSettings();
                }
            }
        }

        public OutputsChangedHandler OutputsChanged;

        /// <summary>
        /// The selected (1-based) preset.  This is a number.  If the SelectedPreset is
        /// n, then Presets[n-1] contains the name of the preset.
        /// </summary>
        public int SelectedPreset
        {
            get
            {
                return selectedPreset;
            }
            set
            {
                if (value < 1 || value > this.NumPresets || this.Presets[value - 1] == null)
                {
                    throw new ArgumentException("Preset " + value + " is not in the list of valid presets");
                }
                this.selectedPreset = value;
                if (this.SelectedPresetChanged != null)
                {
                    this.SelectedPresetChanged(this);
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedPreset"));
                }
            }
        }

        public SelectedPresetChangedHandler SelectedPresetChanged;


        public int SelectedPreview
        {
            get
            {
                return this.selectedPreview;
            }
            set
            {
                if (this.Inputs[value - 1] == null)
                {
                    throw new ArgumentException("Input " + value + " is not in the list of inputs");
                }
                this.selectedPreview = value;
                if (this.SelectedPreviewChanged != null)
                {
                    this.SelectedPreviewChanged(this);
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedPreview"));
                }
            }
        }

        public SelectedPreviewChangedHandler SelectedPreviewChanged;

        /// <summary>
        /// Overrides.  These correspond to the outputs.  Array element (n-1) corresponds to
        /// output n.  The value of the override is 0 if there is no override active, and n
        /// to indicate that input n should go to that output.
        /// </summary>
        public int[] Overrides
        {
            get
            {
                if (this.overrides == null)
                {
                    this.overrides = new int[NUM_OUTPUTS];
                }
                return this.overrides;
            }
            set
            {
                if (value.Length != NUM_OUTPUTS)
                {
                    throw new ArgumentException("Overrides array is the wrong length");
                }
                bool changed = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (this.overrides[i] != value[i])
                    {
                        changed = true;
                    }
                }
                if (changed)
                {
                    this.overrides = value;
                    if (this.OverridesChanged != null)
                    {
                        this.OverridesChanged(this);
                    }
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Overrides"));
                        PropertyChanged(this, new PropertyChangedEventArgs("OverridesActive")); // for binders
                    }
                }
            }
        }

        /// <summary>
        /// OverridesActive.  Returns true if there are currently any overrides configured in the
        /// system.
        /// </summary>
        public bool OverridesActive
        {
            get
            {
                foreach (int i in Overrides)
                {
                    if (i != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public OverridesChangedHandler OverridesChanged;

        const string BOISEFIRST = "Boise First";
        const string MEDIASUITE = "Media Suite";
        const string MATRIXCONTROL = "Matrix Control Bar";
        const string COMPORT = "ComPort";
        const string PRESET = "Preset";
        const string INPUT = "Input";
        const string OUTPUT = "Output";

        private void SaveSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values[COMPORT] = this.comPort;
            for (int i = 0; i < this.Presets.Length; i++)
            {
                localSettings.Values[PRESET + (i + 1)] = this.Presets[i];
            }
            for (int i = 0; i < this.Inputs.Length; i++)
            {
                localSettings.Values[INPUT + (i + 1)] = this.Inputs[i];
            }
            for (int i = 0; i < this.Outputs.Length; i++)
            {
                localSettings.Values[OUTPUT + (i + 1)] = this.Outputs[i];
            }
        }

        private void LoadSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            this.comPort = localSettings.Values[COMPORT] as string;

            for (int i = 0; i < this.Presets.Length; i++)
            {
                this.Presets[i] = localSettings.Values[PRESET + (i + 1)] as string;
            }
            for (int i = 0; i < this.Inputs.Length; i++)
            {
                this.Inputs[i] = localSettings.Values[INPUT + (i + 1)] as string;
            }
            for (int i = 0; i < this.Outputs.Length; i++)
            {
                this.Outputs[i] = localSettings.Values[OUTPUT + (i + 1)] as string;
            }
        }

    }
}
