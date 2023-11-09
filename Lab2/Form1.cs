using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.GoodsGroups". При необходимости она может быть перемещена или удалена.
            //this.goodsGroupsTableAdapter.Fill(this.shopDataSet.GoodsGroups);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=kronus;Initial Catalog=catalog;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(); // Создание команды
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM GOODS";

            string result = "";
            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.Clear();
            }
           
            while (reader.Read())
            {
                result = reader.GetValue(1) + "";
                listBox1.Items.Insert(0, result);

          
            }
            reader.Close();
            con.Close();


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void findGoodsAmountBtn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=kronus;Initial Catalog=catalog;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("SELECT COUNT (*) FROM Goods", con);

            con.Open();

            labelGoodsAmount.Text = "Количество товаров: " + (int)command.ExecuteScalar(); ;
            con.Close();

        }

       

        private void addGoodBtn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=kronus;Initial Catalog=catalog;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand com = new SqlCommand("insertsGood", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@name", textBox1.Text);
            com.Parameters.AddWithValue("@groupId", textBox2.Text);

            com.Parameters.Add(new SqlParameter("@newID", SqlDbType.Int, 4));
            com.Parameters["@newID"].Direction = ParameterDirection.Output;
            try
            {
                com.ExecuteNonQuery();
                int empID = (int)com.Parameters["@newID"].Value;  // Вновь сгенерированный идентификатор
                labelNewGoodid.Text = "Новому товару присвоен ID: " + empID.ToString();

            }
            finally
            {

                con.Close();
            }

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=kronus;Initial Catalog=catalog;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string sqlExpression = $"DELETE FROM GOODS WHERE name = \'{listBox1.SelectedItem.ToString()}\'";
            SqlCommand com = new SqlCommand(sqlExpression, con);
            com.ExecuteNonQuery();

            SqlCommand command = new SqlCommand(); // Создание команды
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM GOODS";

            string result = "";

            SqlDataReader reader = command.ExecuteReader();


            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.Clear();
            }

            while (reader.Read())
            {
                result = reader.GetValue(1) + "";

                listBox1.Items.Insert(0, result);
            }

            reader.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Registrationcs login = new Registrationcs();
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=kronus;Initial Catalog=catalog;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(); // Создание команды
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT name, groupId FROM GOODS WHERE name = @SearchText";
            command.Parameters.AddWithValue("@SearchText", textBoxSearch.Text);

            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            listBoxSearchResults.Items.Clear(); 

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int groupId = reader.GetInt32(1);
                    string result = $" {name}, Группа: {groupId}";
                    listBoxSearchResults.Items.Add(result);
                }
            }
            else
            {
                // Нет совпадений
                MessageBox.Show("Товары не найдены!","Info",MessageBoxButtons.OK);
            }

            reader.Close();
            con.Close();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSearch__TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1__TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
