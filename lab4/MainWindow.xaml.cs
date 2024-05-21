using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;
using MySqlX.XDevAPI.Relational;
using lab4.Data;

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DataTable dt;
        readonly List<ClientDTO> clients;

        public MainWindow()
        {
            InitializeComponent();

            clients = new List<ClientDTO>();
            ADO_assistant myTable = new ADO_assistant();
            dt = myTable.TableLoad();

            list.SelectedIndex = 0;
            list.Focus();
            list.DataContext = dt;

            fillClientDto(dt);

        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void fillClientDto(DataTable dt)
        {
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
    }
}
