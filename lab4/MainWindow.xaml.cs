using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using lab4.Data;

namespace lab4
{
    public partial class MainWindow : Window
    {
        readonly DataTable dt;
        readonly List<ClientDTO> clients;
        readonly ADO_assistant ado;

        public MainWindow()
        {
            InitializeComponent();

            clients = new List<ClientDTO>();
            ado = new ADO_assistant();

            dt = ado.TableLoad();
            UpdateDataList(list, dt);


            list.SelectedIndex = 0;
            list.Focus();
            
        }

        private void UpdateDataList(ListBox list, DataTable dt)
        {
            list.DataContext = ado.TableLoad();
            FillClientDto(dt);
        }

        private void FillClientDto(DataTable dt)
        {
            clients.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ClientDTO dto = new ClientDTO 
                {
                    Id = (int)row["Id"],
                    Name = row["Name"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString(),
                    Income = (int)row["Income"],
                    Spendings = (int)row["Spendings"], 
                };
                clients.Add(dto);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            ado.DeleteClient(clients[list.SelectedIndex].Id);
            UpdateDataList(list, dt);
            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ClientDTO client = clients[list.SelectedIndex];
            // є варіант не використовувати clientDTO в даному випадку, а
            // взяти Id з поля IdText
            client.Name = NameText.Text;
            client.Phone = PhoneText.Text;
            client.Address = AddressText.Text;
            client.Income = int.Parse(IncomeText.Text);
            client.Spendings = int.Parse(SpendText.Text);
            ado.UpdateClient(client);
            UpdateDataList(list, dt);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateClientWindow createClientWindow = new CreateClientWindow();
            createClientWindow.ShowDialog();
            UpdateDataList(list, dt);
        }
    }
}
