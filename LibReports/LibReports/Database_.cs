using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LibReports
{
    class Database_
    {
        private MySqlConnection conn;
        private string m_strServer;
        //public string m_strDatabase;
        private string m_strUserid;
        private string m_strPassword;

        public Database_()
        {
            this.m_strServer = "127.0.0.1";
            this.m_strUserid = "root";
            this.m_strPassword = "root";
        }

        public void BuildConnection()
        {
            try
            {
                string ConString = "SERVER=" + m_strServer + ";" +
                                   "UID=" + m_strUserid + ";" +
                                   "PASSWORD=" + m_strPassword + ";" +
                                   "Port = 3306 ;";
                this.conn = new MySqlConnection(ConString);
            }
            catch (MySqlException ex)
            {
                //rn false;
            }

        }

        private bool OpenConnection()
        {
            try
            {
                this.conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                this.conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public void SelectMultiple(string qry, ref List<string> obj)
        {
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(qry, conn);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    obj.Add(dataReader.GetValue(0).ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
        }

        public int SelectGetCount(string qry)
        {
            //string query = "SELECT Count(*) FROM model";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //create 
                MySqlCommand cmd = new MySqlCommand(qry, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        public void Select(string qry, ref Dictionary<string, string> data)
        {

            //this.dict.Clear();
            string query = qry;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {

                        data.Add(dataReader.GetName(i), dataReader.GetValue(i).ToString());
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

            }
            else
            {

            }
        }
    }
}
