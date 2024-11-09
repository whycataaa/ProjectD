using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEngine;

namespace MY_Framework.Save
{
    public static class CSVTool
    {
        public static void LoadCSVFromFile(string _path,Action<List<string[]>> _a)
        {
            if(!File.Exists(_path))
            {
                Debug.LogError(_path+"不存在");
                return;
            }

            StreamReader sr=null;
            try
            {
                sr=File.OpenText(_path);
                List<string[]> content=new List<string[]>();
                string line;
                while((line=sr.ReadLine())!=null)
                {
                    content.Add(line.Split(","));
                }
                sr.Close();
                sr.Dispose();
                _a?.Invoke(content);
            }
            catch(Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        static List<string> data=new List<string>();
        /// <summary>
        /// 读取某一行
        /// </summary>
        public static List<string> ReadFile(int line,string fileName)
        {
            CSVTool.LoadCSVFromFile(Application.streamingAssetsPath+"/"+fileName+".csv",a=>
            {
                var row=a[line];
                for(int i=0;i<row.Length;i++)
                {
                    data.Add(row[i]);
                }
            }
            );
            foreach(var item in data)
            {
                Debug.Log(item);
            }
            return data;
        }

        public static DataTable OpenCSV(string fileName)
        {
            DataTable dt=new DataTable();
            using (FileStream fs=new FileStream(Application.streamingAssetsPath+"/"+fileName+".csv",FileMode.Open,FileAccess.Read))
            {
                using (StreamReader sr=new StreamReader(fs,Encoding.UTF8))
                {
                    //记录每次读取的一行记录
                    string strLine = "";
                    //记录每行记录中的各字段内容
                    string[] aryLine = null;
                    string[] tableHead = null;
                    //标示列数
                    int columnCount = 0;
                    //标示是否是读取的第一行
                    bool IsFirst = true;
                    //逐行读取CSV中的数据
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        if (IsFirst == true)
                        {
                            tableHead = strLine.Split(',');
                            IsFirst = false;
                            columnCount = tableHead.Length;
                            //创建列
                            for (int i = 0; i < columnCount; i++)
                            {
                                DataColumn dc = new DataColumn(tableHead[i]);
                                dt.Columns.Add(dc);
                            }
                        }
                        else
                        {
                            aryLine = strLine.Split(',');
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < columnCount; j++)
                            {
                                dr[j] = aryLine[j];
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    if (aryLine != null && aryLine.Length > 0)
                    {
                        dt.DefaultView.Sort = tableHead[0] + " " + "asc";
                    }
                    sr.Close();
                    fs.Close();
                    return dt;

                }
            }
        }
    }
}
