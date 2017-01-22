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
    /// Interaction logic for searchDateWin.xaml
    /// </summary>
    /// 

    public partial class searchDateWin : Window
    {

        BusLogic busLogic;
        public bool isSearch;
        public int mAge;
        public int maxiAge;
        public bool? smoke;
        public bool? kosher;
        public bool? quiet;
        public bool? animals;
        public bool? play;
        public string gender;

        public searchDateWin(BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(minAge.Text, out mAge))
            {
                System.Windows.MessageBox.Show("Enter a valid age in natural number", "Error");
            }

            else if (!int.TryParse(maxAge.Text, out maxiAge))
            {
                System.Windows.MessageBox.Show("Enter a valid age in natural number", "Error");
            }

            else
            {
                gender = comboBoxGen.SelectedValue.ToString();
                //get all matching rows

                smoke = checkBoxSmoke.IsChecked;
                kosher = checkBoxKosher.IsChecked;
                quiet = checkBoxquiet.IsChecked;
                animals = checkBoxAnimals.IsChecked;
                play = checkBoxPlay.IsChecked;
                isSearch = true;

                // bool added = busLogic.AdvancedSearchDates(mail, pass, age, gender, smoke, name, kosher, quiet, animals, play, about);
                /*
                 if (added)
                  {
                      System.Windows.MessageBox.Show("User Created");
                      Close();
                  }
                  else
                  {
                      System.Windows.MessageBox.Show("There was problem to find match, Try again", "Error");
                  }

      */

            }


        }
    }
}
//}
