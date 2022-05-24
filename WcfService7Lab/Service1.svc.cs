using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService7Lab
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Guests> GetGuests()
        {
            List<Guests> list = new List<Guests>();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["Lab7DB"].ConnectionString;
            using(SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com = new SqlCommand("Selec * from Guests",con);
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while(r.Read())
                {
                    Guests g = new Guests();
                    g.Name = r["Name"].ToString();
                    g.Name = r["Email"].ToString();
                    g.Name = r["Phone"].ToString();
                    g.Name = r["WillAttend"].ToString();

                    list.Add(g);

                }
            }
            return list;
        }

    }
}
