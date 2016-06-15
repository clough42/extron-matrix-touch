using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidSwitch.ViewModel
{
    public class ChoiceViewModel
    {
        public ChoiceViewModel(String name, String[] choices)
        {
            this.Name = name;
            this.Choices = choices;
        }

        public String Name { get; set; }

        public String[] Choices { get; set; }

        public int Selected { get; set; }

    }
}
