<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�û���¼</title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="login_all">
<div class="logo_l"></div>
<div class="loginuser">
<h1>�Ѿ��ǻ�Ա<em>�������¼�������룬������¼�����ɡ�</em></h1>
        <ul>
          <table border="0" cellspacing="0" cellpadding="0" class="login_tb">
            <asp:Label runat="server" ID="errorMsg" Visible="false">�˺ź����벻ƥ�䣬�����룬����һ��</asp:Label>
            <tr>
              <td>��ԱEmail��</td>
              <td><asp:TextBox ID="email" runat="server"></asp:TextBox></td>
              <td><a href="#">���ǻ�Ա��¼Email��</a></td>
            </tr>
            <tr>
              <td>��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�룺</td>
              <td><asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox></td>
              <td><a href="#">�������룿</a></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td><asp:Button ID="submit" runat="server" Text="��¼" onclick="submit_Click"/></td>
              <td>&nbsp;</td>
            </tr>
          </table>
		  </ul>
</div>
<div class="logo_r"></div>
<div class="clear"></div>
<div class="zc_user">
<h1>�����ǻ�Ա</h1>
        <h2><a href="#"><img src="images/but.jpg" border="0" /></a></h2>
        <h3>���ע���������</h3>
        <ul>
          <li>������Ϣ���ƹ��Ʒ��������˾</li>
          <li>����������Ϣ��������κ��̻�</li>
          <li>�鿴�������ԣ���ͻ���ʱ����</li>
        </ul>
</div>
<div class="d_menu">Copyright &copy; 2009 ����ú̿��ҵ��</div>
  <div class="endpage">
    <p>����:�й�ú̿��ҵЭ�� ����֧��:������ú��ͨ�Ƽ����޹�˾  </p>
    <p>(�����վ��ҳ,���齫������ʾ���ֱ��ʵ���Ϊ:1024*768)</p>
  </div>
</div>
</form>
</body>
</html>
