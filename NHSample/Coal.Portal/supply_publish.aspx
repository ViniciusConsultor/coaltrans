<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supply_publish.aspx.cs" Inherits="supply_publish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>����������Ϣ</title>
<link href="css/admin_style.css" type="text/css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/menu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="h_wrapper">
	<div id="h_header">
		<h1 class="logo"><a href="/" title="����ú̿��ҵ��">����ú̿��ҵ��</a></h1>
		<div class="h_topNav">
			<div class="h_r1"></div>
			<div class="h_navList"><a href="#">��վ��ҳ</a> | <a href="#">��Ҫ�ɹ�</a> | <a href="#">��Ҫ����</a> | <a href="#">��Ѷ</a> | <a href="#">��̳</a></div>
			<div class="h_r1"></div>
		</div>
		<div class="h_mainMenu clearfix">
			<ul class="h_mainNav">
				<li class="current"><a href="uc_index.aspx">ϵͳ��ҳ</a></li>
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
			<dl class="h_tips">
				<dt>��Ҫ���ѣ�</dt>
				<dd>4��28���𣬰��������й�Ӧ��Ϣ�ķ������޸Ľ�ȫ��������һ�ڼ���Ϣ��������Ϊ�ɽ�����Ϣ����ʲô�ǿɽ�����Ϣ����</dd>
				<dd>���Ž���½���Ƴ��������ḻ�����ʺ���С��ҵ�Ľ���ƽ̨�ķ�չ��Ҫ������ǰ�ڵĿͻ����У�4��24����ȥ��һ�ڼ��������ȵĹ���һ�ڼ���Ϣ�Լ�������Ŀɽ�����Ϣ����ͨ��Ϣһ�����򡣽��������Ը������Ĳ�Ʒѡ���ʺϵ���Ϣ�������͡��ش˸�֪����л��������֧�֣�</dd>
			</dl>
			<div class="h_columns clearfix">
				<div class="h_column h_colW2">
					<div class="h_mainTitle">
						<h3>��ѡ��������Ϣ��������</h3>
					</div>
					<div class="h_items clearfix">
						<div class="h_itemsList">
							<img src="images/h_pic.jpg" alt="" />
							<p>�����ʽ���Ŀ����������ר��������ת�á�<br />�磺��Ŀ��Ͷ�ʡ���Ͷ��ȡ�</p>
							<span><a href="coal_supply_publish.aspx">ú̿��Ӧ��Ϣ����</a></span>
						</div>
						<div class="h_itemsList">
							<img src="images/h_pic.jpg" alt="" />
							<p>����ú��Ʒ��Ӧ��Ϣ������<br />�磺��̿�ȡ�</p>
							<span><a href="coalproduct_supply_publish.aspx">ú��Ʒ��Ӧ��Ϣ����</a></span>
						</div>
						<div class="h_itemsList">
							<img src="images/h_pic.jpg" alt="" />
							<p>�����ʽ���Ŀ����������ר��������ת�á�<br />�磺��Ŀ��Ͷ�ʡ���Ͷ��ȡ�</p>
							<span><a href="#">�����ʽ���Ŀ����</a></span>
						</div>
						<div class="h_itemsList">
							<img src="images/h_pic.jpg" alt="" />
							<p>�����ʽ���Ŀ����������ר��������ת�á�<br />�磺��Ŀ��Ͷ�ʡ���Ͷ��ȡ�</p>
							<span><a href="#">�����ʽ���Ŀ����</a></span>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<input id="current_menu" type="hidden" value="menu_5" />
<input id="parent_menu" type="hidden" value="menu_4" />
<p id="h_footer">Copyright &copy; 2009 ����ú̿��ҵ�� ���죺�й�ú̿��ҵЭ�� ����֧�֣�������ú��ͨ�Ƽ����޹�˾</p>
</form>
</body>
</html>
