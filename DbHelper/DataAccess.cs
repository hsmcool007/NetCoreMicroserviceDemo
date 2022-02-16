using System;
using System.Collections.Generic;
using System.Text;
using DbHelper.DataModel;
using Microsoft.Extensions.Configuration;
using DbHelper.MessageDto;
using DotNetCore.CAP;
using System.Linq;

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
            try
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
                dapperUtility.DapperTransaction<Order, CreateOrderMessageDto>(sql, order, messageDto, _capBus);
            }
            catch
            {
                throw;
            }
    
        }


        public static Product GetProductById(int Id)
        {
            string sql = string.Format("SELECT * FROM Product.Product where Id={0}", Id);
            return  dapperUtility.DapperQuery<Product>(sql).FirstOrDefault();


        }

        public static void UpdateProduct(Product p)
        {

            string sql = String.Format(@"UPDATE Product
                                        SET                                     
                                        Name = '{0}',
                                        Stock={1}
                                        WHERE Id = {2} ", p.Name, p.Stock, p.Id);
             dapperUtility.DapperUpdate(sql);
        }




    }
}
