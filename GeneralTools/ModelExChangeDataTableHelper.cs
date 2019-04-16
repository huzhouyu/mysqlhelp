using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTools
{
    /// <summary>
    /// 实体转换成DataTable
    /// </summary>
    public static class ModelToDataTableHelper
    {
        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable FillDataTable<T>(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            var tempVul = CreateData(typeof(T));
            if (tempVul.Item3)
            {
                DataTable dt = tempVul.Item2;
                foreach (T model in modelList)
                {
                    DataRow dataRow = dt.NewRow();
                    int i = 0;

                    foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                    {
                        dataRow[i] = propertyInfo.GetValue(model, null);

                        i++;
                    }
                    dt.Rows.Add(dataRow);
                }
                return dt;
            }
            else
            {
                DataTable dt = tempVul.Item2;
                var keyList = tempVul.Item1.Keys.ToList();
                var keyValueList = tempVul.Item1;
                foreach (T model in modelList)
                {
                    DataRow dataRow = dt.NewRow();
                    var temp = typeof(T).GetProperties();
                    foreach (var item in keyList)
                    {
                        dataRow[tempVul.Item1[item]] = temp[keyValueList[item]].GetValue(model, null);
                    }
                    dt.Rows.Add(dataRow);
                }
                return dt;
            }
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Tuple<Dictionary<int, int>, DataTable, bool> CreateData(Type type)
        {
            DataTable dataTable = new DataTable(type.Name);
            Dictionary<string, string> colms = new Dictionary<string, string>();
            Dictionary<int, int> colmsInt = new Dictionary<int, int>();
            bool isAllDT = true;
            int i = 0;
            int k = 0;
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var tmp = GetToTableNameCellName(propertyInfo);
                if (!tmp.Item1)
                {
                    if (colms.Values.Contains(tmp.Item2))
                    {
                        throw new Exception("格式化后的列名存在冲突");
                    }
                    colmsInt.Add(i, k);
                    i++;
                    colms.Add(propertyInfo.Name, tmp.Item2);
                    dataTable.Columns.Add(new DataColumn(tmp.Item2, propertyInfo.PropertyType));
                }
                else
                {
                    isAllDT = false;
                }
                k++;
            }
            return new Tuple<Dictionary<int, int>, DataTable, bool>(colmsInt, dataTable, isAllDT);
        }



        /// <summary>
        /// 根据实体类获取实体和格式化后的表格数据的位置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<int, int> GetModelToDBTableColmsPostion(Type type)
        {
            Dictionary<string, string> colms = new Dictionary<string, string>();
            Dictionary<int, int> colmsInt = new Dictionary<int, int>();
            int i = 0;
            int k = 0;
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var tmp = GetToTableNameCellName(propertyInfo);
                if (!tmp.Item1)
                {
                    if (colms.Values.Contains(tmp.Item2))
                    {
                        throw new Exception("格式化后的列名存在冲突");
                    }
                    colmsInt.Add(i, k);
                    i++;
                    colms.Add(propertyInfo.Name, tmp.Item2);
                }
                else
                {
                }
                k++;
            }
            return colmsInt;
        }



        /// <summary>
        /// 根据格式化的名字与实体属性的位置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetDBTableColmsToModelPostion(Type type)
        {
            Dictionary<string, int> colms = new Dictionary<string, int>();
            int i = 0;
            int k = 0;
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var tmp = GetToTableNameCellName(propertyInfo);
                if (!tmp.Item1)
                {
                    if (colms.Keys.Contains(tmp.Item2))
                    {
                        throw new Exception("格式的列名存在冲突");
                    }
                    colms.Add(tmp.Item2, k);
                }
                else
                {
                }
                k++;
            }
            return colms;
        }


        /// <summary>
        /// DATAtable与实体转换时全属性映射，igon......失效
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetTableColmsToModelPostion(Type type)
        {
            Dictionary<string, int> colms = new Dictionary<string, int>();
            int i = 0;
            int k = 0;
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var tmp = GetToTableNameCellName(propertyInfo);

                if (colms.Keys.Contains(tmp.Item2))
                {
                    throw new Exception("格式的列名存在冲突");
                }
                colms.Add(tmp.Item2, k);
                k++;
            }
            return colms;
        }



        ///// <summary>
        ///// 根据特性判断该属性是否被转换成列
        ///// </summary>
        ///// <param name="p"></param>
        ///// <returns>Item1表示是否忽略该属性，Item2表示不忽略时的值</returns>
        //public static Tuple<bool, string> GetToTableNameCellName(PropertyInfo p)
        //{
        //    object[] attribute = p.GetCustomAttributes(typeof(ModolToDataTableMarkAttribute), false);
        //    if (attribute.Length == 0)
        //    {
        //        return new Tuple<bool, string>(false, p.Name);
        //    }
        //    else
        //    {
        //        ModolToDataTableMarkAttribute att = attribute[0] as ModolToDataTableMarkAttribute;
        //        if (att.IgnoreThisFiled == true)
        //        {
        //            return new Tuple<bool, string>(true, p.Name);
        //        }
        //        return new Tuple<bool, string>(false, att.FiledName);
        //    }
        //}

        /// <summary>
        /// 根据特性判断该属性是否被转换成列
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Item1表示是否忽略该属性，Item2表示不忽略时的值,Item3表示格式成表格的长度</returns>
        public static Tuple<bool, string, int> GetToTableNameCellName(this PropertyInfo p)
        {
            object[] attribute = p.GetCustomAttributes(typeof(ModolToDataTableMarkAttribute), false);
            if (attribute.Length == 0)
            {//\Optimize\LTE\Predicted\Grid
                return new Tuple<bool, string, int>(false, p.Name, 100);
            }
            else
            {
                ModolToDataTableMarkAttribute att = attribute[0] as ModolToDataTableMarkAttribute;
                if (att.IgnoreThisFiled == true)
                {
                    return new Tuple<bool, string, int>(true, p.Name, 100);
                }
                if (att.FiledName == null)
                {
                    return new Tuple<bool, string, int>(false, p.Name, att.FiledLenth);
                }
                return new Tuple<bool, string, int>(false, att.FiledName, att.FiledLenth);
            }
        }


        /// <summary>
        /// 根据特性获取该属性的值，不能忽略即ignoreThisFiled=true无效
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Item1表示是否忽略该属性，Item2表示不忽略时的值,Item3表示格式成表格的长度</returns>
        public static Tuple<bool, string, int> GetCellName(this PropertyInfo p)
        {
            object[] attribute = p.GetCustomAttributes(typeof(ModolToDataTableMarkAttribute), false);
            if (attribute.Length == 0)
            {
                return new Tuple<bool, string, int>(false, p.Name, 100);
            }
            else
            {
                ModolToDataTableMarkAttribute att = attribute[0] as ModolToDataTableMarkAttribute;
                if (att.FiledName == null)
                {
                    return new Tuple<bool, string, int>(false, p.Name, att.FiledLenth);
                }
                return new Tuple<bool, string, int>(false, att.FiledName, att.FiledLenth);
            }
        }


    }
    /// <summary>
    /// 对实体属性进行标注，表组字段为转换为表格的列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ModolToDataTableMarkAttribute : Attribute
    {
        /// <summary>
        /// 对实体属性进行标注，表组字段为转换为表格的列名
        /// </summary>
        /// <param name="FiledName">table的列名</param>
        /// <param name="Description">描述字段</param>
        /// <param name="IgnoreThisFiled">是否忽略该字段</param>
        /// <param name="filedLenth">生成字段的长度，默认100</param>
        public ModolToDataTableMarkAttribute(string FiledName = null, string Description = "无", bool IgnoreThisFiled = false, int filedLenth = 100)
        {
            this.FiledName = FiledName == null ? Guid.NewGuid().ToString() : FiledName;
            this.Description = Description;
            this.IgnoreThisFiled = IgnoreThisFiled;
            this.FiledLenth = filedLenth;
        }
        private int _FiledLenth;

        private string _FiledName;
        public string FiledName
        {
            get { return _FiledName; }
            set { _FiledName = value; }
        }
        public int FiledLenth
        {
            get { return _FiledLenth; }
            set { _FiledLenth = value; }
        }
        private bool _ignoreThisFiled;
        public bool IgnoreThisFiled
        {
            get { return _ignoreThisFiled; }
            set { _ignoreThisFiled = value; }
        }
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
    /// <summary>
    /// DataTable转换成实体
    /// </summary>
    public static class DataTableToModelHelper
    {
        /// <summary>
        /// DataTable通过反射获取单个像
        /// <param name="ignoreCase">是否忽略大小写，默认不区分</param>
        /// </summary>
        public static T ToSingleModel<T>(this DataTable data, bool ignoreCase = true) where T : new()
        {
            T t = data.GetList<T>(ignoreCase).Single();
            return t;
        }

        /// <summary>
        /// DataTable通过反射获取多个对像
        /// </summary>
        /// <typeparam name="type"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> ToModelList<T>(this DataTable data) where T : new()
        {
            List<T> t = data.GetList<T>(true);
            return t;
        }


        /// <summary>
        /// DataTable通过反射获取多个对像
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="ignoreCase">是否忽略大小写，默认不区分</param>
        /// <returns></returns>
        private static List<T> ToListModel<T>(this DataTable data, bool ignoreCase = true) where T : new()
        {
            List<T> t = data.GetList<T>(ignoreCase);
            return t;
        }



        private static List<T> GetList<T>(this DataTable data, bool ignoreCase = true) where T : new()
        {
            List<T> t = new List<T>();
            if (data == null)
            {
                return t;
            }
            int columnscount = data.Columns.Count;
            if (ignoreCase)
            {
                for (int i = 0; i < columnscount; i++)
                    data.Columns[i].ColumnName = data.Columns[i].ColumnName.ToUpper();
            }

            try
            {
                var properties = new T().GetType().GetProperties();

                var rowscount = data.Rows.Count;
                IDictionary<string, int> dicTmep = new Dictionary<string, int>();

                foreach (var item in ModelToDataTableHelper.GetTableColmsToModelPostion(typeof(T)))
                {
                    if (ignoreCase)
                    {
                        dicTmep.Add(item.Key.ToUpper(), item.Value);
                    }
                    else
                    {
                        dicTmep.Add(item.Key, item.Value);
                    }
                }
                IList<int> tmpList = new List<int>();
                IDictionary<int, int> tmpDic = new Dictionary<int, int>();
                bool isFirst = true;
                foreach (DataRow item in data.Rows)
                {
                    var model = new T();
                    if (isFirst)
                    {
                        int i = 0;
                        foreach (DataColumn dc in data.Columns)
                        {
                            int positiom = 0;

                                if (dicTmep.TryGetValue(dc.ColumnName, out positiom))
                                {
                                    properties[positiom].SetValue(model, Convert.ChangeType(item[dc.ColumnName], properties[positiom].PropertyType));
                                    tmpDic.Add(i, positiom);
                                }
                                i++;

                        }
                        isFirst = false;
                    }
                    else
                    {

                            foreach (var item2 in tmpDic)
                            {
                                properties[item2.Value].SetValue(model, Convert.ChangeType(item[item2.Key], properties[item2.Value].PropertyType), null);
                            }

                    }
                    t.Add(model);
                }
            }
            catch (Exception ex)
            {


                throw ex;
            }


            return t;
        }
    }


    public static class MySqlDBhelper
    {
        private static bool IsExistsTable(string dbName, string tableName)
        {
            string sql = "select t.table_name from information_schema.TABLES t where t.TABLE_SCHEMA ='" + dbName + "' and t.TABLE_NAME ='" + tableName + "'";
            return false;
        }
    }
}
