using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace VidSwitch.View
{
    public delegate void ItemSelectedHandler(int choice);

    public class ExclusiveToggleButtonSet
    {
        private ToggleButton[] buttons;
        private int selected = -1;

        public ExclusiveToggleButtonSet(params ToggleButton[] buttons)
        {
            this.buttons = buttons;

            foreach( ToggleButton button in buttons )
            {
                button.Checked += Button_Checked;
                button.Unchecked += Button_UnChecked;
            }
        }

        public ItemSelectedHandler ItemSelected;

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
                        if (this.selected != i)
                        {
                            this.selected = i;
                            if( this.ItemSelected != null )
                            {
                                this.ItemSelected(this.selected);
                            }
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
                    if (button == buttons[i] && this.selected == i)
                    {
                        button.IsChecked = true; // turn it back on
                    }
                }
            }
        }
    }
}
