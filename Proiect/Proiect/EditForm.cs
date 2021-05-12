using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class EditForm : Form
    {
        User user;

        public EditForm(User user)
        {
            InitializeComponent();

            this.user = user;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            tbLastName.Text = user.LastName;
            tbFirstName.Text = user.FirstName;
            tbAge.Text = user.Age.ToString();
            tbComputerName.Text = user.PCName;
            tbId.Text = user.ID.ToString();

            if (user.Read == true)
                ReadYes.Checked = true;
            else
                ReadNo.Checked = true; 

            if (user.Write == true)
                WriteYes.Checked = true;
            else
                WriteNo.Checked = true;

            if (user.Execute == true)
                ExecuteYes.Checked = true;
            else
                ExecuteNo.Checked = true;

            tbAccess.Text = user.AccessLevel;
            tbCity.Text = user.City;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            user.LastName = tbLastName.Text.Trim();
            user.FirstName = tbFirstName.Text.Trim();
            user.Age = Convert.ToInt32(tbAge.Text);
            user.PCName = tbComputerName.Text.Trim();
            user.ID = Convert.ToInt32(tbId.Text);
            user.UserID = user.ID;
            user.UserIDv2 = user.ID;

            if(ReadYes.Checked == true)
                user.Read = true;
            if (ReadNo.Checked == true)
                user.Read = false;

            if(WriteYes.Checked == true)
                user.Write = true;
            if (WriteNo.Checked == true)
                user.Write = false;

            if(ExecuteYes.Checked == true)
                user.Execute = true;
            if (ExecuteNo.Checked == true)
                user.Execute = false;
        }
    }
}
