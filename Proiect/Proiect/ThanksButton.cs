using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    class ThanksButton : Button
    {
        protected override void OnClick(EventArgs e)
        {
            MessageBox.Show("Thank you for using this app! This app was developed by Vlad Gont.",
                "Info", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
