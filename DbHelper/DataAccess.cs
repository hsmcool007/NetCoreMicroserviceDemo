using System;
using System.Collections.Generic;
using System.Text;
using DbHelper.DataModel;
using Microsoft.Extensions.Configuration;

namespace DbHelper
{



    public static class DataAccess
    {
        private static string  _connectionString;
        private static DapperUtility dapperUtility;
        public static void ConnectionConfigure(string connectionString)
        {
            _connectionString = connectionString;
            dapperUtility = new DapperUtility(_connectionString);
        }


        public static void InsertTest(Test t)
        {

           string sql=  "INSERT INTO Test(Name) VALUES  (@Name)";
           dapperUtility.DapperInsertSingle<Test>(sql, t);


        }




    }
}
