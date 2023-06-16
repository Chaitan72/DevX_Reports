using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LibReports
{
    class Database_
    {
        private MySqlConnection conn;
        private string m_strServer;
        public string m_strDatabase;
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
    }
}
