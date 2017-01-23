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

        
        public bool applyRequest(string askerMail, int activityID, string chosenKind,int  adId,string  content, string AdvertiserMail)
        {
            sendMailToUser(askerMail, "Your request has been send", "Your request has been send to the partners in the activity and the Advertiser, wait for response.");
            sendMailToUser(AdvertiserMail, "You get request to join for one of actovities you advertise ", askerMail+ " asks to join for the activity. the content of the request is: " + content + ". all the partners in the activity got mail with request for ranking it. ");
           List<string> members= data.getMembersActivity(activityID);
            DateTime localDate = DateTime.Now;
            foreach (string item in members)
            {
                sendMailToUser(item, "someone request to join for one of your actovities", askerMail + " asks to join for one of your the activities. the content of the request is: " + content +". please rank his request. ");
            }
            int status = 1;
           return data.saveRequest(askerMail, localDate, activityID, chosenKind, adId, content, status); 
        }

        public void AdvancedSearchDates(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string gender, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {
             data.AdvancedSearchDates(chosenArea,chosenKind, ref  dt,  payed,minAge,  maxAge,  gender,  smoke,  kosher, quiet,  animals, play); 
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

        internal List<string> GetUserActivities(string userMail)
        {
            return data.getMemberActivities(userMail);
        }
    }
}

