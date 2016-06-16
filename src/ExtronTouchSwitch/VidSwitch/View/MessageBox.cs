using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidSwitch.View
{
    class MessageBox
    {
        public async static void Show(string message)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(message, "Error");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { Id = 0 });

            var result = await dialog.ShowAsync();
        }
    }
}
