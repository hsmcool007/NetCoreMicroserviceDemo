using System;
using System.Collections.Generic;
using System.Text;
using DbHelper.DataModel;
using Microsoft.Extensions.Configuration;
using DbHelper.MessageDto;
using DotNetCore.CAP;

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

        public static int InsertOrder(Order order)
        {

            string sql = @"INSERT INTO `Order`.`Order`
                            (
                            `CreateTime`,
                            `ProductID`,
                            `Count`)
                            VALUES
                            (
                            @CreateTime,
                            @ProductID,
                            @Count)";

            return dapperUtility.DapperInsertSingle<Order>(sql, order);



        }

        public static   void InsertOrderWithCAP(Order order,CreateOrderMessageDto messageDto, ICapPublisher _capBus)
        {
             string sql = "";
             dapperUtility.DapperTransaction<Order, CreateOrderMessageDto>(sql, order, messageDto, _capBus);
        }




    }
}
