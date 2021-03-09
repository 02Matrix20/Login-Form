using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForSQL
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
           

            textBox1.MaxLength = 10;

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            


            if (textBox1.TextLength < 10)
            {
                MessageBox.Show("Довжина логіну менше допустимої. Мінімальна довжина 10 символу.");
                return;

            }
            

            if (textBox2.TextLength < 6)
            {
                MessageBox.Show("Довжина пароля менше допустимої. Мінімальна довжина 6 символу.");
                return;

            }

            else
                     if (textBox2.TextLength > 100)
            {
                MessageBox.Show("Довжина пароля перевищує допустиму. Максимальна довжина 100 символів.");
                return;

            }
            string connStr = "server=localhost; port=3306; username=root;  database=lab3;";
            string sql = "SELECT * FROM `users` WHERE `phone number` = @un AND  `pass`= @up";


            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand(sql, conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters.Add("@up", MySqlDbType.VarChar, 25);

            string pass = GetHash(textBox2.Text); 

            command.Parameters["@un"].Value = textBox1.Text;
            command.Parameters["@up"].Value = pass;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else MessageBox.Show("Такого аккаунту не існує!");
            

            conn.Close();

            
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}
