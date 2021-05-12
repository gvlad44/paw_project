using ControlsLibrary;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Proiect
{

    public partial class AddForm : Form
    {
        List<User> users;
        private string connectionString = "Data Source=database.db";

        public AddForm()
        {
            InitializeComponent();
            users = new List<User>();
        }

        private void DisplayUsers()
        {
            tbLastName.Clear();
            tbFirstName.Clear();
            tbAge.Clear();
            tbComputerName.Clear();
            tbId.Clear();
            tbAccess.Clear();
            tbCity.Clear();
            lvUsers.Items.Clear();
            ReadNo.Checked = ReadYes.Checked = false;
            WriteNo.Checked =WriteYes.Checked = false;
            ExecuteNo.Checked = ExecuteYes.Checked = false;

            foreach (User user in users)
            {
                ListViewItem lvi = new ListViewItem(user.LastName);
                lvi.SubItems.Add(user.FirstName);
                lvi.SubItems.Add(user.Age.ToString());
                lvi.SubItems.Add(user.ID.ToString());
                lvi.SubItems.Add(user.PCName);
                
                 
                if (user.Read == true)
                    lvi.SubItems.Add("Yes");
                if (user.Read == false)
                    lvi.SubItems.Add("No");
                if (user.Write == true)
                    lvi.SubItems.Add("Yes");
                if (user.Write == false)
                    lvi.SubItems.Add("No");
                if (user.Execute == true)
                    lvi.SubItems.Add("Yes");
                if (user.Execute == false)
                    lvi.SubItems.Add("No");


                lvi.SubItems.Add(user.AccessLevel);
                lvi.SubItems.Add(user.City);

                lvi.Tag = user;

                lvUsers.Items.Add(lvi);
            }

            lbUserCount.Text = users.Count.ToString();

        }

       

        public void btnAdd_Click(object sender, EventArgs e)
        {
            string ln = tbLastName.Text.Trim();
            string fn = tbFirstName.Text.Trim();
            int a;
            try
            {
                 a = Convert.ToInt32(tbAge.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                a = 0;

            }
            string pc = tbComputerName.Text.Trim();
            int id ;
            try
            {
                id = Convert.ToInt32(tbId.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                id = 0;
            }

            bool r = false;
            if (ReadYes.Checked == true)
                r = true;
            if (ReadNo.Checked == true)
                r = false;                                               
           

            bool w = false;
            if (WriteYes.Checked == true)
                w = true;
            if (WriteNo.Checked == true)
                w = false;

            bool exe = false;
            if (ExecuteYes.Checked == true)
                exe = true;
            if (ExecuteNo.Checked == true)
                exe = false;

            string acc = tbAccess.Text.Trim();
            string cityy = tbCity.Text.Trim();

            if (!ValidateChildren())
            {
                MessageBox.Show("Form contains errors!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }

            try
            {
                int var = 0;
                if (ReadNo.Checked == true && ReadYes.Checked == true)
                    var = 1;
                if (ReadNo.Checked == false && ReadYes.Checked == false)
                    var = 1;
                if (var == 1)
                    throw new CheckBoxException("You must choose one checkbox of this respective field! " +
                       "Now the program will assign 'NO' to the respective right.");
            }
            catch (CheckBoxException cv)
            {
                MessageBox.Show(cv.mesaj, "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                ReadNo.Checked = true;
                r = false;
            }

            try
            {
                int var = 0;
                if (WriteNo.Checked == true && WriteYes.Checked == true)
                    var = 1;
                if (WriteNo.Checked == false && WriteYes.Checked == false)
                    var = 1;
                if (var == 1)
                    throw new Exception("You must choose one checkbox of this respective field! " +
                       "Now the program will assign 'NO' to the respective right.");
            }
            catch (Exception cv)
            {
                MessageBox.Show(cv.Message, "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                WriteNo.Checked = true;
                w = false;
            }

            try
            {
                int var = 0;
                if (ExecuteNo.Checked == true && ExecuteYes.Checked == true)
                    var = 1;
                if (ExecuteNo.Checked == false && ExecuteYes.Checked == false)
                    var = 1;
                if (var == 1)
                    throw new Exception("You must choose one checkbox of this respective field! " +
                        "Now the program will assign 'NO' to the respective right.");
            }
            catch (Exception cv)
            {
                MessageBox.Show(cv.Message, "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                ExecuteNo.Checked = true;
                exe = false;
            }

            

            if (ValidateChildren())
            {
                User user = new User(ln, fn, a, pc, id, r, w, exe, acc, cityy);
                //users.Add(user);

                try
                {
                    AddUserDB(user);
                    DisplayUsers();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void AddUserDB(User userr)
        {
            var query = "INSERT INTO Users (LastName, FirstName, Age, ID, PCName, Readd, Writee, Executee, AccessLevel, City) " +
                "VALUES (@ln, @fn, @a, @id, @pc, @r, @w, @e, @acc, @cityy);" +
                "" +
                "SELECT last_insert_rowid();";
                    

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {

                    connection.Open();

                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@ln", userr.LastName);
                    command.Parameters.AddWithValue("@fn", userr.FirstName);
                    command.Parameters.AddWithValue("@a", userr.Age);
                    command.Parameters.AddWithValue("@id", userr.ID);
                    command.Parameters.AddWithValue("@pc", userr.PCName);
                    command.Parameters.AddWithValue("@r", userr.Read.ToString());
                    command.Parameters.AddWithValue("@w", userr.Write.ToString());
                    command.Parameters.AddWithValue("@e", userr.Execute.ToString());
                    command.Parameters.AddWithValue("@acc", userr.AccessLevel);
                    command.Parameters.AddWithValue("@cityy", userr.City);

                    Object dbid = command.ExecuteScalar();
                    userr.DBID = (long)dbid;

                    users.Add(userr);

                }            
        }

        private void LoadUserDB()
        {
            var query = "SELECT * FROM Users;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand(query, connection);

                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long dbid = (long)reader["DBID"];
                        string ln = (string)reader["LastName"];
                        string fn = (string)reader["FirstName"];
                        int a = (int)reader["Age"];
                        int id = (int)reader["ID"];
                        string pc = (string)reader["PCName"];


                        bool r;
                        if ((string)reader["Readd"] == "Yes")
                            r = true;
                        else
                            r = false;

                        bool w;
                        if ((string)reader["Writee"] == "Yes")
                            w = true;
                        else
                            w = false;

                        bool exe;
                        if ((string)reader["Executee"] == "Yes")
                            exe = true;
                        else
                            exe = false;

                        string acc = (string)reader["AccessLevel"];
                        string cityy = (string)reader["City"];

                        User user = new User(dbid, ln, fn, a, pc, id, r, w, exe, acc, cityy);
                        users.Add(user);
                    }
                }
            }

        }

        private void DeleteUserDB(User userr)
        {
            string stringSQL = "DELETE FROM Users WHERE DBID=@dbid";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(stringSQL, connection);
                command.Parameters.AddWithValue("@dbid", userr.DBID);

                command.ExecuteNonQuery();

                users.Remove(userr);

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(lvUsers.SelectedItems.Count !=1)
            {
                MessageBox.Show("Select one user!");
            }
            else if(MessageBox.Show("Are you sure?", "Delete", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    ListViewItem lvi = lvUsers.SelectedItems[0];
                    User user = (User)lvi.Tag;
                    DeleteUserDB(user);
                    DisplayUsers();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count != 1)
            {
                MessageBox.Show("Select one user!");
            }
            else
            {
                ListViewItem lvi = lvUsers.SelectedItems[0];
                User user = (User)lvi.Tag;

                EditForm editForm = new EditForm(user);
                if (editForm.ShowDialog() == DialogResult.OK)
                    DisplayUsers();
            }
        }

        private void lvUsers_DoubleClick(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count == 1)
            {
                ListViewItem lvi = lvUsers.SelectedItems[0];
                User user = (User)lvi.Tag;

                EditForm editForm = new EditForm(user);
                if (editForm.ShowDialog() == DialogResult.OK)
                    DisplayUsers();
            }
        }

        private void btnSerializeBinary_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream s = File.Create("serialized.bin"))
            {
                formatter.Serialize(s, users);
            }
        }

        private void btnDeserializeBinary_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream s = File.OpenRead("serialized.bin"))
            {
                users = (List<User>)formatter.Deserialize(s);
                DisplayUsers();
            }
        }

        private void btnSerializeXML_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            using (FileStream stream = File.Create("serialized.xml"))
            {
                serializer.Serialize(stream, users);
            }
        }

        private void btnDeserializeXML_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            using (FileStream stream = File.OpenRead("serialized.xml"))
            {
                users = (List<User>)serializer.Deserialize(stream);
                DisplayUsers();
            }
        }

        private void btnTextFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Save TXT as";
            dialog.Filter = "TXT File  | *.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    writer.WriteLine("LastName,FirstName,Age,ComputerName," +
                        "ID,Read,Write,Execute,AccessLevel,City");

                    foreach (User user in users)
                    {
                        writer.WriteLine($"{user.LastName},{user.FirstName}," +
                            $"{user.Age}, {user.PCName}, {user.ID}, {user.Read}," +
                            $"{user.Write}, {user.Execute}, {user.AccessLevel}, " +
                            $"{user.City}");

                    }
                }
            }
        }

        private void AddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.S)
            {
                btnEdit_Click(sender, e);
            }

            if (e.Alt && e.KeyCode == Keys.D)
            {
                btnDelete_Click(sender, e);
            }
        }

        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count != 1)
            {
                MessageBox.Show("Select one user!");
            }
            else
            {
                ListViewItem lvi = lvUsers.SelectedItems[0];
                User user = (User)lvi.Tag;

                EditForm editForm = new EditForm(user);
                if (editForm.ShowDialog() == DialogResult.OK)
                    DisplayUsers();
            }
        }

            private void AddForm_Load(object sender, EventArgs e)
            {
                try
                {
                    LoadUserDB();
                    DisplayUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void tsBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This app was developed by Vlad Gont.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (tbLastName.Text.Trim().Length < 3)
            {
                errorProvider.SetError(tbLastName, "Name too short!");
                e.Cancel = true;
                tbLastName.Focus();
            }
        }

        private void tbLastName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbLastName, string.Empty);
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (tbFirstName.Text.Trim().Length < 3)
            {
                errorProvider.SetError(tbFirstName, "Name too short!");
                e.Cancel = true;
                tbFirstName.Focus();
            }
        }

        private void tbFirstName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbFirstName, string.Empty);
        }

        private void tbAge_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (tbAge.Text.Trim().Length == 0 || Convert.ToInt32(tbAge.Text) < 18 ||
                    Convert.ToInt32(tbAge.Text) > 120)
                {
                    errorProvider.SetError(tbAge, "Age isn't valid. The age should be" +
                        " between 18 and 120.");
                    e.Cancel = true;
                    tbAge.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Introduce numbers only!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                errorProvider.SetError(tbAge, string.Empty);
                e.Cancel = true;
                tbAge.Focus();
            }
        }

        private void tbAge_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbAge, string.Empty);
        }

        private void tbComputerName_Validating(object sender, CancelEventArgs e)
        {
            if (tbComputerName.Text.Trim().Length < 3)
            {
                errorProvider.SetError(tbComputerName, "Name too short!");
                e.Cancel = true;
                tbComputerName.Focus();
            }
        }

        private void tbComputerName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbComputerName, string.Empty);
        }

        private void tbId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (tbId.Text.Trim().Length == 0 || Convert.ToInt32(tbId.Text) < 0 ||
                    Convert.ToInt32(tbId.Text) > 100)
                {
                    errorProvider.SetError(tbId, "This ID isn't valid. The ID should be" +
                        " between 0 and 100.");
                    e.Cancel = true;
                    tbId.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Introduce numbers only!", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                errorProvider.SetError(tbId, string.Empty);
                e.Cancel = true;
                tbId.Focus();
            }
        }

        private void tbId_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbId, string.Empty);
        }

        private void tbAccess_Validating(object sender, CancelEventArgs e)
        {
            if (tbAccess.Text.Trim().Length < 3)
            {
                errorProvider.SetError(tbAccess, "Not a valid access level!");
                e.Cancel = true;
                tbAccess.Focus();
            }
        }

        private void tbAccess_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbAccess, string.Empty);
        }

        private void tbCity_Validating(object sender, CancelEventArgs e)
        {
            if (tbCity.Text.Trim().Length < 3)
            {
                errorProvider.SetError(tbCity, "Not a valid city!");
                e.Cancel = true;
                tbCity.Focus();
            }
        }

        private void tbCity_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbCity, string.Empty);
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            int noR = 0;
            int noW = 0;
            int noE = 0;

            foreach (User user in users)
            {

                if (user.Read == true)
                    noR++;
                if (user.Write == true)
                    noW++;
                if (user.Execute == true)
                    noE++;
            }

            GraphForm graphForm = new GraphForm(noR, noW, noE);
            graphForm.ShowDialog();
        }
    }

    class CheckBoxException : Exception
    {
        public string mesaj;

        public CheckBoxException()
        {

        }

        public CheckBoxException(string msj)
        {
            mesaj = msj;
        }
    }
}
