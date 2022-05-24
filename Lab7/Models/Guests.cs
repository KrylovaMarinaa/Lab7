using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Lab7.Models
{
    public class Guests
    {
        public string Name
        { get; set; }


        public string Email
        { get; set; }



        public string Phone
        { get; set; }


        public string WillAttend
        { get; set; }

        public List<Guests> GetGuests()
        {
            List<Guests> list = new List<Guests>();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["Lab7DB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com = new SqlCommand("Select * from Guests", con);
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    Guests g = new Guests();
                    g.Name = r["Name"].ToString();
                    g.Email = r["Email"].ToString();
                    g.Phone = r["Phone"].ToString();
                    g.WillAttend = r["WillAttend"].ToString();

                    list.Add(g);

                }
                
                
                
            }
            
            return list;
        }


    }
}