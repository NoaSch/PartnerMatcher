using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PartnerMatcher
{
    /// <summary>
    /// Interaction logic for userAndPass.xaml
    /// </summary>
    public partial class userAndPass : Window
    {
        //string mail;
        public userAndPass()
        {
            InitializeComponent();

        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (passBox.Password == "" || mailTxt.Text == "")
            {
                System.Windows.MessageBox.Show("All fields are mandatory", "Error");

            }

            else if (checkExistMail(mailTxt.Text))
            {
                System.Windows.MessageBox.Show("This e-mail is already in the system", "Error");

            }
            else
            {
                string mail = mailTxt.Text;
                string Pass = passBox.Password;
                AddProfileWin apw = new AddProfileWin();
                apw.mail = mail;
                apw.pass = Pass;
                apw.ShowDialog();
                this.Close();
            }

        }

        private bool checkExistMail(string text)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.Open();
            OleDbDataReader reader = null;
            OleDbCommand command = new OleDbCommand("SELECT * from Profiles WHERE  mail ='" + mailTxt.Text.ToString().Trim() + "'", conn);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

    }
}

