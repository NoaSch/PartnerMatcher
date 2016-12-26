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

namespace PartnerMatcher
{
    /// <summary>
    /// Interaction logic for findWin.xaml
    /// </summary>
    public partial class findWin : Window
    {
        string chosenKind;
        string chosenArea;
        List<string> areas;
        List<string> kinds;

        OleDbConnection con;
        DataTable dt;
        public findWin()
        {

            InitializeComponent();
            addLists();
            lblCount.Visibility = System.Windows.Visibility.Hidden;
            gvData.Visibility = System.Windows.Visibility.Hidden;

            con = new OleDbConnection();
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

        }

        private void addLists()
        {
            areas = new List<string>();
            string[] areasArr = { "South", "North", "Center", "Sharon", "Eilat" };
            areas.AddRange(areasArr);
            comboBoxArea.ItemsSource = areas;


            kinds = new List<string>();
            string[] kindsArr = { "Apartment", "Sport", "Date", "Travel", "Exams" };
            kinds.AddRange(kindsArr);
            comboBoxKind.ItemsSource = kinds;

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
            BindGrid();
        }


        private void BindGrid()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            // cmd.CommandText = "select * from tbEmp";
            //cmd.CommandText = "SELECT * from Adds WHERE  Loc ='" + chosenArea.Trim() + "'  AND Kind = '" + chosenKind.Trim() + "'";
            cmd.CommandText = "SELECT  Kind, Loc, Content from Adds WHERE Loc = '" + chosenArea.Trim() + "'  AND Kind = '" + chosenKind + "'";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();

            if (dt.Rows.Count > 0)
            {
                lblCount.Visibility = System.Windows.Visibility.Hidden;
                gvData.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                lblCount.Visibility = System.Windows.Visibility.Visible;
                gvData.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Not implemented yed");
        }
    }
}
