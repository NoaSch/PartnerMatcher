using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerMatcher
{
    public class BusLogic
    {
        public List<string> areas;
        //public static List<string> kinds;
        public Dictionary<string, string> kinds;



        public BusLogic()
        {
            addLists();
        }
        private void addLists()
        {
            areas = new List<string>();
            string[] areasArr = { "South", "North", "Center", "Sharon", "Eilat" };
            areas.AddRange(areasArr);



            kinds = new Dictionary<string, string>();
            kinds.Add("Real Estate", "realEstates");
            kinds.Add("Sport", "Sport");
            kinds.Add("Travel", "Trips");
            kinds.Add("Dates", "Dates");
        }
    }
}

/* var busLogic = new BusLogic();
       busLogic.PopulateListBoxItems(textBox8.Text);          
listBox1.Items.Clear();
       ListboxItems.DataSource = busLogic.ListboxItems;
* 
}
}
}
*/
