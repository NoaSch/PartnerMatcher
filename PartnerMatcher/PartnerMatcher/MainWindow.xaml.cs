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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PartnerMatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("test");
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            connWin cw = new connWin();
            cw.ShowDialog();
            if (cw.conf == true)
                userName.Content = cw.Username;

        }

        private void crateAcc_Click(object sender, RoutedEventArgs e)
        {
            userAndPass createWin = new userAndPass();
            createWin.Show();


        }

        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yed");

        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            findWin fw = new findWin();
            fw.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yed");
        }
    }
}
