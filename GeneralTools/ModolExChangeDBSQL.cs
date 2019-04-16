//using GeneralTools.ModelToDataTableHelper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace GeneralTools
{
    public static class ModolExChangeDBSQL
    {


        /// <summary>
        /// 根据列的集合生成表的sql语句
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <param name="dataRowColmsList"></param>
        /// <param name="nomrLen"></param>
        /// <param name="specLen"></param>
        /// <returns></returns>

        public static string AccordingDataTableToDBSql(string dbName, string tableName, DataColumnCollection dataRowColmsList, int nomrLen, Dictionary<string, int> specLen = null)
        {
            if (specLen == null)
            {

                specLen = new Dictionary<string, int>();
            }
            var createTableSQL = "use " + dbName + ";" + "CREATE TABLE " + tableName + "(";
            foreach (var item in dataRowColmsList)
            {
                if (specLen.ContainsKey(item.ToString()))
                {
                    createTableSQL += "`" + item + "`" + $" varchar({specLen[item.ToString()]})" + " ,";
                    continue;
                }
                createTableSQL += "`" + item + "`" + $" varchar({nomrLen})" + " ,";
            }
            createTableSQL = createTableSQL.Trim(',') + ")  ENGINE=MYISAM;";


            return createTableSQL;
        }

        /// <summary>
        /// 根据实体类类型生成创建表的sql语句
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public static string AccordingModelToDBSql(string dbName, string tableName, Type T)
        {
            var createTableSQL = "use " + dbName + ";" + "CREATE TABLE IF NOT EXISTS `" + tableName + "`(";
            foreach (var item in T.GetProperties())
            {
                var tmp = ModelToDataTableHelper.GetToTableNameCellName(item);
                if (!tmp.Item1)
                {
                    createTableSQL += $"`{tmp.Item2}` varchar({tmp.Item3}) ,";
                }
            }
            createTableSQL = createTableSQL.Trim(',') + ")  ENGINE=MYISAM;";
            return createTableSQL;
        }


        /// <summary>
        /// 生成指定List实体向数据库插入的sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbName">要插入的数据库</param>
        /// <param name="tableName">要插入的表名</param>
        /// <param name="listModel">实体集合</param>
        /// <param name="MaxCountOnce">多少条数据生成一次插入的sql语句</param>
        /// <returns></returns>

        public static List<string> AccordingModelToInsertDBSQL<T>(string dbName, string tableName, List<T> listModel, int MaxCountOnce=5000) where T : new()
        {
            List<string> list = new List<string>();
            var insertSQL = "use " + dbName + ";" + "INSERT INTO " + tableName + "(";
            var tmpDic = ModelToDataTableHelper.GetModelToDBTableColmsPostion(typeof(T));
            var tmpTproperties = typeof(T).GetProperties();
            foreach (var item in tmpDic)
            {
                insertSQL += "`" + tmpTproperties[item.Value].GetToTableNameCellName().Item2 + "`" + " ,";
            }

            insertSQL = insertSQL.Trim(',') + ")VALUES";
            var count = 0;
            var starttime = DateTime.Now;
            int shumu = listModel == null ? 0 : listModel.Count;
            if (listModel != null && listModel.Count != 0)
            {
                StringBuilder sbu = new StringBuilder();
                sbu.Append(insertSQL);
                foreach (var item in listModel)
                {
                    sbu.Append("(");

                    foreach (var column in tmpDic)
                    {
                        sbu.Append("'" + tmpTproperties[column.Value].GetValue(item, null) + "'" + ",");
                    }
                    sbu.Remove(sbu.Length - 1, 1).Append("),");
                    count++;
                    try
                    {
                        if (count % MaxCountOnce == 0 || count == shumu)
                        {
                            //BynLog.Info(insertSQL);
                            list.Add(sbu.Remove(sbu.Length - 1, 1).ToString());
                            sbu.Clear().Append(insertSQL);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("出错的sql:" + sbu + @" " + ex.Message);
                    }
                }



            }
            return list;
        }








        /// <summary>
        /// 生成指定List实体向数据库更新语句，不含有where条件，特别注意，如果不想全部字段都更新请标记[ModolToDataTableMark(ignoreThisFiled=true)],如果字段为空不更新该字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbName">要更新的数据库</param>
        /// <param name="tableName">要更新的表名</param>
        /// <param name="listModel">实体集合</param>
        /// <returns></returns>

        public static List<string> AccordingModelToUapdatDBSQL<T>(string dbName, string tableName, List<T> listModel) where T : new()
        {
            List<string> list = new List<string>();
            foreach (var item in listModel)
            {
                var tmpSql = "UPDATE " + dbName + "." + tableName + " SET ";
                var tmpU = "";
                foreach (var item1 in typeof(T).GetProperties())
                {
                    var tmp = ModelToDataTableHelper.GetToTableNameCellName(item1);
                    if (!tmp.Item1)
                    {
                        var getval = item1.GetValue(item);
                        if (getval != null)
                        {
                            tmpU += "`" + tmp.Item2 + "`='" + getval + "',";
                        }
                    }
                }
                if (tmpU.Length != 0)
                {
                    list.Add("UPDATE " + dbName + "." + tableName + " SET " + tmpU.Trim(',') + " ");
                }
                else
                {
                    list.Add(null);
                }

            }
            return list;
        }






        /// <summary>
        /// 生成指定List实体向数据库更新语句，通过lamt表达式树进行条件的更新，特别注意，如果不想全部字段都更新请标记[ModolToDataTableMark(ignoreThisFiled=true)],如果字段为空不更新该字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbName">要更新的数据库</param>
        /// <param name="tableName">要更新的表名</param>
        /// <param name="listModel">实体集合</param>
        /// <param name="whereFun">更新的条件</param>
        /// <returns></returns>

        private static List<string> AccordingModelToUapdatDBSQL<T>(string dbName, string tableName, List<T> listModel,Func<T,bool> whereFun) where T : new()
        {
            List<string> list = new List<string>();
            foreach (var item in listModel)
            {
                var tmpSql = "UPDATE " + dbName + "." + tableName + " SET ";
                var tmpU = "";
                foreach (var item1 in typeof(T).GetProperties())
                {
                    var tmp = ModelToDataTableHelper.GetToTableNameCellName(item1);
                    if (!tmp.Item1)
                    {
                        var getval = item1.GetValue(item);
                        if (getval != null)
                        {
                            tmpU += "`" + tmp.Item2 + "`='" + getval + "',";
                        }
                    }
                }
                if (tmpU.Length != 0)
                {
                    list.Add("UPDATE " + dbName + "." + tableName + " SET " + tmpU.Trim(',') + " ");
                }
                else
                {
                    list.Add(null);
                }

            }
            return list;
        }



        /// <summary>
        /// 生成指定List实体向数据库更新语句，不含有where条件，特别注意，如果不想全部字段都更新请标记[ModolToDataTableMark(ignoreThisFiled=true)],如果字段为空不更新该字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbName">要更新的数据库</param>
        /// <param name="tableName">要更新的表名</param>
        /// <param name="Model">实体集合</param>
        /// <returns></returns>

        public static string AccordingModelToUapdatDBSQL<T>(string dbName, string tableName, T Model) where T : new()
        {

            var tmpU = "";
            foreach (var item1 in typeof(T).GetProperties())
            {
                var tmp = ModelToDataTableHelper.GetToTableNameCellName(item1);
                if (!tmp.Item1)
                {
                    var getval = item1.GetValue(Model);
                    if (getval != null)
                    {
                        tmpU += "`" + tmp.Item2 + "`='" + getval + "',";
                    }
                }
            }
            if (tmpU.Length != 0)
            {
                return "UPDATE " + dbName + "." + tableName + " SET " + tmpU.Trim(',') + " ";
            }
            return null;

        }



        /// <summary>
        /// 生成删除指定数据库中指定表的sql语句
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string IsDelteDBDatatable(string dbName, string tableName)
        {
            return "use " + dbName + ";" + "DROP TABLE IF EXISTS `" + tableName + "`;";
        }
    }

}
