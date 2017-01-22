﻿using PartnerMatcher.Logic;
using System;
using System.Collections;
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
        string user;
        string userMail;
        public BusLogic busLogic;

        OleDbConnection con;
        DataTable dt;

        public findWin(BusLogic buslog, string use, string usermail)
        {

            InitializeComponent();
            this.busLogic = buslog;
            userMail = usermail;
            user = use;
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


        public void AdvancedSearch_Click(object sender, RoutedEventArgs e)
        {

            int mAge;
            int maxiAge;
            bool? smoke;
            bool? kosher;
            bool? quiet;
            bool? animals;
            bool? play;
            string gender;
            if (chosenKind.Equals("Dates"))
            {
                lblCount.Visibility = System.Windows.Visibility.Hidden;
                gvDataFree.Visibility = System.Windows.Visibility.Hidden;
                gvData.Visibility = System.Windows.Visibility.Hidden;
                searchDateWin searchhWin = new searchDateWin(busLogic);
                searchhWin.ShowDialog();
                if (searchhWin.isSearch == true)
                {
                   mAge = searchhWin.mAge;
                    maxiAge = searchhWin.maxiAge;
                    smoke = searchhWin.smoke;
                    kosher = searchhWin.kosher;
                    quiet = searchhWin.quiet;
                    animals = searchhWin.animals;
                    play = searchhWin.play;
                    gender = searchhWin.gender;

                    BindAdvancedGrid(mAge,maxiAge, gender, smoke, kosher, quiet, animals, play);
                     //added = busLogic.AdvancedSearchDates(mail, pass, age, gender, smoke, name, kosher, quiet, animals, play, about);

                }

            }
        }

        public void BindAdvancedGrid(int minAge, int maxAge, string gender, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {
            if (chosenKind == null || chosenArea == null)
            {
                MessageBox.Show("Please select kind of activity and area");
                return;
            }

            bool found = false;
            dt = new DataTable();
            //execute the find operation for the payed adds and recive the results
            busLogic.AdvancedSearchDates(chosenArea, chosenKind, ref dt, true, minAge, maxAge, gender, smoke, kosher, quiet, animals, play);
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
            busLogic.AdvancedSearchDates(chosenArea, chosenKind, ref dt, false, minAge, maxAge, gender, smoke, kosher, quiet, animals, play);
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



        private void button1_Click_req(object sender, RoutedEventArgs e)
        {

         
            if (user==null)
                System.Windows.MessageBox.Show("only registered users can apply requests");
            else {

                //IList rows = gvData.SelectedItems;
                //int adId = (int)rows[0];
                //string mail = (string)rows[1]; 
                DataRowView drv = (DataRowView)gvData.SelectedItem;
                DataRowView drvFree = (DataRowView)gvDataFree.SelectedItem;
                if (drv != null && drvFree != null)
                {
                    MessageBox.Show("you can apply for only one ad");
                    gvData.UnselectAll();
                    gvDataFree.UnselectAll(); 
                }
                else
                {
                    if(drv != null)
                    {
                        String adId = (drv["Ad ID"]).ToString();
                        int adIdNum = Int32.Parse(adId);
                        String activityId = (drv["Activity id"]).ToString();
                        int activityNum= Int32.Parse(activityId);
                        String AdvertiserMail = (drv["Advertiser Mail"]).ToString();
                        requestWin cw = new requestWin(busLogic, userMail, adIdNum, AdvertiserMail, activityNum, chosenKind);            
                        cw.ShowDialog();
                        if (cw.addreq == true)
                        {
                            gvData.UnselectAll();
                            MessageBox.Show("the request has been send");

                        }

                    }

                    if (drvFree != null)
                    {
                        String adId = (drvFree["Ad ID"]).ToString();
                        int adIdNum = Int32.Parse(adId);
                        String AdvertiserMail = (drvFree["Advertiser Mail"]).ToString();
                        String activityId = (drvFree["Activity id"]).ToString();
                        int activityNum = Int32.Parse(activityId);
                        requestWin cw = new requestWin(busLogic, userMail, adIdNum, AdvertiserMail, activityNum, chosenKind);
                        cw.ShowDialog();
                        if (cw.addreq == true)
                        {
                            gvDataFree.UnselectAll();

                        }
                    }

                    if (drv==null&& drvFree==null)

                         MessageBox.Show("You have not selected an ad");
                }
              
                // MessageBox.Show(result);
                // var rowIndex = gvData.SelectedIndex;

                //DataGridRow row = (DataGridRow)gvData.ItemContainerGenerator.ContainerFromIndex(rowIndex);



            }

            // if (cw.conf == true)
            //  userName.Content = cw.usr;

            //System.Windows.MessageBox.Show("The Service is Unavailable");
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
