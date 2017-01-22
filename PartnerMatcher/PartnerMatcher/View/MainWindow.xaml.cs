using PartnerMatcher.Logic;
using System;
using System.Windows;


namespace PartnerMatcher.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       public string user;
        public string userMail;  
        //the business locic 
        BusLogic model;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("test");
            model = new BusLogic();

        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            connWin cw = new connWin(model);
            cw.ShowDialog();
            if (cw.conf == true)
            {

                userName.Content = cw.usr;
                user = cw.usr;
                userMail = cw.userMail; 

            }


        }

        private void crateAcc_Click(object sender, RoutedEventArgs e)
        {
            userAndPass createWin = new userAndPass(model);
            createWin.Show();



        }

        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");

        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            findWin fw = new findWin(model, user, userMail);
            fw.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void myAddsBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void myActivitiesBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void myApplicationsBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }


        private void PrefernecesBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void myApplicationsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void myAddsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void myActivitiesBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");

        }

        private void myProfClick(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }
    }
}
