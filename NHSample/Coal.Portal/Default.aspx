<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�ޱ����ĵ�</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/ui.core.js"></script>
<script type="text/javascript" src="js/ui.accordion.js"></script>
<script type="text/javascript" src="js/top.js"></script>
<script type="text/javascript">
    $(function() {
        $("#accordion").accordion();
    });
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="userht_all">
  <script type="text/javascript">
      render_top();
  </script>
  <div id="accordion" class="left_all">
    <h3 class="menu_b"><a href="#">��Ϣ��ҳ1</a></h3>
    <ul>
    <li class="menu_c"><a href="#">��Ϣ��ҳ</a></li>
    <li class="menu_c"><a href="#">��Ϣ��ҳ</a></li>
    <li class="menu_c"><a href="#">��Ϣ��ҳ</a></li>
    </ul>
	<h3 class="menu_b"><a href="#">��Ϣ��ҳ2</a></h3>
	<ul>
    <li class="menu_c"><a href="#">��Ϣ��ҳ2-1</a></li>
    <li class="menu_c"><a href="#">��Ϣ��ҳ2-2</a></li>
    <li class="menu_c"><a href="#">��Ϣ��ҳ2-3</a></li>
    </ul>
    <h3 class="menu_b"><a href="#">��Ϣ��ҳ3</a></h3>
	<ul>
    <li class="menu_c"><a href="#">��Ϣ��ҳ3-1</a></li>
    <li class="menu_c"><a href="#">��Ϣ��ҳ3-2</a></li>
    <li class="menu_c"><a href="#">��Ϣ��ҳ3-3</a></li>
    </ul>
  </div>
  <div class="right_all">
    <h1>��Ϣ��ҳ</h1>
    <div class="tishi"><img src="images/ico01.jpg" align="absmiddle" />�����ڻ�û�е�¼����¼�󼴿�ʹ�ð������֣�</div>
    <div class="loginuser">
      <div class="l_login">
        <h1>�Ѿ��ǻ�Ա<em>�������¼�������룬������¼�����ɡ�</em></h1>
        <ul>
          <table border="0" cellspacing="0" cellpadding="0" class="login_tb">
            <tr>
              <td>��Ա��¼����</td>
              <td><input type="text" name="textfield" /></td>
              <td><a href="#">���˵�¼����</a></td>
            </tr>
            <tr>
              <td>��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�룺</td>
              <td><input type="text" name="textfield2" /></td>
              <td><a href="#">�������룿</a></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td><input type="submit" name="Submit" value="��&nbsp;&nbsp;¼" /></td>
              <td>&nbsp;</td>
            </tr>
          </table>
        </ul>
      </div>
      <div class="r_login">
        <h1>�����ǻ�Ա</h1>
        <h2><a href="#"><img src="images/but.jpg" border="0" /></a></h2>
        <h3>���ע���������</h3>
        <ul>
          <li>������Ϣ���ƹ��Ʒ��������˾</li>
          <li>����������Ϣ��������κ��̻�</li>
          <li>�鿴�������ԣ���ͻ���ʱ����</li>
        </ul>
      </div>
    </div>
  </div>
  <script type="text/javascript">
      render_bottom();
  </script>
</div>
</form>
</body>
</html>

