using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace WinForSQL
{
    public partial class RegisterForm : Form
    {
       
        public RegisterForm()
        {
            InitializeComponent();
            textBox1.Text = "Введіть ім'я";
            textBox1.ForeColor = Color.Gray;
            

            textBox2.Text = "Введіть вік";
            textBox2.ForeColor = Color.Gray;
           

            textBox3.Text = "Введіть e-mail";
            textBox3.ForeColor = Color.Gray;
           

            textBox4.Text = "Введіть номер";
            textBox4.ForeColor = Color.Gray;
            textBox4.MaxLength = 10;

            textBox5.Text = "Введіть пароль";
            textBox5.ForeColor = Color.Gray;
            
        }
        private string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
        private void button1_Click(object sender, EventArgs e)
        {

           
            if (textBox1.Text == "Введіть ім'я")
            {
                MessageBox.Show("Введіть ім'я");
                return;
            }

            if (textBox1.TextLength < 1)
            {
                MessageBox.Show("Довжина ім'я менше допустимої. Мінімальна довжина 1 символу.");
                return;

            }

            else
                     if (textBox1.TextLength > 1000)
            {
                MessageBox.Show("Довжина ім'я перевищує допустиму. Максимальна довжина 1000 символів.");
                return;

            }

            if (textBox2.Text == "Введіть вік")
            {
                MessageBox.Show("Введіть вік");
                return;
            }

            int age;
            bool ishourValid = int.TryParse(textBox2.Text, out age);

            if (age < 16)
            {
                MessageBox.Show(" Мінімальний вік 16!");
                return;

            }

            else
                    if (textBox2.TextLength > 100)
            {
                MessageBox.Show("Максимальний вік 100");
                return;

            }


            if (textBox3.Text == "Введіть e-mail ")
            {
                MessageBox.Show("Введіть e-mail");
                return;
            }
            if (textBox3.TextLength < 10)
            {
                MessageBox.Show("Довжина e-mail менше допустимої. Мінімальна довжина 10 символу.");
                return;

            }

            else
                    if (textBox3.TextLength > 1000)
            {
                MessageBox.Show("Довжина e-mail перевищує допустиму. Максимальна довжина 1000 символів.");
                return;

            }




            if (textBox4.Text == "Введіть номер")
            {
                MessageBox.Show("Введіть номер");
                return;
            }
            if (textBox4.TextLength < 10)
            {
                MessageBox.Show("Довжина номеру менше допустимої. Мінімальна довжина 10 символу.");
                return;

            }
            else
                    if (textBox5.TextLength > 10)
            {
                MessageBox.Show("Довжина номеру перевищує допустиму. Максимальна довжина 10 символів.");
                return;

            }



            if (textBox5.Text == "Введіть пароль")
            {
                MessageBox.Show("Введіть пароль");
                return;
            }
            if (textBox5.TextLength < 7)
            {
                MessageBox.Show("Довжина пароля менше допустимої. Мінімальна довжина 7 символу.");
                return;
          
            }

            else
                    if (textBox5.TextLength > 100)
            {
                MessageBox.Show("Довжина пароля перевищує допустиму. Максимальна довжина 1000 символів.");
                return;
               
            }



            if (chekUsers())
            {
                return;
            }

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`phone number`, `pass`, `name`, `age`, `mail`) VALUES (@phnumber, @pass, @name, @age,@mail);", db.GetConnection()) ;

            string pass = GetHash(textBox5.Text);
            command.Parameters.Add("@phnumber", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@age", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = textBox3.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт створено");
            else
                MessageBox.Show("Аккаунт не створено");
            db.closeConnection();
        }

        public Boolean chekUsers()
        {
          

            MySqlConnection conn = new MySqlConnection("server=localhost; port=3306; username=root;  database=lab3;");
            conn.Open();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `phone number` = @un ", conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            

            command.Parameters["@un"].Value = textBox4.Text;
          

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такий логін вже існує, введіть інший");
                return true;
            }
            else return false;

        }

        Point lastPoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введіть ім'я")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введіть ім'я";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Введіть вік")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть вік";
                textBox2.ForeColor = Color.Gray;
            }
        }

       

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Введіть e-mail")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введіть e-mail";
                textBox3.ForeColor = Color.Gray;
            }
        }
       
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Введіть пароль")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Введіть пароль";
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Введіть номер")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Введіть номер";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
