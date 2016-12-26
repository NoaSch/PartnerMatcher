using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using System.Windows.Shapes;

namespace PartnerMatcher
{
    /// <summary>
    /// Interaction logic for connWin.xaml
    /// </summary>
    public partial class connWin : Window
    {
        public string Username;
        public bool conf;
        public connWin()
        {
            InitializeComponent();
            conf = false;
        }


        private void connect_Click(object sender, RoutedEventArgs e)
        {
            {
                if (passxt.Text == "" || mailTxt.Text == "")
                {
                    System.Windows.MessageBox.Show("All fields are mandatory", "Error");

                }

                else
                {
                    Username = mailTxt.Text;

                    OleDbConnection conn = new OleDbConnection();

                    // conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\noa\Dropbox\תיקייה משותפת ניתוץ\עבודה 3\GUI\PartnerMatcher\PartnerMatcher\Database.mdb"
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
                    conn.Open();
                    OleDbDataReader reader = null;
                    //  OleDbDataAdapter da = new OleDbDataAdapter("select  * from tblmedic where medicalschool ='" + combobox.text.ToString().Trim() + "'", conn);

                    OleDbCommand command = new OleDbCommand("SELECT Pass from Passwords WHERE  UserName ='" + mailTxt.Text.ToString().Trim() + "'", conn);
                    // OleDbCommand command = new OleDbCommand("SELECT * from  Passwords WHERE UserName='@1'", conn);
                    //command.Parameters.AddWithValue("@1", Username);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string ans = reader[0].ToString();
                        if (ans != passxt.Text)
                        {
                            System.Windows.MessageBox.Show("Wrong Password", "Error");
                        }
                        else
                        {
                            conf = true;
                            Close();

                        }
                    }

                    conn.Close();



                }
            }

        }

        /*
         *
         *
         *  connection.Open();
                OleDbDataReader reader = null;
                OleDbCommand command = new OleDbCommand("SELECT * from  Users WHERE LastName='@1'", connection);
                command.Parameters.AddWithValue("@1", userName);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListboxItems.Add(reader[1].ToString() + "," + reader[2].ToString());
                } 
         */
    }
}
