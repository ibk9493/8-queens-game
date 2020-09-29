using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace MyAttemptAtQueens
{
    /// <summary>
    /// Interaction logic for ExitPage.xaml
    /// </summary>
    public partial class ExitPage : Page
    {

        public SqlConnection con;
        public ExitPage()
        {
            
            string constring = "Data Source = DESKTOP-I5DML83\\SQLEXPRESS; Integrated Security = True";
            con = new SqlConnection(constring);
            con.Open();

            InitializeComponent();
            DataTable data = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM UserScore", con);
            ad.Fill(data);
            dt.ItemsSource = data.DefaultView;

        }
    }
}
