using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBCourse
{
    public partial class Authorization : Form
    {
        Registration frm3;
        public Boolean authorized;
        public string userLogin = "";
        private DataTables data;

        public Authorization(DataTables dt)
        {
            InitializeComponent();
            data = dt;
            passwordBox.PasswordChar = '*';
            authorized = false;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            String login = loginBox.Text.Trim();
            String password = passwordBox.Text.Trim();
            users us = data.CheckAuthorization(login, password);
            if (us != null)
            {
                authorizationLbl.Text = "";
                authorized = true;
                userLogin = login;
                Close();
            }
            else
                authorizationLbl.Text = "Неверная пара логин/пароль";
        }

        private void newPersonBtn_Click(object sender, EventArgs e)
        {
            if (frm3 == null)
            {
                frm3 = new Registration(data);
                frm3.FormClosed += frm3_FormClosed;
            }
            frm3.Show(this);
            Hide();
        }

        public void frm3_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm3 = null;
            Show();
            data.SetTables();
        }

    }
}
