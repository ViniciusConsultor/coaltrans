using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Data.SqlClient;

namespace Coal.Util
{
    public class DataUtility
    {
        /// <summary>
        /// ������ת��Ϊresultobject
        /// </summary>
        /// <param name="dtSource">Դ����</param>
        /// <param name="filter">datatable��ɸѡ����</param>
        /// <param name="sorter">datatable����������</param>
        /// <param name="pageIndex">��ҳ��ʼ����</param>
        /// <param name="pageSize">ÿҳ��¼����0Ϊ�޷�ҳ</param>
        /// <returns>����ResultObject</returns>
        public static ResultObject ConvertToResultObject(DataTable dtSource,string filter,string sorter,int pageIndex,int pageSize)
        {
            ResultObject RO = new ResultObject();
            if (dtSource == null || dtSource.Rows.Count == 0)
            {
                RO["records"] = 0;
                return null;
            }

            DataRow[] row = dtSource.Select(filter, sorter);

            int recordCount = row.Length;
            int start = pageIndex * pageSize;
            ArrayList list = new ArrayList();
            int endIndex = start + pageSize;

            //�޷�ҳ״̬��endindexΪ��¼����
            if (pageSize == 0)
            {
                endIndex = recordCount;
            }

            if (endIndex > recordCount)
                endIndex = recordCount;

            for (int i = start; i < endIndex; i++)
            {
                IDictionary dict = new SortedList();

                foreach(DataColumn column in dtSource.Columns)
                {
                    dict[column.ColumnName] = row[i][column.ColumnName];
                }
                list.Add(dict);
            }

            RO["records"] = recordCount;
            RO.Rows = list;


            return RO;
        }

        public static ResultObject ConvertToResultObject(DataTable dtSource, int pageIndex, int pageSize)
        {
            return ConvertToResultObject(dtSource, string.Empty, string.Empty, pageIndex, pageSize);
        }

        public static ResultObject ConvertToResultObject(DataTable dtSource)
        {
            return ConvertToResultObject(dtSource, string.Empty, string.Empty, 0, 0);
        }

        public static ResultObject ConvertToResultObject(DataTable dtSource, string filter, string sorter)
        {
            return ConvertToResultObject(dtSource, filter, sorter, 0, 0);
        }

        /// <summary>
        /// ������ת��Ϊresultobject ������
        /// </summary>
        /// <param name="List">Դ����</param>
        /// <returns>����ResultObject</returns>
        public static ResultObject ConvertToResultObject(IList listobj)
        {
            ResultObject RO = new ResultObject();
            if (listobj == null || listobj.Count == 0)
            {
                RO["records"] = 0;
                return null;
            }

            ArrayList list = new ArrayList();

            for (int i = 0; i < listobj.Count; i++)
            {
                IDictionary dict = new SortedList();

                PropertyInfo[] properties = listobj[i].GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    dict[p.Name] = p.GetValue(listobj[i], null);
                }
                list.Add(dict);
            }

            RO["records"] = listobj.Count;
            RO.Rows = list;

            return RO;
        }

        #region used for building partial sql to access database.

        /// <summary>
        /// ���ز�����ΪNULL���б�
        /// </summary>
        /// <param name="model"></param>
        /// <returns>����dictionary��Ĭ�Ϸ���count=0��dic</returns>
        public static Dictionary<string, object> GetModelProperties(object model)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();

            //���Ҫ��ӵ��ֶκ�ֵ
            foreach (PropertyInfo p in model.GetType().GetProperties())
            {
                object value = p.GetValue(model, null);
                string key = p.Name;
                if (value != null)
                {
                    properties.Add(p.Name, value);
                }
            }
            return properties;
        }

        /// <summary>
        /// ������Ӽ�¼sql
        /// </summary>
        /// <param name="model">������ΪNull�������б�GetModelProperties(object model)�ķ���ֵ</param>
        /// <param name="tableName">����</param>
        /// <param name="sql">����sql���</param>
        /// <returns>���ز�������</returns>
        private static SqlParameter[] BuildAddSql(Dictionary<string, object> model, string tableName, ref string sql)
        {
            SqlParameter[] reval = null;
            StringBuilder strSql = new StringBuilder();
            if (model.Count > 0)
            {
                strSql.Append(string.Format("INSERT INTO {0}(", tableName));
                string fileds = string.Empty;
                string paraValue = string.Empty;
                reval = new SqlParameter[model.Count];

                int i = 0;
                foreach (KeyValuePair<string, object> keyValue in model)
                {

                    if (i == model.Count - 1)
                    {
                        fileds += keyValue.Key + ")";
                        paraValue += "@" + keyValue.Key + ")";
                    }
                    else
                    {
                        fileds += keyValue.Key + ",";
                        paraValue += "@" + keyValue.Key + ",";
                    }
                    reval[i] = new SqlParameter("@" + keyValue.Key, keyValue.Value);
                    i++;
                }
                strSql.Append(fileds);
                strSql.Append(" VALUES (");
                strSql.Append(paraValue);
            }
            sql = strSql.ToString();

            return reval;
        }

        /// <summary>
        /// ������Ӽ�¼sql
        /// </summary>
        /// <param name="model">������ΪNull�������б�GetModelProperties(object model)�ķ���ֵ</param>
        /// <param name="tableName">����</param>
        /// <param name="sql">����sql���</param>
        /// <returns>���ز�������</returns>
        private static SqlParameter[] BuildUpdateSql(Dictionary<string, object> model, string tableName, string where, ref string sql)
        {
            SqlParameter[] reval = null;
            StringBuilder strSql = new StringBuilder();
            if (model.Count > 0)
            {
                strSql.Append(string.Format("UPDATE {0} SET ", tableName));
                string fileds = string.Empty;
                string paraValue = string.Empty;
                reval = new SqlParameter[model.Count];

                int i = 0;
                foreach (KeyValuePair<string, object> keyValue in model)
                {

                    if (i == model.Count - 1)
                    {
                        strSql.Append(keyValue.Key + "=@" + keyValue.Key);
                    }
                    else
                    {
                        strSql.Append(keyValue.Key + "=@" + keyValue.Key + ",");
                    }
                    reval[i] = new SqlParameter("@" + keyValue.Key, keyValue.Value);
                    i++;
                }
                if (!string.IsNullOrEmpty(where))
                {
                    strSql.Append(" WHERE " + where);
                }
            }
            sql = strSql.ToString();

            return reval;
        }

        private static void BuildSelectSql(Dictionary<string, object> model, string tableName, string where, ref string sql)
        {
            // SqlParameter[] reval = null;
            StringBuilder strSql = new StringBuilder();
            if (model.Count > 0)
            {
                strSql.Append("SELECT ");
                string fileds = string.Empty;
                int i = 0;
                foreach (KeyValuePair<string, object> keyValue in model)
                {

                    if (i == model.Count - 1)
                    {
                        fileds += keyValue.Key + " ";
                    }
                    else
                    {
                        fileds += keyValue.Key + ",";
                    }
                    i++;
                }
                strSql.Append(fileds);
                strSql.Append(string.Format("FROM {0}", tableName));
                if (!string.IsNullOrEmpty(where))
                {
                    strSql.Append(" WHERE " + where);
                }
            }
            sql = strSql.ToString();
        }

        public static SqlParameter[] BuildAdd(object model, string tableName, ref string sql)
        {
            SqlParameter[] parameters = null;
            Dictionary<string, object> properties = GetModelProperties(model);

            if (properties.Count > 0)
            {
                parameters = BuildAddSql(properties, tableName, ref sql);
            }
            return parameters;
        }

        public static SqlParameter[] BuildUpdate(object model, string tableName, string where, ref string sql)
        {
            SqlParameter[] parameters = null;
            Dictionary<string, object> properties = GetModelProperties(model);

            if (properties.Count > 0)
            {
                parameters = BuildUpdateSql(properties, tableName, where, ref sql);
            }
            return parameters;
        }

        public static void BuildSelect(object model, string tableName, string where, ref string sql)
        {
            Dictionary<string, object> properties = GetModelProperties(model);

            if (properties.Count > 0)
            {
                BuildSelectSql(properties, tableName, where, ref sql);
            }
        }

        public static string BuildSelect(string[] selectFields, string tableName, string where)
        {
            string sql = string.Empty;

            if (selectFields.Length > 0)
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append("SELECT ");
                string fileds = string.Empty;
                int i = 0;
                foreach (string keyValue in selectFields)
                {

                    if (i == selectFields.Length - 1)
                    {
                        fileds += keyValue + " ";
                    }
                    else
                    {
                        fileds += keyValue + ",";
                    }
                    i++;
                }
                strSql.Append(fileds);
                strSql.Append(string.Format("FROM {0}", tableName));
                if (!string.IsNullOrEmpty(where))
                {
                    strSql.Append(" WHERE " + where);
                }
                sql = strSql.ToString();
            }
            return sql;

        }

        /// <summary>
        /// �Ƚ�Ҫ��ѯ������ʵ���е������Ƿ���ͬ(ToLower)�����ض�Ӧ��ʵ���е������б�
        /// </summary>
        /// <param name="inputFileds">�������������</param>
        /// <param name="model">��Ӧʵ��</param>
        /// <returns>����ʵ���б�</returns>
        public static List<string> CompareInputFileds(string[] inputFileds, object model)
        {
            PropertyInfo[] ps = model.GetType().GetProperties();
            //�����ݿ���ͬ������
            List<string> columns = new List<string>();
            foreach (string filed in inputFileds)
            {
                foreach (PropertyInfo p in model.GetType().GetProperties())
                {
                    if (p.Name.ToLower() == filed.ToLower())
                    {
                        columns.Add(p.Name);
                    }
                }
            }
            return columns;
        }

        #endregion
    }
}
