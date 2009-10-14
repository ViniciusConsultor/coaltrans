using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Data.OleDb;
using System.Configuration;
namespace DataLogic
{
	/// <summary>
	/// �洢���̷��������
	/// </summary>
	public class BaseExecute
	{
		public BaseExecute() { }

		// �洢���̷���ֵ�Ĳ���
		protected SqlParameter parameterReturnValue;
        // SqlCommand
		public SqlCommand m_cmd;

		public int ReturnValue
		{
			get { return (System.Int32)this.parameterReturnValue.Value; }
		}

		protected void InitCommand(string spName)
		{
			this.m_cmd=new SqlCommand();
			this.m_cmd.CommandText=spName;
			this.m_cmd.CommandType=CommandType.StoredProcedure;
			//--------------------------------------------------------
			this.parameterReturnValue=new SqlParameter();
			this.parameterReturnValue.ParameterName="@ReturnValue";
			this.parameterReturnValue.SqlDbType=SqlDbType.Int;
			this.parameterReturnValue.Size=4;
			this.parameterReturnValue.Direction=ParameterDirection.ReturnValue;
			this.m_cmd.Parameters.Add(this.parameterReturnValue);
			//--------------------------------------------------------
        }

        #region ExecDataTable
        /// <summary>
        /// ��ȡ��ѯ�Ἧ��
        /// </summary>
        /// <returns></returns>
        public DataTable ExecDataTable()
		{
			return ExecDataTable(null);
		}

        /// <summary>
        /// ��ȡ��ѯ�Ἧ��
        /// </summary>
        /// <param name="tableName">��ע����</param>
        /// <returns></returns>
		public DataTable ExecDataTable(string tableName)
		{
			SqlConnection conn=new SqlConnection(SqlConn.GetSqlConnStr());
	
			m_cmd.Connection=conn;
			SqlDataAdapter dat=new SqlDataAdapter(m_cmd);
			
			DataTable dt=new DataTable();

			try
			{
				dat.Fill(dt);
			}
			catch(InvalidOperationException ept)
			{
				throw new DBOpenException(ept.Message,ept);
			}
			catch(SqlException ept)
			{
				//WriteEventLog(ept.Message);
				throw new DBQueryException(ept.Message,ept);
			}
			catch(Exception ept)
			{
				////Console.WriteLine(ept.Message);
				throw ept;
			}
			finally
			{
				// ȷ�����ݿ����ӹر�
				if(conn.State!=ConnectionState.Closed)
					conn.Close();	
			}

			if(tableName!=null)
				dt.TableName=tableName;
						
			return dt;
        }
        #endregion

        #region ExecDataSet
        /// <summary>
        /// ��ȡ��ѯ�Ἧ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecDataSet()
        {
            SqlConnection conn = new SqlConnection(SqlConn.GetSqlConnStr());

            m_cmd.Connection = conn;
            SqlDataAdapter dat = new SqlDataAdapter(m_cmd);

            DataSet ds = new DataSet();

            try
            {
                dat.Fill(ds);
            }
            catch (InvalidOperationException ept)
            {
                throw new DBOpenException(ept.Message, ept);
            }
            catch (SqlException ept)
            {
                //WriteEventLog(ept.Message);
                throw new DBQueryException(ept.Message, ept);
            }
            catch (Exception ept)
            {
                ////Console.WriteLine(ept.Message);
                throw ept;
            }
            finally
            {
                // ȷ�����ݿ����ӹر�
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            return ds;
        }
        #endregion

        #region ExecScalar
        /// <summary>
        /// ��ȡ��ѯ�����һ�е�һ������
        /// </summary>
        /// <returns></returns>
        public object ExecScalar()
		{
			object result = null;
            SqlConnection conn = new SqlConnection(SqlConn.GetSqlConnStr());
			try
			{
				conn.Open();
				m_cmd.Connection = conn;
                result = m_cmd.ExecuteScalar();
			}

			catch(InvalidOperationException ept)
			{
				throw new DBOpenException(ept.Message,ept);
			}
			catch(SqlException ept)
			{
				//WriteEventLog(ept.Message);
				throw new DBQueryException(ept.Message,ept);
			}
			catch(Exception ept)
			{
				////Console.WriteLine(ept.Message);
				throw ept;
			}
			finally
			{
				// ȷ�����ݿ����ӹر�
				if(conn.State!=ConnectionState.Closed)
					conn.Close();	
			}
            return result;
        }
        #endregion

        #region ExecNoQuery
        /// <summary>
        /// ִ��Sql�������Ӱ������
        /// </summary>
        /// <returns></returns>
        public int ExecNoQuery()
		{
			int pot = -1;
			SqlConnection conn=new SqlConnection(SqlConn.GetSqlConnStr());
			try
			{
				conn.Open();
				m_cmd.Connection = conn;
                pot = m_cmd.ExecuteNonQuery();
			}

			catch(InvalidOperationException ept)
			{
				throw new DBOpenException(ept.Message,ept);
			}
			catch(SqlException ept)
			{
				//WriteEventLog(ept.Message);
				throw new DBQueryException(ept.Message,ept);
			}
			catch(Exception ept)
			{
				////Console.WriteLine(ept.Message);
				throw ept;
			}
			finally
			{
				// ȷ�����ݿ����ӹر�
				if(conn.State!=ConnectionState.Closed)
					conn.Close();	
			}
			return pot;
        }
		#endregion

		#region ExecNoQuery ����SqlTransaction����
		/// <summary>
		/// ִ��Sql�������SqlTransaction���񣬷���boolֵ
		/// </summary>
        /// <param name="m_cmds">�ɱ��SqlCommand���飬����ָ��CommandText��CommandType</param>
		/// <returns>true��false</returns>
		public bool ExecNoQuery( params SqlCommand[] m_cmds)
		{
			SqlConnection conn=new SqlConnection(SqlConn.GetSqlConnStr());
			conn.Open();
			using(SqlTransaction trans = conn.BeginTransaction())
			{
				try
				{
					foreach(SqlCommand cmd in m_cmds)
					{
						cmd.Connection = conn;
						cmd.Transaction = trans;
						cmd.ExecuteNonQuery();
					}
					trans.Commit();
					return true;
				}

				catch(InvalidOperationException ept)
				{
					trans.Rollback();
					//throw new DBOpenException(ept.Message,ept);
					return false;
				}
				catch(SqlException ept)
				{
					//WriteEventLog(ept.Message);
					trans.Rollback();
					//throw new DBQueryException(ept.Message,ept);
					return false;
				}
				catch(Exception ept)
				{
					////Console.WriteLine(ept.Message);
					trans.Rollback();
					//throw ept;
					return false;
				}
				finally
				{
					// ȷ�����ݿ����ӹر�
					if(conn.State!=ConnectionState.Closed)
						conn.Close();	
				}
			}
		}

		/// <summary>
		/// ִ��Sql�������SqlTransaction���񣬷���boolֵ
		/// </summary>
		/// <param name="m_cmds">�ɱ��SqlCommand���飬����ָ��CommandText��CommandType</param>
		/// <returns>true��false</returns>
		public bool ExecNoQueryList(SqlCommand[] m_cmds)
		{
			SqlConnection conn = new SqlConnection(SqlConn.GetSqlConnStr());
			conn.Open();
			using (SqlTransaction trans = conn.BeginTransaction())
			{
				try
				{
					foreach (SqlCommand cmd in m_cmds)
					{
						cmd.Connection = conn;
						cmd.Transaction = trans;
						cmd.ExecuteNonQuery();
					}
					trans.Commit();
					return true;
				}

				catch (InvalidOperationException ept)
				{
					trans.Rollback();
					//throw new DBOpenException(ept.Message,ept);
					return false;
				}
				catch (SqlException ept)
				{
					//WriteEventLog(ept.Message);
					trans.Rollback();
					//throw new DBQueryException(ept.Message,ept);
					return false;
				}
				catch (Exception ept)
				{
					////Console.WriteLine(ept.Message);
					trans.Rollback();
					//throw ept;
					return false;
				}
				finally
				{
					// ȷ�����ݿ����ӹر�
					if (conn.State != ConnectionState.Closed)
						conn.Close();
				}
			}
		}

        #endregion

        #region ExecXmlDom
        /// <summary>
        /// ���ز�ѯ��XML
        /// </summary>
        /// <returns></returns>
        public XmlDocument ExecXmlDom()
		{

			return ExecXmlDom("ROOT");
		}

        /// <summary>
        /// ���ز�ѯ��XML
        /// </summary>
        /// <param name="rootName">����XML���ڵ�����</param>
        /// <returns></returns>
		public XmlDocument ExecXmlDom(string rootName)
		{
			string strXml="";
			SqlConnection conn=new SqlConnection(SqlConn.GetSqlConnStr());
			m_cmd.Connection=conn;
			
			try
			{
				conn.Open();
				XmlReader read=m_cmd.ExecuteXmlReader();
				
				while(read.ReadState!=ReadState.EndOfFile)
				{
					read.MoveToContent();
					strXml += read.ReadOuterXml();
				}
				
			}
			catch(InvalidOperationException ept)
			{
				throw new DBOpenException(ept.Message,ept);
			}
			catch(SqlException ept)
			{
				//WriteEventLog(ept.Message);
				throw new DBQueryException(ept.Message,ept);
			}
			catch(Exception ept)
			{
				//Console.WriteLine(ept.Message);
				throw ept;
			}
			finally
			{
				// ȷ�����ݿ����ӹر�
				if(conn.State!=ConnectionState.Closed)
				{
					conn.Close();
				}	
			}

			XmlDocument xmlDoc=new XmlDocument();
			xmlDoc.LoadXml("<"+rootName+">"+strXml+"</"+rootName+">");

			return xmlDoc;
        }
        #endregion


        #region ReceiveParameter
        /// <summary>
		/// �洢���̽��ܲ�����ֵ
		/// </summary>
		/// <param name="dtParameter">����������Ϣ��DataTable</param>
		public void ReceiveParameter(DataTable dtParameter)
		{
			// ö���������Ĳ�������
			foreach(SqlParameter param in this.m_cmd.Parameters)
			{
                string paramName = param.ParameterName;
				// ȥ����������ǰ׺"@"
                string columnName = paramName.Substring(1);

                if (dtParameter.Columns.Contains(columnName))
                    param.Value = dtParameter.Rows[0][columnName];
			}
		}

		/// <summary>
		/// �洢���̽��ܲ�����ֵ
		/// </summary>
		/// <param name="objParameter">����������Ϣ��ʵ�����</param>
		public void ReceiveParameter(object entity)
		{
			// �õ���ʵ�����ľ���������Ϣ
			Type type = entity.GetType();

			// ö���������Ĳ�������
			foreach(SqlParameter param in this.m_cmd.Parameters)
			{
                string paramName = param.ParameterName;
				// ȥ����������ǰ׺"@"
                string columnName = paramName.Substring(1);

				// ��ʵ�������ȥ�����Ƿ������ͬ���ƵĹ�������
                PropertyInfo property = type.GetProperty(columnName);
				if(property!=null)
				{
					param.Value = property.GetValue(entity, null);
					if(property.PropertyType.Name=="DateTime" && param.Value.ToString()=="0001-1-1 0:00:00")
						param.Value = System.DBNull.Value;
					else if( property.PropertyType.Name == "String"&& param.Value == null)
						param.Value = "";
				}
			}
        }
        #endregion

    }

    #region SqlConn
    public class SqlConn
	{
		public SqlConn(){}
		
		/// <summary>
		/// ��Web.Config��ȡSqlServer���ݿ������ַ���
		/// </summary>
		/// <returns>�����ַ���strConn</returns>
		public static string GetSqlConnStr()
		{
            if (System.Configuration.ConfigurationManager.ConnectionStrings["connStr"] != null)
                // return ConfigurationManager.AppSettings["connStr"].ToString();
                return System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ToString();
            else
                return "data source=local; initial catalog=rungoo; user id=sa;password=sa; packet size=4096;Max Pool Size=75; Min Pool Size=5;Enlist=false";
        }
	}
	#endregion

	#region DBOpenException
	public class DBOpenException : ApplicationException
	{
		public DBOpenException(): base()
		{
			
		}
		public DBOpenException(string message) : base(message)
		{
			
		}

		public DBOpenException(string message,Exception inner) : base(message, inner)
		{
			
		}
	}
	#endregion

	#region DBQueryException
	public class DBQueryException : ApplicationException
	{
		public DBQueryException(): base()
		{
			
		}
		public DBQueryException(string message) : base(message)
		{
			
		}

		public DBQueryException(string message,Exception inner) : base(message, inner)
		{
			
		}
	}
	#endregion

}

