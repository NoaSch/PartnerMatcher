using PartnerMatcher.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
    /// Interaction logic for findWin.xaml
    /// </summary>
    public partial class findWin : Window
    {
        string chosenKind;
        string chosenArea;
        public BusLogic busLogic;

        OleDbConnection con;
        DataTable dt;

        public findWin(BusLogic buslog)
        {
            this.busLogic = buslog;
            InitializeComponent();
            comboBoxKind.ItemsSource = buslog.getKinds();
            comboBoxArea.ItemsSource = busLogic.getAreas();
            lblCount.Visibility = System.Windows.Visibility.Hidden;
            gvData.Visibility = System.Windows.Visibility.Hidden;
            gvDataFree.Visibility = System.Windows.Visibility.Hidden;

            con = new OleDbConnection();
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

        }



        private void comboBoxKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chosenKind = comboBoxKind.SelectedValue.ToString(); ;
        }

        private void comboBoxArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chosenArea = comboBoxArea.SelectedItem.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            lblCount.Visibility = System.Windows.Visibility.Hidden;
            gvDataFree.Visibility = System.Windows.Visibility.Hidden;
            gvData.Visibility = System.Windows.Visibility.Hidden;
            BindGrid();
        }

        //find all matches in the db 
        private void BindGrid()
        {
            if (chosenKind == null || chosenArea == null)
            {
                MessageBox.Show("Please select kind of activity and area");
                return;
            }

            bool found = false;
            dt = new DataTable();
            //execute the find operation for the payed adds and recive the results
            busLogic.find(chosenArea, chosenKind, ref dt, true);
            gvData.ItemsSource = dt.AsDataView();


            if (dt.Rows.Count > 0)
            {
                found = true;
                lblCount.Visibility = System.Windows.Visibility.Hidden;
                gvData.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                gvData.Visibility = System.Windows.Visibility.Hidden;
            }

            dt = new DataTable();

            //execute the find operation for the free adds and recive the results
            busLogic.find(chosenArea, chosenKind, ref dt, false);
            gvDataFree.ItemsSource = dt.AsDataView();


            if (dt.Rows.Count > 0)
            {

                gvDataFree.Visibility = System.Windows.Visibility.Visible;

            }
            else if (!found)
            {
                lblCount.Visibility = System.Windows.Visibility.Visible;
                gvDataFree.Visibility = System.Windows.Visibility.Hidden;
            }

        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The Service is Unavailable");
        }

        private void gvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
