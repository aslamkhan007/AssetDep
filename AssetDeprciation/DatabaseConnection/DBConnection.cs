using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AssetDeprciation.DatabaseConnection
{
    public class DBConnection
    {
        public static  SqlConnection getConnection()
        {
                //string connect = "Data Source=ITS-PRINTER\\APPAN;Initial Catalog=AssetDeprication;Integrated Security=SSPI;Persist Security Info=False;";

                //string connect = "Data Source=Test2k;Initial Catalog=Check;User Id=itgrp;Password=power ";
                string connect = "Data Source=test2k;Initial Catalog=AssetDepriciation;User Id=itgrp;Password=power ";
                //string connect = "Data Source=hp;Initial Catalog=AssetDeprication;Integrated Security=SSPI;Persist Security Info=False;";
                SqlConnection  con = new SqlConnection(connect);
                con.Open();
                return con;



        }
    }
}