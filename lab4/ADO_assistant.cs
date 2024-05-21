using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using lab4.Data;

namespace lab4
{
    public class ADO_assistant
    {
        readonly String connectionString = System.Configuration.
            ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;

        DataTable dt = null;
        public DataTable TableLoad()
        {
            if (dt != null) return dt;
            dt = new DataTable();

            using (SqlConnection сonnection = new SqlConnection(connectionString))
            {
                SqlCommand command = сonnection.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandText = "Select Id, Name, Phone, Address, Income, Spendings from Clients";
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка підключення до БД");
                }
            }
            return dt;
        }

        public void AddClient(ClientDTO client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "Insert into Clients (Name, Phone, Address, Income, Spendings) values (@Name, @Phone, @Address, @Income, @Spendings)";
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Address", client.Address);
                command.Parameters.AddWithValue("@Income", client.Income);
                command.Parameters.AddWithValue("@Spendings", client.Spendings);

                try
                {
                    conn.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if(affectedRows == 0)
                    {
                        MessageBox.Show("Помилка в доданні клієнта");
                    }
                }
                catch(Exception) 
                {
                    MessageBox.Show("Помилка в доданні клієнта");
                }

            }
        }

        public void UpdateClient(ClientDTO client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "Update Clients set Name = @Name, Phone = @Phone, Address = @Address, Income = @Income, Spendings = @Spendings where Id = @Id";
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Address", client.Address);
                command.Parameters.AddWithValue("@Income", client.Income);
                command.Parameters.AddWithValue("@Spendings", client.Spendings);
                command.Parameters.AddWithValue("@Id", client.Id);

                try
                {
                    conn.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        MessageBox.Show("Помилка в Id клієнта");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка в Id клієнта");
                }

            }
        }

        public void DeleteClient(int clientId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "Delete from Clients where Id = @Id";
                command.Parameters.AddWithValue("@Id", clientId);

                try
                {
                    conn.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        MessageBox.Show("Помилка в Id клієнта");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка в Id клієнта");
                }

            }
        }

    }
}
