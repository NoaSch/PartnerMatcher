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
                System.Windows.MessageBox.Show("Thish e-mail already in the system", "Error");

            }
            else
            {
                string mail = mailTxt.Text;
                string Pass = passBox.Password;
                AddProfileWin apw = new AddProfileWin();
                apw.mail = mail;
                apw.pass = Pass;
                apw.ShowDialog();
            }


            /*   {
                  OleDbConnection conn = new OleDbConnection();

                   // conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\noa\Dropbox\תיקייה משותפת ניתוץ\עבודה 3\GUI\PartnerMatcher\PartnerMatcher\Database.mdb"
                   conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb"

     ;

                   mail = mailTxt.Text;
                   string Pass = passxt.Text;

                   //OleDbCommand cmd = new OleDbCommand("INSERT into Profiles (mail, Pass) values(@mail, @Pass)");
                   OleDbCommand cmd = new OleDbCommand();
                   // cmd.CommandText = "INSERT into Profiles (mail, Pass) values(" + mail + ", " + Pass + ")";
                   cmd.CommandText = "INSERT into Profiles (mail, Pass) values(@mail, @Pass)";
                   cmd.Connection = conn;

                   conn.Open();

                   if (conn.State == ConnectionState.Open)
                   {
                       cmd.Parameters.Add("@mail", OleDbType.VarChar).Value = mail;
                       cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = Pass;

                       try
                       {
                           cmd.ExecuteNonQuery();
                           MessageBox.Show("Data Added");
                           MailMessage mailMsg = new MailMessage("PartnersMatcher@gmail.com", mail);
                          SmtpClient client = new SmtpClient();
                           client.Port = 25;
                           client.DeliveryMethod = SmtpDeliveryMethod.Network;
                           client.UseDefaultCredentials = false;
                           client.Host = "smtp.google.com";
                           mailMsg.Subject = "this is a test email.";
                           mailMsg.Body = "this is my test email body";

                           // client.Send(mailMsg);
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
               }*/
        }

        private bool checkExistMail(string text)
        {
            OleDbConnection conn = new OleDbConnection();

            // conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\noa\Dropbox\תיקייה משותפת ניתוץ\עבודה 3\GUI\PartnerMatcher\PartnerMatcher\Database.mdb"
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.Open();
            OleDbDataReader reader = null;
            //  OleDbDataAdapter da = new OleDbDataAdapter("select  * from tblmedic where medicalschool ='" + combobox.text.ToString().Trim() + "'", conn);

            OleDbCommand command = new OleDbCommand("SELECT * from Profiles WHERE  mail ='" + mailTxt.Text.ToString().Trim() + "'", conn);
            // OleDbCommand command = new OleDbCommand("SELECT * from  Profiles WHERE mail='@1'", conn);
            //command.Parameters.AddWithValue("@1", mail);
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

