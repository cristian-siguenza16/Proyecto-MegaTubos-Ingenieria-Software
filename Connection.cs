using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Proyecto_MegaTubos
{
    class Connection
    {
        public static MySqlConnection conex()
        {
            //string server = "192.168.43.121";
            //string user = "root2";
            //string psw = "megatubos";
            string server = "localhost";
            string user = "root";
            string psw = "Cralsive1620";
            string database = "libreria";
            string cadena = "server=" + server + ";user=" + user + ";pwd=" + psw + ";database=" + database + ";Sslmode=none;";
            MySqlConnection myCon = new MySqlConnection(cadena);
            try
            {
                return myCon;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
