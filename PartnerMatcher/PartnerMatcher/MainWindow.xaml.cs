using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PartnerMatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusLogic model;
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("test");
            model = new BusLogic();

        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            connWin cw = new connWin();
            cw.ShowDialog();
            if (cw.conf == true)
                userName.Content = cw.usr;

        }

        private void crateAcc_Click(object sender, RoutedEventArgs e)
        {
            userAndPass createWin = new userAndPass();
            createWin.Show();


        }

        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");

        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            findWin fw = new findWin(model);
            fw.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }

        private void myAddsBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }

        private void myActivitiesBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }

        private void myApplicationsBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }


        private void PrefernecesBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }

        private void myApplicationsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }

        private void myAddsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");
        }

        private void myActivitiesBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yet");

        }
    }
}
