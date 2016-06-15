using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidSwitch.ViewModel;
using Windows.UI.Xaml.Controls.Primitives;

namespace VidSwitch.View
{
    public class ExclusiveToggleButtonSet
    {
        private ToggleButton[] buttons;
        private ChoiceViewModel choice;

        public ExclusiveToggleButtonSet(ChoiceViewModel choice, params ToggleButton[] buttons)
        {
            this.choice = choice;
            this.buttons = buttons;

            foreach( ToggleButton button in buttons )
            {
                button.Checked += Button_Checked;
                button.Unchecked += Button_UnChecked;
            }
        }

        private void Button_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;
            if (button != null)
            {
                // mark the new currently-selected button
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (button == buttons[i])
                    {
                        if (this.choice.Selected != i)
                        {
                            this.choice.Selected = i;
                        }
                    }
                }

                // and deselect the others
                for (int i = 0; i < buttons.Length; i++)
                {
                    if( button != buttons[i] ) 
                    {
                        buttons[i].IsChecked = false;
                    }
                }
            }
        }

        private void Button_UnChecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;
            if (button != null)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    // if the selected button is turned off
                    if (button == buttons[i] && this.choice.Selected == i)
                    {
                        button.IsChecked = true; // turn it back on
                    }
                }
            }
        }
    }
}
