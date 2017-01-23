using PartnerMatcher.Logic;
using System;
using System.Collections.Generic;
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

namespace PartnerMatcher.View
{
    /// <summary>
    /// Interaction logic for requestWin.xaml
    /// </summary>
    public partial class postAdWin : Window
    {
        BusLogic busLogic;
        public bool addreq;
        string userMail;
        string activityLine;        
        string activityName; //[1]
        string activityArea; //[2]
        string activityKind; //[3]
        Dictionary<int, string> userActivities;

        public postAdWin(BusLogic logic, string mail)
        {
            InitializeComponent();
            busLogic = logic;
            userMail = mail;
            EnableContent(false);
            userActivities = busLogic.GetUserActivities(userMail);
            ShowActivities();
            //activityID = activityId;
            //this.chosenKind = chosenKind;
            //this.adId = adId;
            //this.AdvertiserMail = AdvertiserMail; 
        }

        private void ShowActivities()
        {
            List<string> list = new List<string>();
            if (userActivities.Keys.Count == 0) //no activities
            {
                list.Add("You don't have any activities yet. please create one first :)");
            }
            else
            {
                list.Add("<Activity Name>\t<Kind>\t<CreationDate>");
                foreach (int activityID in userActivities.Keys)
                {
                    list.Add(userActivities[activityID]);
                }
            }
            ActivitiesListBox.ItemsSource = list;          
        }

        private void EnableContent(bool isEnable)
        {
            additionalInfoTextBox.IsEnabled = isEnable;
            kindTextBox.IsEnabled = isEnable;
            areaInfoTextBox.IsEnabled = isEnable;
        }
        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            //busLogic.applyRequest(askerMail, activityID, chosenKind, adId, content, AdvertiserMail);
            //addreq = true;
            Close();
        }

        private void activityButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActivitiesListBox.SelectedItem==null || ActivitiesListBox.SelectedIndex == 0)
                return;
            activityLine = ActivitiesListBox.SelectedItem.ToString();
            string[] splitted = activityLine.Split('\t');
            activityName = splitted[0];
            activityArea = splitted[1];
            activityKind = splitted[2];
            kindTextBox.Text = activityKind;
            areaInfoTextBox.Text = activityArea;
            additionalInfoTextBox.IsEnabled = true;
        }
    }
}
