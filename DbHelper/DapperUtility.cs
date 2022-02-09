using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbHelper
{
    public class DapperUtility
    {
        string connectionString;

        public DapperUtility(string connectionStr)
        {      
            connectionString = connectionStr;
        }

        public IDbConnection GetDbConnection()
        {                  
            return new MySql.Data.MySqlClient.MySqlConnection(connectionString);          
        }


        public List<T> DapperQuery<T>(string sql)
        {

            List<T> list = new List<T>();
            using (var conn = GetDbConnection())
            {
                try
                {
                    conn.Open();
                    list = conn.Query<T>(sql).AsList();

                }

                catch (MySqlException sqlex)
                {

                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }

            }
            return list;
        }

        public List<T> DapperQuerySP<T>(string spName, object para)
        {
            List<T> list = new List<T>();
            using (var conn = GetDbConnection())
            {
                try
                {
                    conn.Open();
                    list = conn.Query<T>(spName, para, commandType: CommandType.StoredProcedure).AsList();

                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }

            }
            return list;
        }

        public int DapperExcuteSP(string spName, object para)
        {
            var affectedRows = -1;
            using (var conn = GetDbConnection())
            {

                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(spName,
                        para,
                        commandType: CommandType.StoredProcedure);

                    return affectedRows;

                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }

                }
                catch (System.Exception sysex)
                {

                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();

                }

            }
            return affectedRows;
        }

        public IEnumerable<dynamic> DapperQueryDynamic(string sql)
        {

            IEnumerable<dynamic> list = null;
            using (var conn = GetDbConnection())
            {

                try
                {
                    list = conn.Query(sql);
                    conn.Close();
                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();

                }

            }
            return list;

        }

        //public static T DapperQueryFirstOrDefault<T>(string sql)
        //{

        //    T temp;
        //    using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            temp = conn.QueryFirstOrDefault<T>(sql);

        //        }
        //        catch (MySqlException sqlex)
        //        {
        //            // Logger.Error(string.Format("Connection String: {0}", connectionString), sqlex);
        //            switch (sqlex.Number)
        //            {
        //                case 0:
        //                    // Logger.Error("Cannot connect to server.", null);
        //                    break;

        //                case 1045:
        //                    // Logger.Error("Invalid username/password.", null);
        //                    break;
        //            }

        //        }
        //        catch (System.Exception sysex)
        //        {
        //            //Logger.Error(string.Format("Connection String: {0}", connectionString), sysex);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }

        //    }
        //    return temp != null ? temp : null;
        //}

        public int DapperInsertSingle<T>(string sql, T insertItem)
        {
            var affectedRows = -1;
            using (var conn = GetDbConnection())
            {
                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(sql, insertItem);


                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }


            }
            return affectedRows;

        }

        public int DapperInsertMany<T>(string sql, List<T> insertItemList)
        {
            using (var conn = GetDbConnection())
            {
                var affectedRows = -1;
                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(sql, insertItemList);


                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }

                return affectedRows;
            }
        }

        //public static int DapperUpdateSingle<T>(string sql)
        //{
        //    using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        //    {

        //        conn.Open();
        //        var affectedRows = conn.Execute(sql);
        //        conn.Close();
        //        return affectedRows;

        //    }

        //}

        //public static void DapperUpdateMany<T>(string sql, List<T> updateItemList)
        //{
        //    using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        var affectedRows = conn.Execute(sql, updateItemList);
        //        conn.Close();
        //    }
        //}


        public int DapperUpdate(string sql)
        {
            using (var conn = GetDbConnection())
            {
                var affectedRows = -1;
                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(sql);
                    conn.Close();

                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }

                return affectedRows;

            }

        }

        public int DapperDelete(string sql)
        {
            using (var conn = GetDbConnection())
            {
                var affectedRows = -1;
                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(sql);
                    conn.Close();
                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }

                return affectedRows;

            }
        }

        public int DapperExecute<T>(string sql, T item)
        {
            using (var conn = GetDbConnection())
            {
                var affectedRows = -1;
                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(sql, item);
                    conn.Close();

                }
                catch (MySqlException sqlex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sqlex);
                    switch (sqlex.Number)
                    {
                        case 0:
                            //LogManager.Error("Cannot connect to server.", null);
                            break;

                        case 1045:
                            //LogManager.Error("Invalid username/password.", null);
                            break;
                    }
                    throw sqlex;
                }
                catch (System.Exception sysex)
                {
                    //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                    throw sysex;
                }
                finally
                {
                    conn.Close();
                }
                return affectedRows;
            }
        }

        public int DapperExecute(string sql)
        {
            using (var conn = GetDbConnection())
            {
                var affectedRows = -1;
                try
                {
                    conn.Open();
                    affectedRows = conn.Execute(sql);
                    conn.Close();

                }
                catch (MySqlException sqlex)
                {
                  
                    switch (sqlex.Number)
                    {
                        case 0:
                          
                            break;

                        case 1045:
                            break;
                    }
                    throw sqlex;
                }
                catch (System.Exception sysex)
                {
                  
                    throw sysex;
                }
                finally
                {
                    conn.Close();
                }
                return affectedRows;
            }
        }

        public int DapperExecuteSingleQuery<T>(string sql, T insertItem)
        {
            var insertId = -1;
            using (var conn = GetDbConnection())
            {
                try
                {
                    conn.Open();
                    insertId = conn.Query<int>(sql, insertItem).Single();
                }
                catch (MySqlException sqlex)
                {
                 
                    switch (sqlex.Number)
                    {
                        case 0:
                         
                            break;

                        case 1045:
                           
                            break;
                    }
                }
                catch (System.Exception sysex)
                {
                   // //LogManager.Error(string.Format("Connection String: {0}", connectionString), sysex);
                }
                finally
                {
                    conn.Close();
                }


            }
            return insertId;

        }

    }
}
