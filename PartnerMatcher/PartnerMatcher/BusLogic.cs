using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerMatcher
{
    class BusLogic
    {
        public List<string> ListboxItems = new List<string>();
        public void PopulateListBoxItems(string userName)
        {
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\redgabanan\Desktop\Gabanan_Red_dbaseCon\Red_Database.accdb";
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                connection.Open();
                OleDbDataReader reader = null;
                OleDbCommand command = new OleDbCommand("SELECT * from  Users WHERE LastName='@1'", connection);
                command.Parameters.AddWithValue("@1", userName);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ListboxItems.Add(reader[1].ToString() + "," + reader[2].ToString());
                }
            }
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