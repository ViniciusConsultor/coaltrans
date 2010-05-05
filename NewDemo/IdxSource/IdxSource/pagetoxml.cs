using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Web;

namespace IdxSource
{
    public enum XMLNodeMode { BaiduNews,Baidu,SogouNews,TianyaGoogle };

    public class Pagetoxml
    {
        static string RegBaiduNews = "<td class=\"text\"><a href=\"([\\s\\S]+?) \"  mon=\"a=5[\\s\\S]+?<span><b>([\\s\\S]+?)</b></span></a> <font color=#6f6f6f> <nobr>([\\s\\S]+?) ([\\s\\S]+?)</nobr></font><br><font size=-1>([\\s\\S]+?)\\.\\.\\.</font>";
        static string RegSameBaiduNews = "<a href=\"/ns\\?([\\s\\S]+?) \"><font color=#008000>(\\d+)����ͬ����";
        static string RegBaiduWeb = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" [\\s\\S]+? href=\"([\\S]+?)\"  target=\"_blank\" ><font size=\"3\">([\\s\\S]+?)</font></a><br><font size=-1>([\\s\\S]+?)<br><font color=\"#008000\">([\\s\\S]+?( \\.\\.\\. )?[\\S]+?( \\.\\.\\. )?) ([\\S]+?) ( |����)</font>([\\s\\S]+?) <br></font></td></tr></table><br>";
        //RegBaiduWeb = <table border="0" cellpadding="0" cellspacing="0" [\s\S]+? href="([\S]+?)"  target="_blank" ><font size="3">([\s\S]+?)</font></a><br><font size=-1>([\s\S]+?)<br><font color="#008000">([\s\S]+?( \.\.\. )?[\S]+?( \.\.\. )?) ([\S]+?) [ |����]</font> ([\s\S]+?) <br></font></td></tr></table><br>
        static string RegSogouNews = "<a class=\"pp\" href=\"([\\s\\S]+?)\"[\\s\\S]+?<b>([\\s\\S]+?)</b>[\\s\\S]+?<p class=\"newstime\">([\\s\\S]+?) &nbsp;([\\s\\S]+?)</p>[\\s\\S]+?<p>([\\s\\S]+?)</p></td>";


        /// <summary>
        /// �ؼ���+ҳ���ķ�ʽ
        /// </summary>
        /// <param name="URLSeed"></param>
        /// <param name="wordList"></param>
        /// <param name="pageCount"></param>
        /// <param name="FileSeed"></param>
        /// <param name="srcReg"></param>
        /// <param name="nodeMode"></param>
        public static void Do(string URLSeed,string[] wordList,int pageSize,int pageCount,string FileSeed,string srcReg,XMLNodeMode nodeMode,Encoding encoding,bool genSame)
        {
            for (int i = 0; i < wordList.Length; i++)
            {
                string keyWord = wordList[i];
                string urlword = HttpUtility.UrlEncode(keyWord, encoding);
                for (int page = 0; page < pageCount; page++)
                {
                    string srcUrl = string.Format(URLSeed, urlword, (page * pageSize).ToString());
                    string fileName = string.Format(FileSeed, keyWord, page.ToString());

                    GenerateXml(srcUrl, srcReg, fileName, nodeMode, keyWord, genSame);
                }
            }
        }



        public static void GenerateXml(string srcUrl, string srcReg, string fileName,XMLNodeMode nodeMode,string keyWord,bool GenSubSameNews)
        {
            string content = GetPageContent(srcUrl);

            Regex r;
            Match m;


            r = new Regex(srcReg, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            StringBuilder xml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml.Append("<docs>");


            for (m = r.Match(content); m.Success; m = m.NextMatch())
            {
                string xmlNode = GetNodeValue(m, nodeMode,keyWord);
                xml.Append(xmlNode);
            }

            //GensubSame
            if (GenSubSameNews)
            {
                xml.Append(GenSameNews(content,srcReg, nodeMode,keyWord));
            }

            xml.Append("</docs>");

            FileStream nFile = new FileStream(fileName, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(nFile))
            {
                writer.Write(xml.ToString());
            }
        }

        private static string GenSameNews(string content, string srcReg, XMLNodeMode nodeMode, string keyWord)
        {
            switch (nodeMode)
            {
                case XMLNodeMode.BaiduNews:
                    return GetSameBaiduNews(content, srcReg, nodeMode,keyWord);
                default:
                    throw new Exception("The method or operation is not implemented.");
            }
        }

        private static string GetSameBaiduNews(string content, string srcReg, XMLNodeMode nodeMode, string keyWord)
        {
            StringBuilder sb = new StringBuilder(2048);
            Regex samer;
            Match samem;
            string sameBaiduNewsReg = "<a href=\"/ns\\?([\\s\\S]+?) \"><font color=#008000>(\\d+)����ͬ����";

            samer = new Regex(sameBaiduNewsReg, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex r = new Regex(srcReg, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;
            StringBuilder xmlNods = new StringBuilder(2048);

            for (samem = samer.Match(content); samem.Success; samem = samem.NextMatch())
            {
                string tmpurl = samem.Groups[1].Value;
                string sameNumber = samem.Groups[2].Value;
                if (int.Parse(sameNumber) > 29)
                {
                    string tmp100url = tmpurl.Replace(@"&rn=30", @"&rn=10");
                    string sameUrl = "http://news.baidu.com/ns?" + tmp100url;

                    string samecontent = GetPageContent(sameUrl);

                    for (m = r.Match(samecontent); m.Success; m = m.NextMatch())
                    {
                        string xmlNode = GetNodeValue(m, nodeMode, keyWord);
                        xmlNods.Append(xmlNode);
                    }
                }
            }

            return xmlNods.ToString();
        }

        /// <summary>
        /// ����URL���ҳ������
        /// </summary>
        /// <param name="srcUrl"></param>
        /// <returns></returns>
        public static string GetPageContent(string srcUrl)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(srcUrl);
            myRequest.Method = "GET";
            myRequest.ContentType = "application/x-www-form-urlencoded";

            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            string content = reader.ReadToEnd();
            return content;
        }       


        /// <summary>
        /// IGetNode��ڣ���������ƥ���� ���� xml�ڵ�Ԫ��
        /// </summary>
        /// <param name="m"></param>
        /// <param name="nodeMode"></param>
        /// <returns></returns>
        public static string GetNodeValue(Match m, XMLNodeMode nodeMode, string keyWord)
        {
            switch (nodeMode)
            {
                case XMLNodeMode.BaiduNews:
                    return GetBaiduNewsNode(m,keyWord);
                case XMLNodeMode.SogouNews:
                    return GetSogouNewsNode(m);
                case XMLNodeMode.TianyaGoogle:
                    return GetTianyaGoogleNode(m);
                case XMLNodeMode.Baidu:
                    return GetBaiduNode(m, keyWord);
                default:
                    throw new Exception("GetNodeValue(XMLNodeMode) δ����");
            }
        }

        
        #region IGetNodeʵ��
        /// <summary>
        /// IGetNode����ʵ�֡��ٶ�news��ӳ���ϵʵ��
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string GetBaiduNewsNode(Match m, string keyWord)
        {
            StringBuilder xmlNode = new StringBuilder();

            xmlNode.Append("<doc>");
            //http://money.msn.com.cn/finance/internal/87363.shtml
            xmlNode.Append("<doc_url><![CDATA[").Append(m.Groups[2].Value).Append("]]></doc_url>");

            xmlNode.Append("<doc_myKeyword><![CDATA[").Append(keyWord).Append("]]></doc_myKeyword>");

            //xml.Append("<doc_mySrcType><![CDATA[news/blog/bbs/web]]></doc_mySrcType>");
            xmlNode.Append("<doc_mySrcType><![CDATA[").Append(GetURLType(m.Groups[2].Value)).Append("]]></doc_mySrcType>");

            //��<font color=#cc0033>��Ǩ</font>�����˵Ľ���
            xmlNode.Append("<doc_title><![CDATA[").Append(NoHTML(m.Groups[3].Value)).Append("]]></doc_title>");

            //������
            xmlNode.Append("<doc_MySiteName><![CDATA[").Append(NoHTML(m.Groups[4].Value)).Append("]]></doc_MySiteName>");

            //2010-01-13
            xmlNode.Append("<doc_MyDate><![CDATA[").Append(NoHTML(m.Groups[5].Value)).Append("]]></doc_MyDate>");

            //�������е�<font color=#cc0033>��Ǩ</font>ģʽ,�������Ÿ���������.........
            xmlNode.Append("<doc_content><![CDATA[").Append(NoHTML(m.Groups[6].Value)).Append("]]></doc_content>");

            xmlNode.Append("</doc>");

            return xmlNode.ToString();
        }

        /// <summary>
        /// IGetNode����ʵ�֡��ٶ�������ӳ���ϵʵ��
        /// </summary>
        /// <param name="m"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public static string GetBaiduNode(Match m, string keyWord)
        {
            StringBuilder xmlNode = new StringBuilder();
            xmlNode.Append("<doc>");
            xmlNode.Append("<doc_url><![CDATA[").Append(m.Groups[1].Value).Append("]]></doc_url>");
            xmlNode.Append("<doc_myKeyword><![CDATA[").Append(keyWord).Append("]]></doc_myKeyword>");
            xmlNode.Append("<doc_title><![CDATA[").Append(NoHTML(m.Groups[2].Value)).Append("]]></doc_title>");
            xmlNode.Append("<doc_MyDate><![CDATA[").Append(m.Groups[7].Value).Append("]]></doc_MyDate>");
            xmlNode.Append("<doc_content><![CDATA[").Append(NoHTML(m.Groups[3].Value)).Append("]]></doc_content>");
            xmlNode.Append("</doc>");

            return xmlNode.ToString();
        }

        private static string GetSogouNewsNode(Match m)
        {
            StringBuilder xmlNode = new StringBuilder();

            xmlNode.Append("<doc>");

            xmlNode.Append("<doc_url><![CDATA[").Append(m.Groups[1].Value).Append("]]></doc_url>");
            xmlNode.Append("<doc_mySrcType><![CDATA[").Append(GetURLType(m.Groups[1].Value)).Append("]]></doc_mySrcType>");

            //��<font color=#cc0033>��Ǩ</font>�����˵Ľ���
            xmlNode.Append("<doc_title><![CDATA[").Append(NoHTML(m.Groups[2].Value)).Append("]]></doc_title>");

            //MSN�й�
            xmlNode.Append("<doc_MySiteName><![CDATA[").Append(NoHTML(m.Groups[3].Value)).Append("]]></doc_MySiteName>");
            //2010-01-13 15:22
            xmlNode.Append("<doc_MyDate><![CDATA[").Append(NoHTML(m.Groups[4].Value)).Append("]]></doc_MyDate>");

            //�������е�<font color=#cc0033>��Ǩ</font>ģʽ,�������Ÿ���������.........
            xmlNode.Append("<doc_content><![CDATA[").Append(NoHTML(m.Groups[5].Value)).Append("]]></doc_content>");

            xmlNode.Append("</doc>");

            return xmlNode.ToString();
        }

        public static string GetTianyaGoogleNode(Match m)
        {
            StringBuilder xmlNode = new StringBuilder();

            xmlNode.Append("<doc>");

            xmlNode.Append("<doc_url><![CDATA[").Append(m.Groups[1].Value).Append("]]></doc_url>");
            xmlNode.Append("<doc_title><![CDATA[").Append(NoHTML(m.Groups[2].Value)).Append("]]></doc_title>");
            xmlNode.Append("<doc_MySiteName><![CDATA[������̳]]></doc_MySiteName>");
            xmlNode.Append("<doc_content><![CDATA[").Append(NoHTML(m.Groups[3].Value)).Append("]]></doc_content>");

            xmlNode.Append("</doc>");

            return xmlNode.ToString();

        }
        # endregion

        # region �ٶ����� URL ����
        /// <summary>
        ///DEMO����ҳ����ʱ�����ݽ����URL�ж��� ���ţ����ͣ�BBS ����һ����ҳ
        ///�ض��Ĳ�����������̳����ʱ��ֱ��ָ���������
        ///�����汾��������ʽʵ��
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetURLType(string url)
        {
            //
            string lowerURL = url.ToLower();
            if (lowerURL.Contains("blog"))
                return "blog";
            if (lowerURL.Contains("bbs"))
                return "bbs";
            if (lowerURL.Contains("new"))
                return "news";
            return "web";
        }
        # endregion

        public static string NoHTML(string Htmlstring)
        {
            //ɾ��HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
    }


}
