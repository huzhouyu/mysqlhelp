using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTools.CSVAndEXCLEHelp
{
    public static class CSVFileHelper
    {
        /// <summary>
        /// datatable转csv文件
        /// </summary>
        /// <param name="dtCSV"></param>
        /// <param name="csvFileFullName"></param>
        /// <param name="writeHeader"></param>
        /// <param name="isdeleteOld"></param>
        /// <param name="delimeter"></param>
        public static void DataTableToCSV(DataTable dtCSV, string csvFileFullName, bool writeHeader, bool isdeleteOld, string delimeter)
        {
            string path = Path.GetDirectoryName(csvFileFullName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (null != dtCSV)
            {
                if (isdeleteOld)
                {
                    //Delete the old one
                    if (File.Exists(csvFileFullName))
                    {
                        File.Delete(csvFileFullName);
                    }
                }
                string tmpLineText = "";

                //Write header
                if (writeHeader)
                {
                    tmpLineText = "";
                    for (int i = 0; i < dtCSV.Columns.Count; i++)
                    {
                        string tmpColumnValue = dtCSV.Columns[i].ColumnName;
                        if (tmpColumnValue.Contains(delimeter))
                        {
                            tmpColumnValue = "\"" + tmpColumnValue + "\"";
                        }

                        if (i == dtCSV.Columns.Count - 1)
                        {
                            tmpLineText += tmpColumnValue;
                        }
                        else
                        {
                            tmpLineText += tmpColumnValue + delimeter;
                        }
                    }
                    WriteFile(csvFileFullName, tmpLineText);
                }

                //Write content
                for (int j = 0; j < dtCSV.Rows.Count; j++)
                {
                    tmpLineText = "";
                    for (int k = 0; k < dtCSV.Columns.Count; k++)
                    {
                        string tmpRowValue = dtCSV.Rows[j][k].ToString();
                        if (tmpRowValue.Contains(delimeter))
                        {
                            tmpRowValue = "\"" + tmpRowValue + "\"";
                        }

                        if (k == dtCSV.Columns.Count - 1)
                        {
                            tmpLineText += tmpRowValue;
                        }
                        else
                        {
                            tmpLineText += tmpRowValue + delimeter;
                        }
                    }
                    WriteFile(csvFileFullName, tmpLineText);
                }
            }
        }
        private static void WriteFile(string fileFullName, string message)
        {
            using (StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.UTF8))
            {
                sw.WriteLine(message);
            }
        }
        /// <summary>
        /// 打开csv文件
        /// </summary>
        /// <param name="fullFileName">文件全名</param>
        /// <param name="TitleRows"></param>
        /// <param name="separators"></param>
        /// <param name="firstRow">开始列</param>
        /// <param name="firstColumn"></param>
        /// <param name="getRows">获取多少行</param>
        /// <param name="getColumns">获取多少列</param>
        /// <param name="haveTitleRow">是有标题行</param>
        /// <returns></returns>
        public static DataTable OpenCSV(string fullFileName, string[] TitleRows, string separators = ",", Int16 firstRow = 0, Int16 firstColumn = 0, Int16 getRows = 0, Int16 getColumns = 0, bool haveTitleRow = false)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fullFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //是否已建立了表的字段
            bool bCreateTableColumns = false;
            //第几行
            int iRow = 1;

            //去除无用行
            if (firstRow > 0)
            {
                for (int i = 1; i < firstRow; i++)
                {
                    sr.ReadLine();
                }
            }

            // { ",", ".", "!", "?", ";", ":", " " };
            string[] separatorsList = separators.Split('|');
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                strLine = strLine.Trim();
                aryLine = strLine.Split(separatorsList, System.StringSplitOptions.None);

                if (bCreateTableColumns == false)
                {
                    bCreateTableColumns = true;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = firstColumn; i < (getColumns == 0 ? columnCount : firstColumn + getColumns); i++)
                    {
                        DataColumn dc
                            = new DataColumn(haveTitleRow == true ? aryLine[i] : TitleRows != null ? TitleRows[i] : i.ToString());
                        dt.Columns.Add(dc);
                    }

                    bCreateTableColumns = true;

                    if (haveTitleRow == true)
                    {
                        continue;
                    }
                }


                DataRow dr = dt.NewRow();
                for (int j = firstColumn; j < (getColumns == 0 ? columnCount : firstColumn + getColumns); j++)
                {
                    dr[j - firstColumn] = aryLine[j];
                }
                dt.Rows.Add(dr);

                iRow = iRow + 1;
                if (getRows > 0)
                {
                    if (iRow > getRows)
                    {
                        break;
                    }
                }

            }

            sr.Close();
            fs.Close();
            return dt;
        }


        /// <summary>
        /// Datatable转化成ExceL文件
        /// </summary>
        /// <param name="m_DataTable"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>

        public static string DataToExcel(System.Data.DataTable m_DataTable, string savePath)
        {
            string FileName = savePath;  //文件存放路径     
            string path = Path.GetDirectoryName(FileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (System.IO.File.Exists(FileName))                                //存在则删除            
            {
                System.IO.File.Delete(FileName);
            }
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objStreamWriter;
            string strLine = "";
            objFileStream = new System.IO.FileStream(FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            objStreamWriter = new System.IO.StreamWriter(objFileStream, Encoding.Unicode);
            for (int i = 0; i < m_DataTable.Columns.Count; i++)
            {
                strLine = strLine + m_DataTable.Columns[i].Caption.ToString() + Convert.ToChar(9);      //写列标题            
            }
            objStreamWriter.WriteLine(strLine);
            strLine = "";
            for (int i = 0; i < m_DataTable.Rows.Count; i++)
            {
                for (int j = 0; j < m_DataTable.Columns.Count; j++)
                {
                    if (m_DataTable.Rows[i].ItemArray[j] == null)
                        strLine = strLine + " " + Convert.ToChar(9);                                    //写内容                    
                    else
                    {
                        string rowstr = "";
                        rowstr = m_DataTable.Rows[i].ItemArray[j].ToString();
                        if (rowstr.IndexOf("\r\n") > 0)
                            rowstr = rowstr.Replace("\r\n", " ");
                        if (rowstr.IndexOf("\t") > 0)
                            rowstr = rowstr.Replace("\t", " ");
                        strLine = strLine + rowstr + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }
            objStreamWriter.Close();
            objFileStream.Close();
            return FileName;        //返回生成文件的绝对路径        
        }
    }
}
