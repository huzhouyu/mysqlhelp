using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace GeneralTools.Test1
{

    public interface IMQueryable
    {
        List<T> GetModle<T>();
        DataTable GetTable();

    }

    public static class MQueryable
    {

        public static List<T> GetModle<T>(this DBase bs) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static DataTable GetTable(this DBase bs)
        { 
            var sql = "";
            Dictionary<string, string> colmsList = new Dictionary<string, string>();
            foreach (var item in bs.CondtionList)
            {
                switch (item.Type)
                {
                    case SqlType.Where:sql= LamToSql.GetWhereSql(sql,item.Fun,item.DataType, item.Data,colmsList);break;
                    case SqlType.Join: sql=LamToSql.GetJoinSql(sql,item.Fun,item.DataType, item.Data, colmsList); break;
                    case SqlType.Select: sql = LamToSql.GetSelectSql(sql, item.Fun, item.DataType, item.Data, colmsList); break;
                    case SqlType.GroupBy: sql = LamToSql.GetGroupBySql(sql, item.Fun, item.DataType, item.Data, colmsList); break;
                    case SqlType.Max: sql = LamToSql.GetFunSql(sql, item.Fun, item.DataType, item.Data, colmsList,"MAX"); break;
                    case SqlType.Min: sql = LamToSql.GetFunSql(sql, item.Fun, item.DataType, item.Data, colmsList,"MIN"); break;
                }
                //sql+= LamToSql.GetWhereSql(item.Fun, item.Data);
            }
            return null;
            throw new NotImplementedException();
        }

        public static DBase MaxSelect<T>(this DBase t, Expression<Func<T, object>> fun, object date = null)
        {
            t.AddType<T>(SqlType.Max, fun, date);
            return t;
        }
        public static DBase MaxSelect<T,T1>(this DBase t, Expression<Func<T, T1, object>> fun, object date = null)
        {
            t.AddType<T>(SqlType.Max, fun, date);
            return t;
        }
        public static DBase MaxSelect<T, T1, T2>(this DBase t, Expression<Func<T, T1, T2, object>> fun, object date = null)
        {
            t.AddType<T>(SqlType.Max, fun, date);
            return t;
        }
        public static DBase MaxSelect<T, T1, T2, T3>(this DBase t, Expression<Func<T, T1, T2, T3, object>> fun, object date = null)
        {
            t.AddType<T>(SqlType.Max, fun, date);
            return t;
        }
        public static DBase MaxSelect<T, T1, T2, T3, T4>(this DBase t, Expression<Func<T, T1, T2, T3, T4, object>> fun, object date = null)
        {
            t.AddType<T>(SqlType.Max, fun, date);
            return t;
        }
        public static DBase MinSelect<T>(this DBase t, Expression<Func<T, object>> fun, object date = null)
        {

            t.AddType<T>(SqlType.Min, fun, date);
            return t;
        }



        public static DBase MinSelect<T, T1>(this DBase t, Expression<Func<T, T1, object>> fun, object date = null)
        {

            t.AddType<T1>(SqlType.Min, fun, date);
            return t;
        }
        public static DBase MinSelect<T, T1,T2>(this DBase t, Expression<Func<T, T1, T2, object>> fun, object date = null)
        {

            t.AddType<T1>(SqlType.Min, fun, date);
            return t;
        }
        public static DBase MinSelect<T, T1,T2,T3>(this DBase t, Expression<Func<T, T1, T2, T3, object>> fun, object date = null)
        {

            t.AddType<T1>(SqlType.Min, fun, date);
            return t;
        }
        public static DBase MinSelect<T, T1,T2,T3,T4>(this DBase t, Expression<Func<T, T1, T2, T3, T4, object>> fun, object date = null)
        {

            t.AddType<T1>(SqlType.Min, fun, date);
            return t;
        }

        public static DBase Where<T>(this DBase t, Expression<Func<T, object>> fun, object date = null)
        {

            t.AddType<T>(SqlType.Where, fun, date);
            return t;
        }
        public static DBase Join<T,T1>(this DBase t, Expression<Func<T,T1, object>> fun, object date = null)
        {

            t.AddType<T1>(SqlType.Join,fun, date);
            return t;
        }

        public static DBase GroupBy<T>(this DBase t, Expression<Func<T, object>> fun)
        {

            t.AddType<T>(SqlType.GroupBy, fun, null);
            return t;
        }
        public static DBase GroupBy<T, T1>(this DBase t, Expression<Func<T, T1, object>> fun)
        {
            t.AddType<T>(SqlType.GroupBy, fun, null);
            return t;
        }
        public static DBase GroupBy<T, T1, T2>(this DBase t, Expression<Func<T, T1, T2, object>> fun)
        {
            t.AddType<T>(SqlType.GroupBy, fun, null);
            return t;
        }
        public static DBase GroupBy<T, T1, T2, T3>(this DBase t, Expression<Func<T, T1, T2, T3, object>> fun)
        {
            t.AddType<T>(SqlType.GroupBy, fun, null);
            return t;
        }
        public static DBase GroupBy<T, T1, T2, T3, T4>(this DBase t, Expression<Func<T, T1, T2, T3, T4, object>> fun)
        {
            t.AddType<T>(SqlType.GroupBy, fun, null);
            return t;
        }

        public static DBase Select<T>(this DBase t, Expression<Func<T,object>> fun)
        {
            
            t.AddType<T>(SqlType.Select, fun, null);
            return t;
        }
        public static DBase Select<T,T1>(this DBase t, Expression<Func<T, T1, object>> fun)
        {
            t.AddType<T>(SqlType.Select, fun, null);
            return t;
        }
        public static DBase Select<T,T1,T2>(this DBase t, Expression<Func<T, T1, T2, object>> fun)
        {
            t.AddType<T>(SqlType.Select, fun, null);
            return t;
        }
        public static DBase Select<T,T1,T2,T3>(this DBase t, Expression<Func<T, T1, T2, T3, object>> fun)
        {
            t.AddType<T>(SqlType.Select, fun, null);
            return t;
        }
        public static DBase Select<T,T1,T2,T3,T4>(this DBase t, Expression<Func<T, T1, T2, T3, T4, object>> fun)
        {
            t.AddType<T>(SqlType.Select, fun, null);
            return t;
        }

    }


    public class DBQueryable : DBContextBase
    {
        //public cityinfo cityinfo { get; set; }
    }

    public class DBContextBase
    {
        private static Dictionary<string, TableInfo> _dicInfo = new Dictionary<string, TableInfo>();
        public DBContextBase()
        {
            if (_dicInfo.Count == 0)
            {

                var types = this.GetType().GetProperties();
                foreach (var item in types)
                {
                    var FieldName = item.Name;
                    TableInfo ti = new TableInfo();
                    ti.DBName = "byndb";
                    ti.TableName = FieldName;
                    _dicInfo.Add(FieldName, ti);
                }
            }
        }
        public DBase Do<T>()
        {
            var tmp= new DBase(_dicInfo);
            tmp.AddType<T>(SqlType.Where, null, null);
            return tmp;
        }
    }

    public class TableInfo
    {
        public string TableName { get; set; }
        public string DBName { get; set; }
        //public Type DBType { get; set; }
    }
    public class DBase
    {
        private static Dictionary<string, TableInfo> _dicInfo = new Dictionary<string, TableInfo>();
        public DBase(Dictionary<string,TableInfo> tableInfoList)
        {
            _dicInfo = tableInfoList;
        }
        internal List<DBaseExpression> CondtionList { get { return this._condtionList; } }

        private List<DBaseExpression> _condtionList = new List<DBaseExpression>();
        /// <summary>
        /// 不要调用这个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="t"></param>
        internal void AddType<T1>(SqlType type,Expression where, object t)
        {
            _condtionList.Add(new DBaseExpression(type,where,t,typeof(T1)));

        }
    }

    public enum SqlType
    {
        Join,
        Where,
        Select,
        Update,
        Insert,
        GroupBy,
        Max,
        Min
    }
    internal class DBaseExpression
    {
        public DBaseExpression(SqlType type, Expression fun, object data,Type dataType)
        {
            this.Data = data;
            this.Fun = fun;
            this.Type = type;
            this.DataType = dataType;

        }
        public Type DataType { get; set; }
        public SqlType Type { get; set; }
        public Expression Fun { get; set; }
        public object Data { get; set; }
    }


    public static class QueryClass
    {
        static void Mian()
        {
        }
    }

    internal static class LamToSql
    {

        /// <summary>
        /// 生成where的sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static T WhereT<T>(this T t,Expression valueSelector) where T: DBase,new()
        {
            t.AddType<T>(SqlType.Where, valueSelector, t);
            return t;
        }

        public static T Join<T, T1>(this T t, Expression<Func<T, T1, bool>> fun, object date = null) where T :DBase where  T1:DBase
        {

            t.AddType<T1>(SqlType.Join, fun, date);
            return t;
        }



        public static string GetWhereSql(string sql,Expression valueSelector, Type type, object obj, Dictionary<string, string> colmsList)
        {
            if (sql == "")
            {

                string sqlWhere = "SELECT ";
                var list = type.GetProperties();
                foreach (var item in colmsList.Keys)
                {
                    sqlWhere += $" `{item}`,";
                }
                foreach (var item in list)
                {
                    if (!colmsList.ContainsKey(item.Name))
                    {
                        colmsList.Add(item.Name, type.Name);
                        sqlWhere += $" `{item.Name}`,";
                    }
                }
                sqlWhere = sqlWhere.Trim(',') + $" FROM `{type.Name}` ";
                if (valueSelector != null)
                {
                    var tmp = ((LambdaExpression)valueSelector).Body;
                    var resut = DoMethod(tmp.NodeType)(tmp, BinaryExpressionPosion.Left, obj); //GetWhereSqlForExpression(tmp,BinaryExpressionPosion.Left,obj);
                    string tmpt = "";
                    if (resut != null)
                    {
                        tmpt = resut.ToString();
                        tmpt = " WHERE " + tmpt.Substring(1, tmpt.Length - 2);
                    }
                    sqlWhere += tmpt;
                }
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
            else
            {
                string sqlWhere = "SELECT ";
                var tmpName = Guid.NewGuid().ToString();
                var list = type.GetProperties();
                foreach (var item in colmsList.Keys)
                {
                    sqlWhere += $" `{item}`,";
                }
                foreach (var item in list)
                {
                    if (!colmsList.ContainsKey(item.Name))
                    {
                        colmsList.Add(item.Name, type.Name);
                        sqlWhere += $" `{item.Name}`,";
                    }
                }
                sqlWhere = sqlWhere.Trim(',') + $" FROM ({sql}) `{tmpName}`";
                if (valueSelector != null)
                {
                    var tmp = ((LambdaExpression)valueSelector).Body;
                    var resut = DoMethod(tmp.NodeType)(tmp, BinaryExpressionPosion.Left, obj); //GetWhereSqlForExpression(tmp,BinaryExpressionPosion.Left,obj);
                    string tmpt = "";
                    if (resut != null)
                    {
                        tmpt = resut.ToString();
                        tmpt = " WHERE " + tmpt.Substring(1, tmpt.Length - 2);
                    }
                    sqlWhere += tmpt;
                }
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
        }

        public static string GetSelectSql(string sql, Expression valueSelector, Type type, object obj, Dictionary<string, string> colmsList)
        {

            var resut = "";
            if (valueSelector != null)
            {
                var tmp = ((LambdaExpression)valueSelector).Body;
                var pameS = new Dictionary<string, string>();
                if (valueSelector.NodeType == ExpressionType.Lambda)
                {
                    foreach (var item in ((LambdaExpression)valueSelector).Parameters)
                    {
                        pameS.Add(item.Name, item.Type.Name);
                    }
                }
                resut = GetSelectSqlForExpressionT(sql, tmp, BinaryExpressionPosion.Left, type, obj, colmsList,pameS); //GetWhereSqlForExpression(tmp,BinaryExpressionPosion.Left,obj);
            }
            return resut;
        }


        public static string GetFunSql(string sql, Expression valueSelector, Type type, object obj, Dictionary<string, string> colmsList,string Funsign)
        {

            var resut = "";
            if (valueSelector != null)
            {
                var tmp = ((LambdaExpression)valueSelector).Body;
                var pameS = new Dictionary<string, string>();
                if (valueSelector.NodeType == ExpressionType.Lambda)
                {
                    foreach (var item in ((LambdaExpression)valueSelector).Parameters)
                    {
                        pameS.Add(item.Name, item.Type.Name);
                    }
                }
                resut = GetFunSqlForExpressionT(sql, tmp, BinaryExpressionPosion.Left, type, obj, colmsList, pameS, Funsign); //GetWhereSqlForExpression(tmp,BinaryExpressionPosion.Left,obj);
            }
            return resut;
        }





        public static string GetGroupBySql(string sql, Expression valueSelector, Type type, object obj, Dictionary<string, string> colmsList)
        {

            var resut = "";
            if (valueSelector != null)
            {
                var tmp = ((LambdaExpression)valueSelector).Body;
                var pameS = new Dictionary<string, string>();
                if (valueSelector.NodeType == ExpressionType.Lambda)
                {
                    foreach (var item in ((LambdaExpression)valueSelector).Parameters)
                    {
                        pameS.Add(item.Name, item.Type.Name);
                    }
                }
                resut = GetGroupBySqlForExpressionT(sql, tmp, BinaryExpressionPosion.Left, type, obj, colmsList, pameS); //GetWhereSqlForExpression(tmp,BinaryExpressionPosion.Left,obj);
            }
            return resut;
        }

        /// <summary>
        /// 获取JoinSql字符串
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetSelectSqlForExpressionT(string sql, Expression exp, BinaryExpressionPosion posion, Type type, object obj, Dictionary<string, string> colmsList,Dictionary<string,string> pams)
        {
            var tm = (List<NewClass>)DoMethod(exp.NodeType)(exp, BinaryExpressionPosion.Left, null);
            foreach (var item in tm)
            {
                item.TableName = pams.Where(u => u.Key == item.TableName).FirstOrDefault().Value;
            }
            string sqlWhere = "SELECT ";
            if (sql == "")
            {

                foreach (var item in tm)
                {
                    if (!colmsList.ContainsKey(item.NewName.Trim('`')))
                    {
                        colmsList.Add(item.NewName.Trim('`'), item.TableName);
                        sqlWhere += $" `{item.oldName.Trim('`')}` `{item.NewName.Trim('`')}`,";
                    }
                }
                sqlWhere = sqlWhere.Trim(',') + $" FROM `{tm.FirstOrDefault().TableName}` ";
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
            else
            {
                var tmpName = Guid.NewGuid().ToString();
                Dictionary<string, string> comls = new Dictionary<string, string>();
                foreach (var item in tm)
                {

                    foreach (var item1 in colmsList.Where(u => u.Value == item.TableName))
                    {
                        if (item1.Key.Contains(item.oldName.Trim('`')))
                        {
                            colmsList.Remove(item.oldName.Trim('`'));
                            comls.Add(item.NewName, item.TableName);
                            sqlWhere += $" `{item1.Key}` `{item.NewName.Trim('`')}`,";
                            break;
                        }
                    }
                }
                    
                sqlWhere = sqlWhere.Trim(',') + $" FROM ({sql}) `{tmpName}`";
                colmsList.Clear();
                foreach (var item in comls)
                {
                    colmsList.Add(item.Key, item.Value);
                }
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
        }






        /// <summary>
        /// 获取Max字符串
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetFunSqlForExpressionT(string sql, Expression exp, BinaryExpressionPosion posion, Type type, object obj, Dictionary<string, string> colmsList, Dictionary<string, string> pams,string FunSign)
        {

            var tm = (List<NewClass>)DoMethod(exp.NodeType)(exp, BinaryExpressionPosion.Left, null);
            foreach (var item in tm)
            {
                item.TableName = pams.Where(u => u.Key == item.TableName).FirstOrDefault().Value;
            }
            string sqlWhere = "SELECT ";
            if (sql == "")
            {

                foreach (var item in tm)
                {
                    if (!colmsList.ContainsKey(item.NewName.Trim('`')))
                    {
                        if (item.NewName.Trim('`') == item.oldName.Trim('`'))
                        {
                            colmsList.Add(item.NewName.Trim('`'), item.TableName);
                            sqlWhere += $" `{item.NewName.Trim('`')}`,";
                        }
                        else
                        {
                            colmsList.Add(item.NewName.Trim('`'), item.TableName);
                            sqlWhere += $" {FunSign}(`{item.oldName.Trim('`')})`) `{item.NewName.Trim('`')}`,";
                        }
                    }
                }
                sqlWhere = sqlWhere.Trim(',') + $" FROM `{tm.FirstOrDefault().TableName}` ";
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
            else
            {
                var tmpName = Guid.NewGuid().ToString();
                Dictionary<string, string> comls = new Dictionary<string, string>();
                foreach (var item in tm)
                {

                    foreach (var item1 in colmsList.Where(u => u.Value == item.TableName))
                    {
                        if (item1.Key.Contains(item.oldName.Trim('`')))
                        {
                            colmsList.Remove(item.oldName.Trim('`'));
                            if (item.oldName.Trim('`') == item.NewName)
                            {
                                colmsList.Add(item.NewName.Trim('`'), item.TableName);
                                sqlWhere += $" `{item.NewName.Trim('`')}`,";
                                break;
                            }
                            else
                            {
                                comls.Add(item.NewName, item.TableName);
                                sqlWhere += $" {FunSign}(`{item1.Key}`) `{item.NewName.Trim('`')}`,";
                                break;
                            }
                        }
                    }
                }

                sqlWhere = sqlWhere.Trim(',') + $" FROM ({sql}) `{tmpName}`";
                colmsList.Clear();
                foreach (var item in comls)
                {
                    colmsList.Add(item.Key, item.Value);
                }
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
        }




        /// <summary>
        /// 获取GroupBy字符串
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetGroupBySqlForExpressionT(string sql, Expression exp, BinaryExpressionPosion posion, Type type, object obj, Dictionary<string, string> colmsList, Dictionary<string, string> pams)
        {
            
            List<string> tt = new List<string>();
            if (exp.NodeType==ExpressionType.New)
            {
                var tm = (List<NewClass>)DoMethod(exp.NodeType)(exp, BinaryExpressionPosion.Left, null);
                foreach (var item in tm)
                {
                    tt.Add(item.oldName);
                }
            }
            else
            {
                tt.Add(DoMethod(exp.NodeType)(exp, BinaryExpressionPosion.Left, null).ToString());
            }
            string sqlWhere = "SELECT ";
            if (sql == "")
            {


                var list = type.GetProperties();
                foreach (var item in colmsList.Keys)
                {
                    sqlWhere += $" `{item}`,";
                }
                foreach (var item in list)
                {
                    if (!colmsList.ContainsKey(item.Name))
                    {
                        colmsList.Add(item.Name, type.Name);
                        sqlWhere += $" `{item.Name}`,";
                    }
                }
                sqlWhere = sqlWhere.Trim(',') + $" FROM `{type.Name}` GROUP BY {string.Join(",", tt.ToArray())} ";
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
            else
            {
                var tmpName = Guid.NewGuid().ToString();
                foreach (var item in colmsList)
                {
                    sqlWhere += $" `{item.Key}`,";
                }

                sqlWhere = sqlWhere.Trim(',') + $" FROM ({sql}) `{tmpName}` GROUP BY {string.Join(",",tt.ToArray())} ";
                //SELECT * FROM cityinfo WHERE
                return sqlWhere;
            }
        }




        public static string GetJoinSql(string sql,Expression valueSelector, Type type, object obj, Dictionary<string, string> colmsList)
        {

            var resut = "";
            if (valueSelector != null)
            {
                var tmp = ((LambdaExpression)valueSelector).Body;
                resut = GetJoinSqlForExpressionT(sql,tmp, BinaryExpressionPosion.Left,type, obj,colmsList); //GetWhereSqlForExpression(tmp,BinaryExpressionPosion.Left,obj);
            }
            return resut;
        }

        /// <summary>
        /// 获取Sql字符串
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object GetWhereSqlForExpressionT(Expression exp, BinaryExpressionPosion posion, object obj)
        {
            object sqlWhere = "";
            if (exp != null)
            {
                var sign = ExpressionTypeToSqlWhere(exp.NodeType);
                if (sign.Item1 == AboutIndex.Single)
                {
                    posion = BinaryExpressionPosion.Right;
                }
                if (sign.Item1 == AboutIndex.Dob)
                {
                    var tmpTest = (BinaryExpression)exp;
                    var LeftResult = DoMethod(tmpTest.Left.NodeType)(tmpTest.Left, BinaryExpressionPosion.Left, obj);
                    var RightResult = DoMethod(tmpTest.Right.NodeType)(tmpTest.Right, BinaryExpressionPosion.Right, obj);
                    if (IsNeedSign(RightResult.GetType()))
                    {
                        var tmpStr = RightResult.ToString();
                        if (tmpStr.Length > 3 && tmpStr[0] == '(' && tmpStr[tmpStr.Length - 1] == ')' && tmpStr[1] == '\'' && tmpStr[tmpStr.Length - 2] == '\'')
                        {

                            RightResult = $"'{tmpStr.Substring(2, tmpStr.Length - 4)}'";
                        }
                        else
                        {
                            RightResult = $"'{tmpStr}'";
                        }
                    }
                    sqlWhere = $"({LeftResult}{sign.Item2}{RightResult})";
                }
                else if (sign.Item1 == AboutIndex.Single)
                {
                    var tmpTest = (BinaryExpression)exp;
                    var LeftResult = DoMethod(tmpTest.Left.NodeType)(tmpTest.Left, BinaryExpressionPosion.Right, obj);
                    var RightResult = DoMethod(tmpTest.Right.NodeType)(tmpTest.Right, BinaryExpressionPosion.Right, obj);
                    sqlWhere = $"({LeftResult}{sign.Item2}{RightResult})";
                }
            }
            return sqlWhere;
        }

        /// <summary>
        /// 获取JoinSql字符串
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetJoinSqlForExpressionT(string sql,Expression exp, BinaryExpressionPosion posion, Type type, object obj, Dictionary<string, string> colmsList)
        {

            if (sql == "")
            {
                sql = $"`{type.Name}`";
            }
            else
            {
                sql = $"({sql})";
            }
           string tableSql = "";
            if (exp != null)
            {
                if (exp.NodeType == ExpressionType.Convert)
                {
                    exp = ((UnaryExpression)exp).Operand;
                }
                var sign = ExpressionTypeToSqlWhere(exp.NodeType);
                if (sign.Item1 != AboutIndex.NoHave)
                {
                    var tmpTest = (BinaryExpression)exp;
                    var LeftResult = DoMethod(tmpTest.Left.NodeType)(tmpTest.Left, BinaryExpressionPosion.Left, obj);
                    //tableSql.Add(Guid.NewGuid().ToString(), LeftResult);
                    var RightResult = DoMethod(tmpTest.Right.NodeType)(tmpTest.Right, BinaryExpressionPosion.Left, obj);
                    //tableSql.Add(type.Name, RightResult);
                    //SELECT b.`name` FROM (SELECT * FROM cityinfo) as t JOIN cityinfo b ON t.cityid =b.cityid 
                    tableSql = "SELECT ";
                    string tmpTable = Guid.NewGuid().ToString();
                    string joinTable = Guid.NewGuid().ToString();
                    foreach (var item in colmsList.Keys)
                    {
                        tableSql += $"`{tmpTable}`.`{item}`,";
                    }
                    if (colmsList.Values.Contains(type.Name))
                    {

                    }
                    else
                    {
                        foreach (var item in type.GetProperties())
                        {
                            if (!colmsList.ContainsKey(item.Name))
                            {
                                tableSql += $"`{joinTable}`.`{item.Name}`,";
                                colmsList.Add(item.Name, type.Name);
                            }
                            else
                            {
                                int count = 1;
                                string name = item.Name + count;
                                while (colmsList.ContainsKey(name))
                                {
                                    count++;
                                    name += item.Name + count;
                                }
                                tableSql += $"`{joinTable}`.`{item.Name}` `{name}`,";
                                colmsList.Add(name, type.Name);
                            }
                        }
                    }
                    tableSql = tableSql.Trim(',') + $" FROM {sql} `{tmpTable}` JOIN `{type.Name}` `{joinTable}` ON `{tmpTable}`.{LeftResult}{sign.Item2}`{joinTable}`.{RightResult} ";
                }
            }
            return tableSql;
        }



        /// <summary>
        /// NodeType=Convert处理程序
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object ExpressionConvntT(Expression exp, BinaryExpressionPosion posion, object obj)
        {
            object sqlWhere = null;
            var unaryExpression = (UnaryExpression)exp;
            var tmpT = unaryExpression.Operand;
            sqlWhere = DoMethod(tmpT.NodeType)(tmpT, posion, obj);
            return sqlWhere;
        }


        private static object ConstantGetValT(Expression item, BinaryExpressionPosion posion, object obj)
        {
            var name = (ConstantExpression)item;
            var tmpDa = ((ConstantExpression)item).Value;
            //var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(tmpDa));
            //var data = dic[name.];
            var t1 = Convert.ChangeType(tmpDa, item.Type);
            return t1;
        }

        /// <summary>
        /// 获取方法的计算结果，不对结果做任何处理
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object GetMeodResultT(Expression exp, BinaryExpressionPosion posion, object obj)
        {
            var tmpTest = (MethodCallExpression)exp;
            Type type = null;
            Expression tt = null;
            Object tmp = null;
            if (tmpTest.Object != null)
            {
                tt = tmpTest.Object;
                var pamValTu = DoMethod(tt.NodeType)(tt, posion, obj);
                var pamVal = pamValTu;
                type = tmpTest.Object.Type;
                if (type == typeof(string))
                {
                    tmp = System.Activator.CreateInstance(type, pamVal.ToString().ToCharArray());
                }
                else
                {
                    tmp = pamVal;
                }
            }
            else
            {
                type = tmpTest.Method.ReflectedType;
                //tmp = System.Activator.CreateInstance(type);
            }
            List<Type> types = new List<Type>();
            List<object> parameters = new List<object>();
            foreach (var item in tmpTest.Arguments)
            {
                parameters.Add(DoMethod(item.NodeType)(item, posion, obj));
                types.Add(item.Type);
            }
            var t = type.GetMethod(tmpTest.Method.Name, types.ToArray());
            var kkk = t.Invoke(tmp, parameters.ToArray());
            return kkk;
        }

        private static object ParameterGetValT(Expression item, BinaryExpressionPosion posion, object obj)
        {
            var t1 = ((ParameterExpression)item).Name;
            return t1;
            //if (t1.Item1)
            //{
            //    return t1.Item2;
            //}
            //else
            //{
            //    return obj;
            //}
        }

        /// <summary>
        /// ExpressionType为Add,Subtract,Multiply,Divede时的计算结果
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="posion"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static object ASMDCalResultT(Expression exp, BinaryExpressionPosion posion, object t)
        {
            object sqlWhere = null;
            var TmpTest = (BinaryExpression)exp;
            if (posion == BinaryExpressionPosion.Right)
            {
                var left = DoMethod(TmpTest.Left.NodeType)(TmpTest.Left, posion, t);
                var right = DoMethod(TmpTest.Right.NodeType)(TmpTest.Right, posion, t);
                if (left.GetType() == typeof(String) || right.GetType() == typeof(String))
                {
                    var leftStr = left.ToString();
                    var rightStr = right.ToString();
                    sqlWhere = leftStr + rightStr;
                }
                else if (left.GetType() == typeof(double) || right.GetType() == typeof(double))
                {
                    sqlWhere = CalReslut<double, double, double>(Convert.ToDouble(left), Convert.ToDouble(right), TmpTest.NodeType);
                }
                else if (left.GetType() == typeof(int))
                {
                    sqlWhere = CalReslut<int, int, int>(Convert.ToInt32(left), Convert.ToInt32(right), TmpTest.NodeType);
                }
                else if (left.GetType() == typeof(DateTime) || right.GetType() == typeof(DateTime))
                {
                    sqlWhere = CalReslut<DateTime, DateTime, DateTime>(Convert.ToDateTime(left), Convert.ToDateTime(right), TmpTest.NodeType);
                }
            }
            else
            {
                sqlWhere = DoMethod(TmpTest.Left.NodeType)(TmpTest.Left, posion, t) + ExpressionTypeToSqlWhere(exp.NodeType).Item2 + DoMethod(TmpTest.Right.NodeType)(TmpTest.Right, posion, t);
            }

            return sqlWhere;
        }

        private static object NewGetValT(Expression exp, BinaryExpressionPosion posion, object t)
        {
            var tmpExp= (NewExpression)exp;
            List<NewClass> list = new List<NewClass>();
            for (int i = 0; i < tmpExp.Arguments.Count; i++)
            {
                var newC = new NewClass();
                var Argument=tmpExp.Arguments[i];
                if (Argument.NodeType == ExpressionType.MemberAccess)
                {
                    var mebSon = ((MemberExpression)Argument).Expression;
                    newC.TableName =DoMethod(mebSon.NodeType)(mebSon,BinaryExpressionPosion.Left,t).ToString();
                }
                newC.oldName = DoMethod(Argument.NodeType)(Argument, BinaryExpressionPosion.Left, t).ToString();
                var Member = tmpExp.Members[i];
                newC.NewName = Member.Name;
                list.Add(newC);
            }
            return list;
        }
        /// <summary>
        /// 计算等式的数据，BinaryExpressionPosion.Right字段名会替换为值，Left返回字段名
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Object CalBinaryExpressionPosionT(Expression exp, BinaryExpressionPosion posion, object t)
        {
            object sqlWhere = null;
            if (posion == BinaryExpressionPosion.Right)
            {

                var tmpExp = ((MemberExpression)exp);
                var tmpExpSon = tmpExp.Expression;
                if (tmpExpSon.NodeType == ExpressionType.Constant)
                {
                    var tm = ((ConstantExpression)tmpExpSon).Value;
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(tm));
                    var data = dic[tmpExp.Member.Name];

                    sqlWhere = data;
                }
                else
                {
                    sqlWhere = DoMethod(tmpExpSon.NodeType)(tmpExp, posion, t);
                }

            }
            else
            {
                var tmpExp = ((MemberExpression)exp);
                var tmpExpSon = tmpExp.Expression;
                if (tmpExpSon.NodeType == ExpressionType.Constant)
                {
                    var tm = ((ConstantExpression)tmpExpSon).Value;
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(tm));
                    sqlWhere = dic[tmpExp.Member.Name];
                }
                else if (tmpExpSon.NodeType == ExpressionType.Parameter)
                {
                    sqlWhere = $"`{tmpExp.Member.Name.ToString()}`";
                }
                else
                {
                    var tm = DoMethod(tmpExpSon.NodeType)(tmpExpSon, posion, t);
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(tm));
                    sqlWhere = dic[tmpExp.Member.Name];


                }

            }
            return sqlWhere;
        }

       

        /// <summary>
        /// 加减乘除的结果计算
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="OutResltTpe"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="expressionType"></param>
        /// <returns></returns>
        private static object CalReslut<T1, T2, OutResltTpe>(T1 left, T2 right, ExpressionType expressionType)
        {
            var leftType = left.GetType();
            var rightType = right.GetType();
            ParameterExpression a = Expression.Parameter(leftType, "i");   //创建一个表达式树中的参数，作为一个节点，这里是最下层的节点
            ParameterExpression b = Expression.Parameter(rightType, "j");
            BinaryExpression result = null;
            switch (expressionType)
            {
                case ExpressionType.Add: result = Expression.Add(a, b); break;
                case ExpressionType.Divide: result = Expression.Divide(a, b); break;
                case ExpressionType.Multiply: result = Expression.Multiply(a, b); break;
                case ExpressionType.Subtract: result = Expression.Subtract(a, b); break;
            }
            Expression<Func<T1, T2, OutResltTpe>> lambda = Expression.Lambda<Func<T1, T2, OutResltTpe>>(result, a, b);
            var tmpMethod = lambda.Compile();
            return tmpMethod(left, right);
        }

        /// <summary>
        /// 是否需要格式化为字符串
        /// </summary>
        /// <param name="tmpType"></param>
        /// <returns></returns>
        private static bool IsNeedSign(Type tmpType)
        {
            if (tmpType == typeof(int) || tmpType == typeof(double) || tmpType == typeof(float) || tmpType == typeof(DateTime) || tmpType == typeof(Int64))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取类中的属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Tuple<bool, object> GetModelValue(string FieldName, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                var tmp = Ts.GetProperty(FieldName);
                if (tmp == null)
                {
                    return new Tuple<bool, object>(false, obj);
                }
                object o = tmp.GetValue(obj, null);
                return new Tuple<bool, object>(true, o); ;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据ExpressionType判断是否含有，AND，OR，!=，=，>，>=，<等含有返回True否则返回False
        /// </summary>
        /// <param name="et"></param>
        /// <returns></returns>
        private static Tuple<AboutIndex, string> ExpressionTypeToSqlWhere(ExpressionType et)
        {
            string str = "";
            AboutIndex isNeedNextCal = AboutIndex.Dob;
            switch (et)
            {
                case ExpressionType.AndAlso: isNeedNextCal = AboutIndex.Single; str = " AND "; break;
                case ExpressionType.OrElse: isNeedNextCal = AboutIndex.Single; str = " OR "; break;
                case ExpressionType.NotEqual: str = "!="; break;
                case ExpressionType.Equal: str = "="; break;
                case ExpressionType.GreaterThan: str = ">"; break;
                case ExpressionType.GreaterThanOrEqual: str = ">="; break;
                case ExpressionType.LessThan: str = "<"; break;
                case ExpressionType.LessThanOrEqual: str = "<="; break;
                case ExpressionType.Add: isNeedNextCal = AboutIndex.Single; str = " + "; break;
                case ExpressionType.Subtract: isNeedNextCal = AboutIndex.Single; str = " - "; break;
                case ExpressionType.Multiply: isNeedNextCal = AboutIndex.Single; str = " * "; break;
                case ExpressionType.Divide: isNeedNextCal = AboutIndex.Single; str = " / "; break;
                default: isNeedNextCal = AboutIndex.NoHave; ; break;
            }

            return new Tuple<AboutIndex, string>(isNeedNextCal, str);
        }
        /// <summary>
        /// 根据ExpressionType获取对应的处理函数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Func<Expression, BinaryExpressionPosion, object, object> DoMethod(ExpressionType type)
        {
            Func<Expression, BinaryExpressionPosion, object, object> func = null;
            switch (type)
            {
                case ExpressionType.Add: func = ASMDCalResultT; break;
                case ExpressionType.Subtract: func = ASMDCalResultT; break;
                case ExpressionType.Multiply: func = ASMDCalResultT; break;
                case ExpressionType.Divide: func = ASMDCalResultT; break;
                case ExpressionType.AndAlso: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.OrElse: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.NotEqual: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.Equal: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.GreaterThan: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.GreaterThanOrEqual: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.LessThan: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.LessThanOrEqual: func = GetWhereSqlForExpressionT; break;
                case ExpressionType.Convert: func = ExpressionConvntT; break;
                case ExpressionType.Call: func = GetMeodResultT; break;
                case ExpressionType.Constant: func = ConstantGetValT; break;
                case ExpressionType.MemberAccess: func = CalBinaryExpressionPosionT; break;
                case ExpressionType.Parameter: func = ParameterGetValT; break;
                case ExpressionType.New: func = NewGetValT; break;
            }
            return func;
        }


        public enum AboutIndex
        {
            Dob,
            Single,
            NoHave
        }

        public enum BinaryExpressionPosion
        {
            Left,
            Right
        }

    }
    public class NewClass
    {
        public string TableName { get; set; }
        public string NewName { get; set; }
        public string oldName { get; set; }
    }
}
