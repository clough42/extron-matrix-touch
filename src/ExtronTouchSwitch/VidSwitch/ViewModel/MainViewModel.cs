using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidSwitch.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.PresetChoice = new ChoiceViewModel("Preset",
                new string[]
                {
                    "Loop All",
                    "PC Countdown",
                    "PC Presentation",
                    "DVD 1",
                    "Camera",
                    "Transition",
                    "PC Practice",
                    "Satellite",
                    "DVD 2 / TV",
                    "Imag Split",
                    "Mac Presentation",
                    "Mac Countdown",
                    "Mac Practice",
                    "",
                    "",
                    ""
                });

            this.PreviewChoice = new ChoiceViewModel("Preview",
                new String[]
                {
                    "Production PC",
                    "Loop PC",
                    "DVD 1",
                    "Camera 1",
                    "DVD 2 / TV",
                    "Satellite",
                    "Mac",
                    ""
                });

           /* this.Outputs = new String[]
            {
                "Left Projector",
                "Right Projector",
                "Rear Projector",
                "Lobby Right",
                "Lobby Right-Middle",
                "Lobby Left-Middle",
                "Lobby Left",
                ""
            };*/
        }

        public ChoiceViewModel PresetChoice { get; set; }

        public ChoiceViewModel PreviewChoice { get; set; }

    }
}
