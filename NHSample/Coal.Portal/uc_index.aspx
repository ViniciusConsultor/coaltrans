<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uc_index.aspx.cs" Inherits="uc_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>��ҳ</title>
<link href="css/admin_style.css" type="text/css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/menu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="h_wrapper">
	<div id="h_header">
		<h1 class="logo"><a href="index.html" title="����ú̿��ҵ��">����ú̿��ҵ��</a></h1>
		<div class="h_topNav">
			<div class="h_r1"></div>
			<div class="h_navList"><a href="#">��վ��ҳ</a> | <a href="#">��Ҫ�ɹ�</a> | <a href="#">��Ҫ����</a> | <a href="#">��Ѷ</a> | <a href="#">��̳</a></div>
			<div class="h_r1"></div>
		</div>
		<div class="h_mainMenu clearfix">
			<ul class="h_mainNav">
				<li class="current"><a href="#">ϵͳ��ҳ</a></li>
			</ul>
			<div class="h_userInfo"><span>��ӭ DONGKUO ���� ��¼ϵͳ [<a href="#">�˳�ϵͳ</a>]</span></div>
			<div class="h_userExplain"><a href="#">��Ŀǰ����ͨ��Ա���������Ϊ����ͨ��Ա</a></div>
		</div>
	</div>
	<div id="h_content" class="clearfix">
		<div id="nav" class="h_sideBar">
			<div id="nav_tree" class="h_tree"></div>
		</div>
		<div class="h_main">
			<div class="h_notice">
				<div class="h_mainTitle">
					<h3>��Ҫ��������</h3>
				</div>
				<p class="h_check">�����˺Ż�δͨ����֤���޷��յ��ͻ����κη��� <input name="check" type="button" value="�����֤" class="h_buttun1" /></p>
				<dl class="clearfix">
					<dt class="dtm"><span>�̻����</span></dt>
					<dd class="ddw">����δ�����κ��̻���ݣ��޷���һʱ�����������̻� <a href="#">������Ѷ���</a> �����ڽ���qq��tom��������̻�����ʼ���̫�ȶ���Ϊ��֤��Ϣ��ʱ���գ���ʹ�ö�������Ϊtom.com��qq.com���̻�����û���"�����̻����"�����������䡣</dd>
				</dl>
				<dl class="clearfix">
					<dt><span>��ҵ��Ծ����</span></dt>	
					<dd>�����ܼ�û�е�¼��վ��Ҳû�е�¼ó��ͨ�����Ļ�Ծ��ֵΪ0 <a href="#">�鿴����</a></dd>
				</dl>
				<dl class="clearfix nobd">
					<dt><span>�������</span></dt>
					<dd>�����������������Ⱥ���Ƴ��޵�Ѻ��Ϣ���ٴ����������Ҫ�κη��ã� <a href="#">��������</a></dd>
				</dl>
			</div>
			<div class="h_columns clearfix">
				<div class="h_column h_colW1">
					<div class="h_mainTitle">
						<h3>���¹���</h3>
					</div>
					<ul class="h_columnList">
						<li>�̻���ı��<a href="#">������Ч��������Ӫ��Ч��</a></li>
						<li>����ͨ��<a href="#">��������Ǳ�ڿͻ����������ͻ���Ϣ</a></li>
						<li>�ƶ������ͨ��<a href="#">������ʱ��ز�����κ��̻������̱�����</a></li>
						<li>���⾭��<a href="#">�����������������������ⱦ��</a></li>
					</ul>
				</div>
				<div class="h_column h_colW1">
					<div class="h_mainTitle">
						<h3>���»</h3>
					</div>
					<ul class="h_columnList">
						<li>&middot;<a href="#">���Ͽ���100�죬��Ҫ׬100�������Ͽ���100�죬��Ҫ׬100�� </a></li>
						<li>&middot;<a href="#">���Ͽ���100�죬��Ҫ׬100�������Ͽ���100�죬��Ҫ׬100�� </a></li>
						<li>&middot;<a href="#">���Ͽ���100�죬��Ҫ׬100�������Ͽ���100�죬��Ҫ׬100�� </a></li>
						<li>&middot;<a href="#">���Ͽ���100�죬��Ҫ׬100�������Ͽ���100�죬��Ҫ׬100�� </a></li>
					</ul>
				</div>
				<div class="h_column h_colW2">
					<div class="h_mainTitle">
						<h3>����������ͨ��Ա���޷����ܰ���Ͱ���Ȩ���񣬽���������Ϊ����ͨ��</h3>
					</div>
					<p>����ͨ��Ա�ǰ���Ͱ͵�һ�ֻ�Ա��ݣ�ӵ���Ĵ���Ȩ��������ֵ���񣬼�������վ�����ƹ㡢�ŻݵĻ������Ȩ������ϸ���������� <a href="#">����˽�</a>��</p>
				</div>
			</div>
		</div>
	</div>
</div>
<p id="h_footer">Copyright &copy; 2009 ����ú̿��ҵ�� ���죺�й�ú̿��ҵЭ�� ����֧�֣�������ú��ͨ�Ƽ����޹�˾</p>
</form>
</body>
</html>
