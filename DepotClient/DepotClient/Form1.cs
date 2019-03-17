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

namespace DepotClient
{
    public partial class Form1 : Form
    {
        string connectionString;
        public MySqlConnection connection = new MySqlConnection();

        private static int GetException(MySqlException ex)
        {
            if (ex.InnerException != null && ex.InnerException is MySqlException)
            {
                return ((MySqlException)ex.InnerException).Number;
            }
            else
            {
                return ex.Number;
            }
        }

        private bool OpenConnection(string login, string password)
        {
            connectionString = string.Format("Database=depot;Data Source=localhost;User Id={0};Password={1}",
                login, password);
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                switch (GetException(ex))
                {
                    case 1042:
                        MessageBox.Show("Отсутствует подключение к серверу");
                        break;
                    case 1045:
                        MessageBox.Show("Доступ запрещен");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Form1()
        {
            InitializeComponent();
            AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            if (OpenConnection(login, password))
            {
                this.Hide();
                Form2 mainWindow = new Form2(connection);
                mainWindow.Show();
            }
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            Application.Exit();
        }
    }
}
