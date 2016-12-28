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
    /// Interaction logic for AddProfileWin.xaml
    /// </summary>
    public partial class AddProfileWin : Window
    {
        int age;
        string name;
        bool? smoke;
        public string mail;
        public string pass;
        public AddProfileWin()
        {
            InitializeComponent();
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || ageTxt.Text == "")
            {
                System.Windows.MessageBox.Show("All fields are mandatory", "Error");

            }
            else if (!int.TryParse(ageTxt.Text, out age))
            {
                System.Windows.MessageBox.Show("Enter a valid age in natural number", "Error");
            }
            else
            {
                name = nameTextBox.Text;
                smoke = checkBoxSmoke.IsChecked;
                OleDbConnection conn = new OleDbConnection();

                // conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\noa\Dropbox\תיקייה משותפת ניתוץ\עבודה 3\GUI\PartnerMatcher\PartnerMatcher\Database.mdb"
                conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";




                //OleDbCommand cmd = new OleDbCommand("INSERT into Profiles (mail, Pass) values(@mail, @Pass)");
                OleDbCommand cmd = new OleDbCommand();
                // cmd.CommandText = "INSERT into Profiles (mail, Pass) values(" + mail + ", " + Pass + ")";
                cmd.CommandText = "INSERT into Profiles (mail, Pass, age, smoke, fullName) values(@mail, @Pass,@age, @smoke,@name)";
                cmd.Connection = conn;

                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    cmd.Parameters.Add("@mail", OleDbType.VarChar).Value = mail;
                    cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = pass;
                    cmd.Parameters.Add("@age", OleDbType.Integer).Value = age;
                    cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                    cmd.Parameters.Add("@name", OleDbType.VarChar).Value = name;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Added");
                        /*MailMessage mailMsg = new MailMessage("PartnersMatcher@gmail.com", mail);
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "smtp.google.com";
                        mailMsg.Subject = "this is a test email.";
                        mailMsg.Body = "this is my test email body";

                        client.Send(mailMsg);*/
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

