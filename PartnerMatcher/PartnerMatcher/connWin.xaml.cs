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
        public string usr;
        public bool conf;
        public connWin()
        {
            InitializeComponent();
            conf = false;
        }


        private void connect_Click(object sender, RoutedEventArgs e)
        {
            {
                if (passBox.Password == "" || mailTxt.Text == "")
                {
                    System.Windows.MessageBox.Show("All fields are mandatory", "Error");

                }

                else
                {

                    OleDbConnection conn = new OleDbConnection();

                    // conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\noa\Dropbox\תיקייה משותפת ניתוץ\עבודה 3\GUI\PartnerMatcher\PartnerMatcher\Database.mdb"
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
                    conn.Open();
                    OleDbDataReader reader = null;
                    //  OleDbDataAdapter da = new OleDbDataAdapter("select  * from tblmedic where medicalschool ='" + combobox.text.ToString().Trim() + "'", conn);

                    OleDbCommand command = new OleDbCommand("SELECT Pass, FullName from Profiles WHERE  mail ='" + mailTxt.Text.ToString().Trim() + "'", conn);
                    // OleDbCommand command = new OleDbCommand("SELECT * from  Profiles WHERE mail='@1'", conn);
                    //command.Parameters.AddWithValue("@1", mail);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string ans = reader[0].ToString();
                        if (ans != passBox.Password)
                        {
                            System.Windows.MessageBox.Show("Wrong Password", "Error");
                        }
                        else
                        {
                            conf = true;
                            usr = reader[1].ToString();
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
                command.Parameters.AddWithValue("@1", mail);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListboxItems.Add(reader[1].ToString() + "," + reader[2].ToString());
                } 
         */
    }
}
