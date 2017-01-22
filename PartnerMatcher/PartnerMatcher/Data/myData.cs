using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerMatcher.Data
{
    //class for the data layer
    public class myData
    {
        OleDbConnection conn;
        OleDbCommand cmd;

        public myData()
        {
            conn = new OleDbConnection();
            cmd = new OleDbCommand();
        }

        //add new user to the db
        public bool AddUserToDB(string mail, string pass, int age, string gender, bool? smoke, string name, bool? kosher, bool? quiet, bool? animals, bool? play, string about)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into Profiles (mail, Pass, age, gender, smoke, fullName, kosher, quiet, animals, play,about) values(@mail, @Pass,@age,@gender, @smoke,@name, @kosher, @quiet,@animals,@play,@about)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@mail", OleDbType.VarChar).Value = mail;
                cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = pass;
                cmd.Parameters.Add("@age", OleDbType.Integer).Value = age;
                cmd.Parameters.Add("@gender", OleDbType.VarChar).Value = gender;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = name;
                cmd.Parameters.Add("@kosher", OleDbType.Boolean).Value = kosher;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = quiet;
                cmd.Parameters.Add("@animals", OleDbType.Boolean).Value = animals;
                cmd.Parameters.Add("@play", OleDbType.Boolean).Value = play;
                cmd.Parameters.Add("@about", OleDbType.VarChar).Value = about;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void AdvancedSearchDates(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string gender, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {
                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND gender = '" + gender.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals + " AND play = " + play;
            }
            else
            {
                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND gender = '" + gender.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals + " AND play = " + play;
            }

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols(ref dt);
            //draw the results as a table
        }


        //check if a mail is exist in the db
        public bool checkIfUserExist(string mail)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from Profiles WHERE  mail ='" + mail.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }


        public bool saveRequest(string mail, DateTime date, int activityId, string kindName, int adId, string content, int status)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into RequestId (askerMail, RequsetDate, ActivityId, KindName, AdId, Content, Status) values(@askerMail, @RequsetDate ,@activityId ,@kindName, @adId, @content, @status)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@askerMail", OleDbType.VarChar).Value = mail;
                cmd.Parameters.Add("@date", OleDbType.DBDate).Value = date;
                cmd.Parameters.Add("@activityId", OleDbType.Integer).Value = activityId;
                cmd.Parameters.Add("@KindName", OleDbType.VarChar).Value = kindName;
                cmd.Parameters.Add("@AdId", OleDbType.Integer).Value = adId;
                cmd.Parameters.Add("@Content", OleDbType.VarChar).Value = content;
                cmd.Parameters.Add("@Status", OleDbType.Integer).Value = status;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        //ckeck if the password of user is correct
        public bool checkPassword(string mail, string password)
        {
            //get the user's password from the db
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT Pass, FullName from Profiles WHERE  mail ='" + mail.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            //check if the mail exists
            if (!reader.HasRows)
            {
                conn.Close();
                return false;
            }
            else
            {
                while (reader.Read())
                {
                    string ans = reader[0].ToString();
                    if (ans != password)
                    {
                        conn.Close();
                        return false;
                    }
                    else
                    {
                        conn.Close();
                        return true;
                    }
                }
            }

            return false;
        }

        //get the user's name
        public string getName(string mail)
        {
            string ans = "";
            //get the user's password from the db
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT FullName from Profiles WHERE  mail ='" + mail.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            //check if the mail exists
            if (!reader.HasRows)
            {
                return "";
            }
            else
            {
                while (reader.Read())
                {
                    ans = reader[0].ToString();
                    return ans;
                }
            }

            return ans;
        }
        
        //list of all the members in a requested activity
        public List<string> getMembersActivity(int id)
        {
            OleDbDataReader reader = null;
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * from ActivitiesMembers" + " WHERE ActivityId = " + id;
            // OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            // DataTable dt = new DataTable();
            //  da.Fill(dt);
            reader = cmd.ExecuteReader();
            List<string> members = new List<string>();
            while (reader.Read())
            {
                members.Add(reader[1].ToString());
            }
            return members;
        }

        //list of all the activities that the user is a member in them
        public List<string> getMemberActivities(string userMail)
        {
            OleDbDataReader reader = null;
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Activities.ActivityName FROM Activities, ActivitiesMembers " + " WHERE ActivitiesMembers.Mail = '" + userMail.Trim() + "' AND Activities.ActivityId = ActivitiesMembers.ActivityId ";
            // OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            // DataTable dt = new DataTable();
            //  da.Fill(dt);
            reader = cmd.ExecuteReader();
            List<string> activities = new List<string>();
            while (reader.Read())
            {                
                activities.Add(reader[1].ToString());
            }
            return activities;
        }

        //remove unnececery colums from the search results
        private void deleteCols(ref DataTable dt)
        {
            // dt.Columns.Remove("mail");
            dt.Columns.Remove("Payed");
            // dt.Columns.Remove("addID");
            dt.Columns.Remove("smoke");
            dt.Columns.Remove("kosher");
            dt.Columns.Remove("quiet");
            dt.Columns.Remove("animals");
            dt.Columns.Remove("play");
        }


        //find matching adds from the db
        public void find(string chosenArea, string chosenKind, ref DataTable dt, bool payed)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {
                if (chosenArea == "")
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True";
                else
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'";
            }
            else
            {
                if (chosenArea == "")
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False";
                else
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'";
            }

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols(ref dt);
            //draw the results as a table
        }

        //get the list of kinds 
        internal List<string> getKinds()
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.Open();
            OleDbDataReader reader = null;

            cmd = new OleDbCommand("SELECT * From Kinds", conn);
            reader = cmd.ExecuteReader();
            List<string> kinds = new List<string>();
            while (reader.Read())
            {
                kinds.Add(reader[0].ToString());
            }
            return kinds;
        }

        //get the list of areas from the db
        internal List<string> getAreas()
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";

            OleDbDataReader reader = null;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd = new OleDbCommand("SELECT * From Areas", conn);
            reader = cmd.ExecuteReader();
            List<string> areas = new List<string>();
            while (reader.Read())
            {
                areas.Add(reader[0].ToString());
            }
            return areas;
        }
    }
}






