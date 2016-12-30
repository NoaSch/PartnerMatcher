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
        //list of areas
        public List<string> areas;
        //map between nkind name to it's table in the db
        public Dictionary<string, string> kinds;



        public BusLogic()
        {
            addLists();
        }

        private void addLists()
        {
            areas = new List<string>();
            string[] areasArr = { "North", "South", "Center", "Jerusalem" };
            areas.AddRange(areasArr);
            kinds = new Dictionary<string, string>();
            kinds.Add("Real Estate", "realEstates");
            kinds.Add("Sport", "Sport");
            kinds.Add("Travel", "Trips");
            kinds.Add("Dates", "Dates");
        }
    }
}

