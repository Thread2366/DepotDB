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
    public partial class Form3 : Form
    {
        DataGridView mainWindowGrid;
        DataTable mainWindowTable;
        MySqlConnection connection;
        MySqlCommand result;
        string tableName;

        public Form3(MySqlConnection conn, string currentTable,
            DataGridView grid, DataTable table, MySqlCommand searchQuery)
        {
            InitializeComponent();
            connection = conn;
            tableName = currentTable;
            mainWindowGrid = grid;
            mainWindowTable = table;
            result = searchQuery;

            dataGridView1.Columns.Add("attributes", "Атрибуты");
            dataGridView1.Columns.Add("values", "Значения");
            foreach (DataGridViewColumn c in mainWindowGrid.Columns)
            {
                dataGridView1.Rows.Add(c.HeaderText, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder queryText = new StringBuilder("SELECT * FROM ");
            queryText.AppendFormat("{0} WHERE 1 = 1", tableName);
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                string searchValue = r.Cells[1].Value.ToString();
                if (searchValue != "")
                {
                    queryText.AppendFormat(" AND {0} {1}",
                    mainWindowTable.Columns[r.Index].Caption,
                    searchValue);
                }
            }
            result.Connection = connection;
            result.CommandText = queryText.ToString();
            this.Close();
        }
    }
}
