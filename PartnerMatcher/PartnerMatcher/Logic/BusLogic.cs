using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PartnerMatcher.Data;
using System.Data;
namespace PartnerMatcher.Logic
{

    //class for the business logic layer
    public class BusLogic
    {

        myData data;

        public BusLogic()
        {
            data = new myData();
        }

        //get the list of the areas
        public List<string> getAreas()
        {
            return data.getAreas();
        }

        //get the List of kinds
        public List<string> getKinds()
        {
            return data.getKinds();
        }

        //send system mail to a user
        public bool sendMailToUser(string mailAddr, string title, string content)
        {
            try
            {
                //send the mail to the user's mail               
                MailMessage mailMsg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mailMsg.From = new MailAddress("partnersmatcherapp@gmail.com");
                mailMsg.To.Add(mailAddr);
                mailMsg.Subject = title;
                mailMsg.Body = content;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("partnersmatcherapp@gmail.com", "p12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //send new user details to the data layer
        public bool addUser(string mail, string pass, int age, string gender, bool? smoke, string name, bool? kosher, bool? quiet, bool? animals, bool? play, string about)
        {
            if (data.AddUserToDB(mail, pass, age, gender, smoke, name, kosher, quiet, animals, play, about))
            {
                sendMailToUser(mail, name + ", Welcome To PartnersMatcher", "Hope you find your second part for your activity");
                return true;
            }

            else
            {
                return false;
            }
        }


        public bool checkIfUseExist(string mail)
        {
            return data.checkIfUserExist(mail);
        }

        public bool checkPassword(string mail, string password)
        {
            bool ans = data.checkPassword(mail, password);
            return ans;
        }

        public string GetName(string mail)
        {
            return data.getName(mail);
        }


        public void find(string chosenArea, string chosenKind, ref DataTable dt, bool payed)
        {
            data.find(chosenArea, chosenKind, ref dt, payed);
        }
    }
}

