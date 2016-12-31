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
        bool? kosher;
        bool? quiet;
        bool? animals;
        bool? play;
        string gender;

        public AddProfileWin()
        {
            InitializeComponent();
            comboBoxGen.Items.Add("male");
            comboBoxGen.Items.Add("female");
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

                gender = comboBoxGen.SelectedValue.ToString();
            //get all matching rows
            name = nameTextBox.Text;
            smoke = checkBoxSmoke.IsChecked;
            kosher = checkBoxKosher.IsChecked;
            quiet = checkBoxquiet.IsChecked;
            animals = checkBoxAnimals.IsChecked;
            play = checkBoxPlay.IsChecked;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into Profiles (mail, Pass, age, gender, smoke, fullName, kosher, quiet, animals, play) values(@mail, @Pass,@age,@gender, @smoke,@name, @kosher, @quiet,@animals,@play)";
            cmd.Connection = conn;

            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@mail", OleDbType.VarChar).Value = mail;
                cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = pass;
                cmd.Parameters.Add("@age", OleDbType.Integer).Value = age;
                cmd.Parameters.Add("@gender", OleDbType.VarChar).Value = gender;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = name;
                cmd.Parameters.Add("@kosher", OleDbType.Boolean).Value = kosher;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = quiet;
                cmd.Parameters.Add("@animals", OleDbType.Boolean).Value = animals;
                cmd.Parameters.Add("@play", OleDbType.Boolean).Value = play;

                try
                {
                    //send the mail to the user's mail
                    cmd.ExecuteNonQuery();
                    MailMessage mailMsg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mailMsg.From = new MailAddress("partnersmatcherapp@gmail.com");
                    mailMsg.To.Add(mail);
                    mailMsg.Subject = name + ", Welcome to Partners Matcher";
                    mailMsg.Body = "wish you to find the second part of your Activity";

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("partnersmatcherapp@gmail.com", "p12345678");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mailMsg);
                    MessageBox.Show("User Created");
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
                MessageBox.Show("DB Error call support");
            }
            Close();
        }

        private void comboBoxGen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gender = comboBoxGen.SelectedValue.ToString();
        }
    }

}

