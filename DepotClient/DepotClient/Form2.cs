using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DepotClient
{
    public partial class Form2 : Form
    {
        Form1 authorizationWindow = new Form1();

        private Dictionary<string, string> translations = new Dictionary<string, string>();
        private string currentTable;
        
        private MySqlConnection connection;
        private DataTable table = new DataTable();

        public Form2(MySqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
        }
        
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            authorizationWindow.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            FillTranslations(translations);
            MySqlCommand command = new MySqlCommand("SHOW TABLES", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(SeekTranslation(translations, reader.GetValue(0).ToString()));
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand searchQuery = new MySqlCommand();
            Form3 searchWindow = new Form3(connection, currentTable, dataGridView1, table, searchQuery);
            searchWindow.ShowDialog();
            if (searchQuery.Connection != null)
            {
                try
                {
                    MySqlDataReader reader = searchQuery.ExecuteReader();
                    table.Clear();
                    table.Load(reader);
                    dataGridView1.DataSource = table;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.HeaderText = SeekTranslation(translations, column.HeaderText);
                    }
                    dataGridView1.Refresh();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader reader;
            command.Connection = connection;
            if (comboBox1.SelectedItem != null)
            {
                string tableName = SeekTranslation(translations, comboBox1.SelectedItem.ToString());
                command.CommandText = (string.Format("SELECT * FROM depot.{0}", tableName));
                reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
                dataGridView1.DataSource = table;
                currentTable = tableName;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderText = SeekTranslation(translations, column.HeaderText);
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
        }
        
        object primaryKey;

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            primaryKey = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
        }

        private bool IsEditable = true;

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (IsEditable)
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                var commandString = new StringBuilder();
                commandString.AppendFormat("UPDATE {0} SET {1}=@value WHERE {2}=@id",
                    currentTable,
                    table.Columns[e.ColumnIndex].Caption,
                    table.Columns[0].Caption);
                command.CommandText = commandString.ToString();
                command.Parameters.AddWithValue("@value", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                command.Parameters.AddWithValue("@id", primaryKey);
                command.ExecuteNonQuery();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount == 0) return;
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            var commandString = new StringBuilder();
            commandString.AppendFormat("INSERT INTO {0} VALUES(", currentTable);
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                commandString.AppendFormat("default, ");
            }
            commandString.Length -= 2;
            commandString.Append(")");
            command.CommandText = commandString.ToString();
            command.ExecuteNonQuery();
            button2_Click(this, new EventArgs());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount == 0) return;
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            var commandString = new StringBuilder();
            commandString.AppendFormat("DELETE FROM {0} WHERE ", currentTable);
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                commandString.AppendFormat("{0}={1} OR ", dataGridView1.Columns[0].HeaderText, row.Cells[0].Value);
            }
            commandString.Length -= 3;
            command.CommandText = commandString.ToString();
            command.ExecuteNonQuery();
            button2_Click(this, new EventArgs());
        }
        
        private static void FillTranslations(Dictionary<string, string> translations)
        {
            translations.Add("RegNumber", "Номер вагона");
            translations.Add("RegName", "Приписка к дороге");
            translations.Add("RegChief", "Приписка к дирекции");
            translations.Add("Type", "Тип вагона");
            translations.Add("TypeYear", "Год выпуска");
            translations.Add("TypeRepair", "Тип ремонта");
            translations.Add("Picture", "Фотография");
            translations.Add("Money", "Стоимость ремонта");
            translations.Add("Bonus", "Качество ремонта");
            translations.Add("BonusPercent", "Премия в процентах");
            translations.Add("DateStart", "Начало ремонта");
            translations.Add("DateStop", "Окончание ремонта");
            translations.Add("Reason", "Причина поступления в ремонт");
            translations.Add("External", "Внешняя/местная ЖД");
            translations.Add("BankExternal", "Банк внешней ЖД");
            translations.Add("InnExternal", "ИНН внешней ЖД");
            translations.Add("AddressExternal", "Юридический адрес внешней ЖД");
            translations.Add("FIOchief", "ФИО бригадира");
            translations.Add("Base", "Образование бригадира (ВУЗ)");
            translations.Add("FIOworker", "ФИО работника");
            translations.Add("BaseWorker", "Образование работника (ВУЗ)");
            translations.Add("YearWorker", "Стаж работы");
            translations.Add("SpecialWorker", "Основная специальность работника");
            translations.Add("BonusWorker", "Премия в рублях работнику");
            translations.Add("Comment", "Примечания (за что премия)");
            translations.Add("NumberBankCard", "Номер карты");
            translations.Add("BrigadeID", "Номер бригады");
            translations.Add("RailwayCarriageID", "Номер вагона");
            translations.Add("WorkerID", "Номер рабочего");

            translations.Add("awards", "Премии");
            translations.Add("brigades", "Бригады");
            translations.Add("railway_carriages", "Вагоны");
            translations.Add("repair_works", "Ремонтные работы");
            translations.Add("workers", "Рабочие");
            translations.Add("Премии", "awards");
            translations.Add("Бригады", "brigades");
            translations.Add("Вагоны", "railway_carriages");
            translations.Add("Ремонтные работы", "repair_works");
            translations.Add("Рабочие", "workers");

        }

        private static string SeekTranslation(Dictionary<string, string> translations, string str)
        {
            if (translations.ContainsKey(str))
                return translations[str];
            else
                return str;
        }
    }
}
