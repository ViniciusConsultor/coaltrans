<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg_step1.aspx.cs" Inherits="reg_step1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�û�ע��</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/jquery.validate.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            var validator = $("#form1").validate({
                rules: {
                    // simple rule, converted to {required:true}
                    tbxEmail: {
                        required: true,
                        email: true
                    },
                    tbxNickName: "required",
                    tbxPassword: {
                        required: true
                    },
                    tbxConfirmPassword: {
                        required: true,
                        equalTo: "#tbxPassword"
                    },
                    tbxValidCode: {
                        required: true,
                        remote: {
                            url: "Handler/ValidateCode.ashx",
                            type: "post",
                            data: {
                                code: function() {
                                    return $("#tbxValidCode").val();
                                }
                            }
                        }
                    }
                },
                messages: {
                    tbxEmail: {
                        required: "Email����Ϊ��",
                        email: "����д��Ч��Email"
                    },
                    tbxNickName: "�ǳƲ���Ϊ��",
                    tbxPassword: {
                        required: "���벻��Ϊ��"
                    },
                    tbxConfirmPassword: {
                        required: "��ȷ������",
                        equalTo: "���벻һ��"
                    },
                    tbxValidCode: {
                        required: "��¼����֤��",
                        remote: "��֤�����"
                    }
                },
                errorPlacement: function(error, element) {
                    var error_container = element.parent("td").next("td").children("div");
                    error.appendTo(error_container.children("p").empty());
                    error_container.removeClass().addClass("zhushi02");
                },
                success: function(label) {
                    label.parent("p").parent("div").removeClass().addClass("zhushi03");
                    label.parent("p").empty().text("��֤ͨ��");
                },
                onkeyup: true
            });

            var prompt = { tbxEmail: "����д��Ч��Email",
                tbxPassword: "������6-20��Ӣ����ĸ�����������",
                tbxConfirmPassword:"���ٴ��������룬��ȷ��",
                tbxNickName: "�ǳ���6-10��Ӣ����ĸ�����������"
            };

            $("input").focus(function() {
                var id = $(this).attr("id");
                var msg_container = $(this).parent("td").next("td").children("div");
                msg_container.children("p").empty().html(prompt[id]);
                msg_container.removeClass().addClass("zhushi01");

                if (id == "tbxValidCode") {
                    $(this).attr("value", "");
                    $("#validimg").attr("src", "Handler/ValidCodeGenerator.ashx?" + new Date).click(function() {
                        $(this).attr("src", "Handler/ValidCodeGenerator.ashx?" + new Date);
                    });
                }
            });

            $("input").blur(function() {
                var id = $(this).attr("id");
                validator.element("#" + id);
                if ($(this).attr("id") == "tbxValidCode") {
                    $(this).unbind("focus");
                }
            });
        });
    </script>
</head>
<body>
<form id="form1" name="form1" runat="server">
<div class="userht_all">
  <div class="topban"></div>
  <div class="h_menu">��������:010-88888888</div>
  <div class="login_bz">
    <ul>
      <li class="bz_a">��дע����Ϣ</li>
    </ul>
  </div>
  <div class="clear"></div>
  <div class="login_xx">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th width="200">Email��ַ��</th>
    <td width="220"><asp:TextBox ID="tbxEmail" TextMode="SingleLine" name="tbxEmail" runat="server"></asp:TextBox></td>
    <td width="480"><div><p></p></div></td>
  </tr>
  <tr>
    <th>�����ǳƣ�</th>
    <td><asp:TextBox ID="tbxNickName" TextMode="SingleLine" name="tbxNickName" runat="server"></asp:TextBox></td>
    <td><div><p></p></div></td>
  </tr>
  <tr>
    <th>�趨���룺</th>
    <td><asp:TextBox ID="tbxPassword" TextMode="Password" name="tbxPassword" runat="server"></asp:TextBox></td>
    <td><div><p></p></div></td>
  </tr>
  <tr>
    <th>����һ�����룺</th>
    <td><asp:TextBox ID="tbxConfirmPassword" TextMode="Password" name="tbxConfirmPassword" runat="server"></asp:TextBox></td>
    <td><div><p></p></div></td>
  </tr>
  <tr>
    <th>��֤�룺</th>
    <td><asp:TextBox id="tbxValidCode" name="tbxValidCode" CssClass="in_no1" TextMode="SingleLine" runat="server" Text="��ȡ��֤��" ></asp:TextBox>
      <img id="validimg" width="80" height="20" style="vertical-align:text-bottom" /></td>
     <td><div><p></p></div></td>
  </tr>
  <tr>
    <th>&nbsp;</th>
    <td colspan="2"><input name="" type="checkbox" value="" />�����Ķ���ͬ�⡶�������</td>
    </tr>
  <tr>
    <th>&nbsp;</th>
    <td><asp:ImageButton ID="Submit" ImageUrl="images/but0.jpg" width="184" height="35" 
            border="0" runat="server" onclick="Submit_Click" /></td>
    <td>&nbsp;</td>
  </tr>
</table>

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

