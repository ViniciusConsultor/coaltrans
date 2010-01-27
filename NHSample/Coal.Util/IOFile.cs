using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Coal.Util
{
    /// <summary>
    /// �ļ���д��
    /// </summary>
    public class IOFile
    {
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="strPath">���·��</param>
        /// <param name="strFileName">������ļ���</param>
        /// <param name="strHtml">�ļ�����</param>
        /// <param name="coding">�ַ�����</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public static bool SaveFile(string strPath, string strFileName, string strHtml, Encoding coding)
        {
            try
            {
                CheckDir(strPath);//���·��
                string path = Path.Combine(strPath, strFileName);
                StreamWriter sw = new StreamWriter(path, false, coding);
                sw.Write(strHtml);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ���·���治���ڣ������ڼ�����
        /// </summary>
        /// <param name="strPath"></param>
        public static void CheckDir(string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
        }

        /// <summary>
        /// ��ȡ�ļ�
        /// </summary>
        /// <param name="strFilePath">��Ҫ����html�ļ���·��</param>
        /// <param name="strFileName">�ļ���</param>
        /// <param name="coding">����</param>
        /// <returns>��ȡ�������ַ�</returns>
        public static string GetFile(string strFilePath, string strFileName, Encoding coding)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                string file = Path.Combine(strFilePath, strFileName);
                if (System.IO.File.Exists(file))
                {
                    using (StreamReader sr = new StreamReader(file, coding))
                    {
                        strHtml.Append(sr.ReadToEnd());
                        sr.Close();
                        //while ((strlLine = sr.ReadLine()) != null)
                        //{
                        //    strHtml.Append(strlLine);
                        //}
                        //sr.Close();
                    }
                    return strHtml.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// ����ļ���չ��
        /// </summary>
        /// <param name="path">�ļ�·��</param>
        /// <returns></returns>
        public static string Extension(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    return string.Empty;
                }

                return System.IO.Path.GetExtension(path);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="uploadFile">�ϴ��ؼ�</param>
        public static void UploadFile(System.Web.UI.WebControls.FileUpload uploadFile, string fileName, string savePath)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(savePath);
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                if (uploadFile.PostedFile.ContentLength > 0)
                {
                    string file = HttpContext.Current.Server.MapPath(savePath + "\\" + fileName);

                    if (File.Exists(file) == false)///����Ƿ����ͬ���ļ�
                    {
                        uploadFile.PostedFile.SaveAs(file);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="uploadFile">�ϴ��ؼ�</param>
        public static void UploadFile(System.Web.UI.HtmlControls.HtmlInputFile uploadFile, string fileName, string savePath)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(savePath);
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                    
                }
                if (uploadFile.PostedFile.ContentLength > 0)
                {
                    string file = HttpContext.Current.Server.MapPath(savePath + "\\" + fileName);

                    if (File.Exists(file) == false)///����Ƿ����ͬ���ļ�
                    {
                        uploadFile.PostedFile.SaveAs(file);
                    }
                }
            }
            catch
            {
            }
        }

        public static void UpLoadFileByPath(string FilePath,string FileName, string SavaPath)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(SavaPath);
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                FileInfo file = new FileInfo(FilePath);
                if (file.Length > 0)
                {
                    string ToPath=HttpContext.Current.Server.MapPath(SavaPath + "\\" + FileName);
                    //����WebClientʵ��
                    WebClient MywebClient = new WebClient();
                    //����WebClientʵ����windows������֤����
                    MywebClient.Credentials = CredentialCache.DefaultCredentials;
                    //����Ҫ�ϴ��ļ����ļ���
                    FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //���ϴ��ļ������ж����Ʊ���
                    BinaryReader r = new BinaryReader(fs);
                    //��BinaryReader�����ݴ��뵽byte[]��ȥ
                    byte[] PostArry = r.ReadBytes((int)fs.Length);
                    //ͨ��PUT��������ToPath������ļ��ϴ���������
                    Stream postStream = MywebClient.OpenWrite(ToPath, "PUT");
                    if (postStream.CanWrite)
                    {
                        postStream.Write(PostArry, 0, PostArry.Length);
                    }                    
                    postStream.Close();
                }                
            }
            catch(Exception e)
            {
                LogUtility.WriteErrLog(e);
            }
        }
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        public static void DeleteFile(string fileName, string filePath)
        {
            try
            {
                DirectoryInfo Path = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath).ToString());
                string delfile = Path + fileName;
                if (File.Exists(delfile))
                {
                    File.Delete(delfile);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// ��ȡ�ļ���Ϣ������DataTable����
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFile(string filePath)
        {
            try
            {
                DataTable DTFileInfo = new DataTable();
                DTFileInfo.Columns.Add(new DataColumn("FileName", typeof(String)));
                DTFileInfo.Columns.Add(new DataColumn("FileSize", typeof(Int32)));
                DTFileInfo.Columns.Add(new DataColumn("FileType", typeof(String)));
                DTFileInfo.Columns.Add(new DataColumn("FileCreateTime", typeof(String)));
                DataRow FileRow;

                DirectoryInfo FilePath = new DirectoryInfo(HttpContext.Current.Server.MapPath(filePath));

                foreach (FileInfo FileInfomation in FilePath.GetFiles())
                {
                    FileRow = DTFileInfo.NewRow();
                    FileRow["FileName"] = FileInfomation.Name.ToString();
                    FileRow["FileSize"] = Int32.Parse(FileInfomation.Length.ToString());
                    FileRow["FileType"] = FileInfomation.Extension.ToString();
                    FileRow["FileCreateTime"] = FileInfomation.CreationTime.ToString();

                    DTFileInfo.Rows.Add(FileRow);
                }

                return DTFileInfo;
            }
            catch
            {
                return null;
            }
        }

       

        /// <summary>
        /// ���ļ����ص�DropDownList
        /// </summary>
        /// <param name="list"></param>
        /// <param name="hasFirst"></param>
        /// <param name="firstValue"></param>
        /// <param name="firstText"></param>
        /// <param name="filePath"></param>
        //public static void DropDownListFileList(DropDownList list, bool hasFirst, string firstValue, string firstText, string filePath)
        //{
        //    try
        //    {
        //        list.Items.Clear();
        //        if (hasFirst)
        //        {
        //            ListItem item = new ListItem();
        //            item.Value = firstValue;
        //            item.Text = firstText;
        //            list.Items.Add(item);
        //        }
        //        DataTable dt = GetFile(filePath);
        //        if ((dt != null) && (dt.Rows.Count > 0))
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                ListItem item = new ListItem(dt.Rows[i]["FileName"].ToString(), filePath + "/" + dt.Rows[i]["FileName"].ToString());
        //                list.Items.Add(item);
        //            }
        //        }
        //    }
        //    catch
        //    { }
        //}
    }
}
