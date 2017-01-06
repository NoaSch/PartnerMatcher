﻿using System;
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

namespace PartnerMatcher
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
            busLogic = buslog;
            InitializeComponent();
            comboBoxKind.ItemsSource = busLogic.kinds.Keys.ToList();
            comboBoxArea.ItemsSource = busLogic.areas;
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
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;

            string tableName = this.busLogic.kinds[chosenKind];
            if (chosenArea == null)
                cmd.CommandText = "SELECT * from " + tableName + " WHERE Payed = True";
            else

                cmd.CommandText = "SELECT * from " + tableName + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols();
            //draw the results as a table
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
            if (chosenArea == null)
                cmd.CommandText = "SELECT * from " + tableName + " WHERE Payed = False";
            else
                cmd.CommandText = "SELECT * from " + tableName + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'";

            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols();
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

        //remove unnececery colums from the search results
        private void deleteCols()
        {
            dt.Columns.Remove("mail");
            dt.Columns.Remove("Payed");
            dt.Columns.Remove("addID");
            dt.Columns.Remove("smoke");
            dt.Columns.Remove("kosher");
            dt.Columns.Remove("quiet");
            dt.Columns.Remove("animals");
            dt.Columns.Remove("play");
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
