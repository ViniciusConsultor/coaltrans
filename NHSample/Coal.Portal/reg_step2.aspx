<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg_step2.aspx.cs" Inherits="reg_step2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�ޱ����ĵ�</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form>
<div class="userht_all">
  <div class="topban"></div>
  <div class="h_menu">��������:010-88888888</div>
  <div class="login_bz">
    <ul>
      <li class="bz_b">1.��дע����Ϣ</li>
      <li class="bz_a">2.ͨ���ʼ�ȷ��</li>
      <li class="bz_b">3.ע��ɹ�</li>
    </ul>
  </div>
  <div class="clear"></div>
  <div class="login_email">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <th width="200">&nbsp;</th>
        <td width="292">���� <em><asp:Label ID="email" runat="server"></asp:Label></em> ������֤�ʼ�<br />
          ����ʼ��е����ӵ�ַ�����ɼ����˺ţ����ע�ᡣ </td>
        <!--<td width="408"><div class="goemail"><a href="#">ǰ��freemail.yeah.net����</a></div></td>-->
      </tr>
    </table>
  </div>
  <div class="email_txt">
    <h1>���2Сʱ����δ�յ������ʼ����볢�ԣ�</h1>
    <ul>
      <li>��������е������ʼ��������ʼ����ܱ�����Ϊ�����ʼ��� </li>
      <li>��ѯ�ͷ����ͷ��绰��010-88888888</li>
      <li><asp:Label ID="ValidTest" runat="server"></asp:Label></li>
    </ul>
  </div>
  <div class="clear"></div>
  <div class="d_menu">Copyright &copy; 2009 ����ú̿��ҵ��</div>
  <div class="endpage">
    <p>����:�й�ú̿��ҵЭ�� ����֧��:������ú��ͨ�Ƽ����޹�˾ </p>
    <p>(�����վ��ҳ,���齫������ʾ���ֱ��ʵ���Ϊ:1024*768)</p>
  </div>
</div>
</form>
</body>
</html>

