using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;
using System.Configuration;

namespace ImageExtractor
{
    public class Data
    {
        public List<Patient> PatientDataService()
        {
            List<Patient> list = new List<Patient>();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString))
            {
                conn.Open();
                string query = "Select  Pac,OfficeID from drdata.scandoc where Pac <> 0 order by OfficeID";
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.CommandText = query;
                    comm.Connection = conn;
                    MySqlDataReader reader= comm.ExecuteReader();
                    while (reader.Read())
                    {
                        Patient p = new Patient();
                        p.PatientID = Convert.ToInt32(reader[0]);
                        p.OfficeID = Convert.ToInt32(reader[1]);
                        list.Add(p);
                    }
                    comm.Dispose();
                    conn.Close();
                }
            }
            return list;
        }
        public DataTable ImageDataService(int PatientID)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString)) 
            {
                conn.Open();
                string query = $"select * from drdata.scandoc WHERE Pac={PatientID}";
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.CommandText = query;
                    comm.Connection = conn;
                    dt.Load(comm.ExecuteReader());
                    comm.Dispose();
                    conn.Close();
                }
            }
            return dt;
        }

    }
}