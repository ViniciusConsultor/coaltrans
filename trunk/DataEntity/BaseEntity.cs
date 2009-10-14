using System;
using System.Data;
using System.Xml;
using System.Reflection;
//using System.Collections.Generic;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// ����ʵ������
	/// </summary>
	[Serializable]
	public abstract class BaseEntity
	{
		private bool m_IsEmpty = true;
		private int m_ReturnValue=0;

		public BaseEntity() { }

		public bool IsEmpty
		{
			get { return m_IsEmpty; }
			set { m_IsEmpty=value; }
		}

		public int ReturnValue
		{
			get { return m_ReturnValue; }
			set { m_ReturnValue=value; }
		}

		/// <summary>
		/// ��ʼ��
		/// </summary>
		protected void InitMetaData()
		{
			foreach(PropertyInfo property in this.GetType().GetProperties())
			{									
				string typeString=property.PropertyType.ToString();
				switch(typeString)
				{				
					case "System.String":
						Console.WriteLine(property.Name);
						property.SetValue(this,"",null);
						break;	
					case "System.DateTime":
						property.SetValue(this,new DateTime(1900,1,1),null);
						break;		
				} 							
			}
		}

        /// <summary>
		/// ʵ�������ݱ���໥ת��
        /// </summary>
        #region MetaDataTable
        public DataTable MetaDataTable
		{
			get //��ʵ��ת��Ϊ���ݱ�
			{ 				
				DataTable dt = this.GetDataTableSchema;
			
				// �������,�ѹ������Ժ͹����ֶε�ֵ�洢
				DataRow row = dt.NewRow();
				foreach(DataColumn column in dt.Columns)
				{
					string columnName = this.TransformFirstToUpper(column.ColumnName);
                    PropertyInfo property = this.GetType().GetProperty(columnName);
                    if (property != null)
                    {
                        if (column.DataType.ToString() != "System.Byte[]")
                            row[columnName] = property.GetValue(this, null);
                        else
                            row[columnName] = DBNull.Value;
                    }
				}				
				dt.Rows.Add(row);
				return dt;  //���ش�һ�����ݵı�DataTable
			}
            set //�����ݱ�ת��Ϊʵ�� ���磺obj.MetaDataTable = exec.ExecDataTable()
			{
				DataTable dtReceive = value;

				// �����������Ϊ0�����ʾ��ʵ��û�г�ʼ����IsEmpty=true
                if (dtReceive == null || dtReceive.Rows.Count == 0)
                {
                    this.m_IsEmpty = true;
                    return;
                }
                else
                {
                    this.m_IsEmpty = false;
                }

				// ö��ʵ��������,�ѽ��ܵ�datatable�е����ݸ��o���������Լܹ���datatable
                foreach (DataColumn column in dtReceive.Columns)
                {
                    string columnName = this.TransformFirstToUpper(column.ColumnName);
                    PropertyInfo property = this.GetType().GetProperty(columnName);
					if( property != null && dtReceive.Rows[0][columnName] != DBNull.Value)
					{
						if(property.PropertyType.Name=="DateTime"&&dtReceive.Rows[0][columnName].ToString()=="")
						{}
						else
							property.SetValue(this, dtReceive.Rows[0][columnName], null);
					}
				}				
			}
        }
        #endregion

        /// <summary>
        /// ��ʵ��ṹת��Ϊ��ṹ
        /// </summary>
        #region GetSchema
        public DataTable GetDataTableSchema
		{
			get
			{
				string tableName = this.GetType().Name;
				DataTable dt = new DataTable(tableName);
				// �ܹ���ṹ
				// ö�ٹ�������-- public property	
                foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
				{
                    string typeString = propertyInfo.PropertyType.ToString();

					// ���Ը����Ե����������Ƿ��ܱ��汾��֧��
					if(this.IsSupportType(typeString) == true)
					{
                        dt.Columns.Add(propertyInfo.Name, Type.GetType(typeString));
                        dt.Columns[propertyInfo.Name].AllowDBNull = true;
					}								
				}
				return dt;
			}
        }

        /// <summary>
        /// �������������Ƿ��ܴ˰汾֧��
        /// </summary>
        /// <param name="typeString">��������</param>
        /// <returns></returns>
        private bool IsSupportType(string typeString)
		{
			bool isSupport=false;
			switch(typeString)
			{
				case "System.Boolean":
					isSupport = true;
					break;
				case "System.Byte":
					isSupport = true;
					break;
				case "System.String":
					isSupport = true;
					break;
				case "System.Byte[]":
					isSupport = true;
					break;
				case "System.Char":
					isSupport = true;
					break;
				case "System.DateTime":
					isSupport = true;
					break;
				case "System.Decimal":
					isSupport = true;
					break;
                case "System.Single":
                    isSupport = true;
                    break;
				case "System.Int32":
					isSupport = true;
					break;
				case "System.Int64":
					isSupport = true;
					break;
				case "System.Int16":
					isSupport = true;
					break;							
			} 
			return isSupport;
        }
        #endregion

		public void WriteXmlData(string fileName)
		{
			DataSet ds=new DataSet("BaseEntitySet");
			DataTable dt=this.MetaDataTable;
			ds.Tables.Add(dt);
			ds.WriteXml(fileName,XmlWriteMode.WriteSchema);		
		}

		public XmlDocument XmlDom
		{
			get
			{
				DataSet ds=new DataSet("BaseEntitySet");
				DataTable dt=this.MetaDataTable;
				ds.Tables.Add(dt);

				XmlDocument doc=new XmlDocument();
				doc.LoadXml(ds.GetXml());
				return doc;
			}
		}

        /// <summary>
        /// ��ָ���ַ���ת����һ����ĸΪ��д��
        /// ����ַ���ֻ��������ĸ����ת��Ϊ��д
        /// </summary>
        /// <param name="text">Ҫת�����ַ���</param>
        /// <returns></returns>
        #region TransformFirstToUpper
        public string TransformFirstToUpper(string str)
        {
            string tmp = "";
            if (str.Length > 2)
                tmp = str.Substring(0, 1).ToUpper() + str.Substring(1);
            else
            {
                if (str.Length == 0)
                    tmp = str;
                else
                    tmp = str.ToUpper();
            }
            return tmp;
        }
        #endregion
	}
}
