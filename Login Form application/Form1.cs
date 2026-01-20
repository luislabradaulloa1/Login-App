using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login_Form_application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-8MAP9P3\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True;Encrypt=False");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            String username, password;

            username = usernameValue.Text;
            password = passwordValue.Text;

            try
            {
                string querry = "SELECT * FROM Login_new WHERE username = '"+usernameValue.Text+"' AND password = '"+passwordValue.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = usernameValue.Text;
                    password = passwordValue.Text;

                    MenuForm form2 = new MenuForm();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernameValue.Clear();
                    passwordValue.Clear();

                    usernameValue.Focus();
                }
            }
            catch
            {
                MessageBox.Show("An error occurred while trying to log in.");
            }
            finally
            {
                conn.Close();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            usernameValue.Clear();
            passwordValue.Clear();

            usernameValue.Focus();
        }

        private void ShowPasswordChanged(object sender, EventArgs e)
        {
            passwordValue.PasswordChar = showPasswordCheckBox.Checked ? '\0' : '*';
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
