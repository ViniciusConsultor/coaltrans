using System;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
namespace Coal.Util
{
	/// <summary>
	/// ��ʾ��Ϣ��ʾ�Ի���
	/// </summary>
	public class MessageBox
	{		
		/// <summary>
		/// ��ʾ��Ϣ��ʾ�Ի���
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="msg">��ʾ��Ϣ</param>
		//public static void  Show(System.Web.UI.Page page,string msg)
        public static void Show(string msg)
		{
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
            HttpContext.Current.Response.Write("<script language='javascript'>alert('" + msg.ToString() + "');</script>");
            HttpContext.Current.Response.End();
		}

		/// <summary>
		/// �ؼ���� ��Ϣȷ����ʾ��
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="msg">��ʾ��Ϣ</param>
		public static void  ShowConfirm(System.Web.UI.WebControls.WebControl Control,string msg)
		{
			Control.Attributes.Add("onclick", "return confirm('" + msg + "');") ;
		}

		/// <summary>
		/// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="msg">��ʾ��Ϣ</param>
		/// <param name="url">��ת��Ŀ��URL</param>
        //public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        public static void ShowAndRedirect(string msg, string url)
		{
			StringBuilder Builder=new StringBuilder();
            Builder.Append("<script language='javascript'>");
			Builder.AppendFormat("alert('{0}');",msg);
			Builder.AppendFormat("window.location.href='{0}'",url);
			Builder.Append("</script>");
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
            HttpContext.Current.Response.Write(Builder.ToString());
            HttpContext.Current.Response.End();
		}
		/// <summary>
		/// ����Զ���ű���Ϣ
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="script">����ű�</param>
		//public static void ResponseScript(System.Web.UI.Page page,string script)
        public static void ResponseScript(string script)
		{
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
            HttpContext.Current.Response.Write("<script language='javascript'>" + script + "</script>");         
		}

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����Զ���ű���Ϣ
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="script">����ű�</param>
        //public static void ResponseScript(System.Web.UI.Page page,string script)
        public static void ShowAndScript(string msg, string script)
        {
            HttpContext.Current.Response.Write("<script language='javascript'>alert('" + msg + "');" + script + ";</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        //public static void ShowAndBack(System.Web.UI.Page page, string msg)
        public static void ShowAndBack(string msg)
        {
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert(\"" + msg + "\");window.history.back(1);</script>");
            HttpContext.Current.Response.Write("<script language='javascript'>alert(\"" + msg + "\");window.history.back(1);</script>");
            HttpContext.Current.Response.End();
        }
	}
}
