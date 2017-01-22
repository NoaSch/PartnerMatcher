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
        string activityName;
        string activityArea;
        string activityKind;
        List<string> userActivities;

        int adId;
        string AdvertiserMail;
        public postAdWin(BusLogic logic, string mail)
        {
            InitializeComponent();
            busLogic = logic;
            userMail = mail;
            userActivities = busLogic.GetUserActivities(userMail);
            ActivitiesListBox.ItemsSource = userActivities;
            //activityID = activityId;
            //this.chosenKind = chosenKind;
            //this.adId = adId;
            //this.AdvertiserMail = AdvertiserMail; 
        }


        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            //busLogic.applyRequest(askerMail, activityID, chosenKind, adId, content, AdvertiserMail);
            //addreq = true;
            Close();
        }

        private void activityButton_Click(object sender, RoutedEventArgs e)
        {
            activityName= ActivitiesListBox.SelectedItem.ToString();
        }
    }
}
