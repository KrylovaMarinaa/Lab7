using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lab7.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        [RegularExpression("\\(?([8]|[+][7])\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{4})", ErrorMessage = "Вы ввели некорректный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите, примите ли участие в вечеринке")]
        public bool? WillAttend { get; set; }


        public void AddGuests(GuestResponse guest)
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["Lab7DB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com1 = new SqlCommand("SELECT * FROM Guests WHERE Email='" + Email + "' ", con);
                com1.Parameters.Add(new SqlParameter("@Email", guest.Email));
                con.Open();

                SqlDataReader r1 = com1.ExecuteReader();
                r1.Read();


                if (r1.HasRows)
                {
                    con.Close();
                    con.Open();


                    SqlCommand com2 = new SqlCommand("Update Guests set Name=@Name,Phone=@Phone,WillAttend=@WillAttend where Email=@Email", con);
                    com2.Parameters.Add(new SqlParameter("@Name", guest.Name));
                    com2.Parameters.Add(new SqlParameter("@Email", guest.Email));
                    com2.Parameters.Add(new SqlParameter("@Phone", guest.Phone));
                    com2.Parameters.Add(new SqlParameter("@WillAttend", guest.WillAttend.ToString()));
                    SqlDataReader r2 = com2.ExecuteReader();
                    r2.Read();
                    
                }
                //новая команда
                else
                {
                    con.Close();
                    con.Open();


                    string Name = guest.Name;
                    SqlCommand com = new SqlCommand("Insert into Guests (Name,Email,Phone,WillAttend) values (@Name,@Email,@Phone,@WillAttend)", con);
                    com.Parameters.Add(new SqlParameter("@Name", guest.Name));
                    com.Parameters.Add(new SqlParameter("@Email", guest.Email));
                    com.Parameters.Add(new SqlParameter("@Phone", guest.Phone));
                    com.Parameters.Add(new SqlParameter("@WillAttend", guest.WillAttend.ToString()));

                    //con.Open();
                    com.ExecuteNonQuery();
                }
                con.Close();

            }



        }
    }
}