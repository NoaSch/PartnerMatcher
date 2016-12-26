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
        string Username;
        public userAndPass()
        {
            InitializeComponent();
           
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (passxt.Text == "" || mailTxt.Text == "")
            {
                System.Windows.MessageBox.Show("All fields are mandatory", "Error");

            }

            else
            {
                OleDbConnection conn = new OleDbConnection();

                // conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\noa\Dropbox\תיקייה משותפת ניתוץ\עבודה 3\GUI\PartnerMatcher\PartnerMatcher\Database.mdb"
                conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb"

  ;

                Username = mailTxt.Text;
                string Pass = passxt.Text;

                //OleDbCommand cmd = new OleDbCommand("INSERT into Passwords (UserName, Pass) values(@Username, @Pass)");
                OleDbCommand cmd = new OleDbCommand();
                // cmd.CommandText = "INSERT into Passwords (UserName, Pass) values(" + Username + ", " + Pass + ")";
                cmd.CommandText = "INSERT into Passwords (UserName, Pass) values(@Username, @Pass)";
                cmd.Connection = conn;

                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    cmd.Parameters.Add("@UserName", OleDbType.VarChar).Value = Username;
                    cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = Pass;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Added");
                        MailMessage mail = new MailMessage("noasch4@gmail.com", Username);
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "smtp.google.com";
                        mail.Subject = "this is a test email.";
                        mail.Body = "this is my test email body";
                        // client.Send(mail);
                        conn.Close();
                    }
                    catch (OleDbException ex)
                    {
                        MessageBox.Show(ex.Source);
                        MessageBox.Show(ex.Message);
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Connection Failed");
                }
            }
        }
    }

}

