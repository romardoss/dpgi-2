using lab4.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;


namespace lab4
{
    public partial class CreateClientWindow : Window
    {
        public CreateClientWindow()
        {
            InitializeComponent();
            ADO_assistant aDO_Assistant = new ADO_assistant();
            List<ClientDTO> clients = new List<ClientDTO>();
            var dt = aDO_Assistant.TableLoad();
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
            IdText.Text = (clients.Last().Id + 1).ToString();
        }

        private void SubmitCreateButton_Click(object sender, RoutedEventArgs e)
        {
            ADO_assistant aDO_Assistant = new ADO_assistant();
            ClientDTO clientDTO = new ClientDTO
            {
                Id = int.Parse(IdText.Text),
                Name = NameText.Text,
                Phone = PhoneText.Text,
                Address = AddressText.Text,
                Income = int.Parse(IncomeText.Text),
                Spendings = int.Parse(SpendText.Text),
            };

            aDO_Assistant.AddClient(clientDTO);

            Close();
        }
    }
}
