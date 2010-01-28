<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Invite_publish.aspx.cs" Inherits="InfoPublish_Invite_publish" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�����б���Ϣ</title>
<link href="css/admin_style.css" type="text/css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/uc.js"></script>
<script type="text/javascript" src="js/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="js/jquery.validate.js"></script>
<script type="text/javascript">
$(document).ready(function(){
    //���ص�һ�������б�
    $.post("Handler/RegionHandler.ashx", { parent_id: 9000 }, function(data, status) {
        $("#selRegion").append("<option value='-1' >---��ѡ��ʡ��---</option>");
        
        for (var i = 0; i < data.regions.length; i++) {
            $("#selRegion").append('<option value="' + data.regions[i].id + '">' + data.regions[i].name + '</option>');
        }
    }, "json");   
    
    function check()
    {
        var Flag=true;  
        $("input[type='text']").each(function(){   
            $(this).next().html("");         
            var val=$.trim($(this).val());
            if(val=="")
            {                                
                $(this).next().html("�����Ϊ�գ�");
                Flag=false;
            }
        }); 
        if($("#selRegion").val()=="-1")
        {
            $("#selRegion").next().html("");
            $("#selRegion").next().html("��ѡ��ʡ�ݣ�");
            Flag=false;
        }
        else
        {
            $("#selRegion").next().html("");
        }
        var oEditor = FCKeditorAPI.GetInstance('txtDetails');//content��fckʵ��������,Ҳ�Ǳ��ı�������� 
        oEditor.UpdateLinkedField();//������ݸ��£������ⲽ�����Ļ�������Ҫ��ڶ��β��ܵõ����ݽ��         
        var content=$.trim(oEditor.GetXHTML(true));
        if(content=="")
        {     
            $("#yztxtDetails").html("��鲻��Ϊ�գ�");
            Flag=false;
        }
        else
        {
            $("#yztxtDetails").html("");
        }
        return Flag;
    }
    
    $("#BtnSubmit").click(function(){
      if(check())
      {
        var oEditor = FCKeditorAPI.GetInstance('txtDetails');//content��fckʵ��������,Ҳ�Ǳ��ı�������� 
        oEditor.UpdateLinkedField();//������ݸ��£������ⲽ�����Ļ�������Ҫ��ڶ��β��ܵõ����ݽ�� 
        
        var content=$.trim(oEditor.GetXHTML(true));        
        var RequestStr="({'txtDetails':'"+escape(content)+"',";
        $("input[type='text']").each(function(){
            var name=$(this).attr("name");
            var val=$(this).val();
            RequestStr+="'"+name+"':'"+val+"',";
        });
        $("select").each(function(){
            var name=$(this).attr("name");
            var val=$(this).val();
            RequestStr+="'"+name+"':'"+val+"',";
        });
        $("input[type='file']").each(function(){
            var name=$(this).attr("name");
            var val=$(this).val();
            RequestStr+="'"+name+"':'"+escape(val)+"',";
        });
        RequestStr+="'action':'InviteInfo'})";
       RequestStr=eval(RequestStr);        
        $.ajax({
           type: "POST",
           url: "Handler/InfoManage.ashx",
           data: RequestStr,
           dataType: "json",
           success: function(data) {
                if (data.statusCode == 1) {
                   $("#Msg").html("��ϲ�����ύ�ɹ���");
                }
                else {
                   $("#Msg").html("�Բ����ύʧ�ܣ���������˶�������Ϣ��");
                }
            }
        });
      }  
    });
});
</script>
<style type="text/css">
    .style2
    {
        width: 75px;
    }
    .style3
    {
        width: 144px;
    }
    .style4
    {
        width: 80px;
    }
    .style5
    {
        width: 86px;
    }
    .style6
    {
        width: 154px;
    }
</style>
</head>
<body>
   <form id="form1" runat="server">
   <div id="h_wrapper">
    <!--#include File="uc_top.inc"-->
	<div id="h_content" class="clearfix">
		<div id="nav" class="h_sideBar">
			<div id="nav_tree" class="h_tree"></div>
		</div>
		<div class="h_main">
			<dl class="h_tips">
				<dt>Ϊ��������ܸ���ȷ�ҵ����Ĳ�Ʒ�������������¼������������Ϣ���ȣ���ø��õ�������</dt>
				<dd>1��һ����Ϣֻ����һ����Ʒ��</dd>
				<dd>2�����Ĳ�Ʒ������س����ڱ����У�</dd>
				<dd>3������������д��Ӧ��Ϣ</dd>
			</dl>
			<div class="h_columns clearfix">
				<div class="h_column h_colW2">
					<div class="h_mainTitle">
						<ul class="h_itemsMenu" id="tabMenu">
							<li class="active"><a href="javascript:void(0);">ú̿�б�</a></li>
						</ul>
					</div>
					<div id="tabMenu_Content0">
						<div class="h_itemsBody h_item_bb">
							<table cellpadding="0" cellspacing="0" border="0">							   
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>���⣺</td>
							        <td align="left" colspan="3"><input type="text" size="50" id="txtInviteTitle" name="txtInviteTitle" /><span style="color:Red;"></span></td>							        
							   </tr> 
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>������</td>
							        <td align="left" colspan="3">
							        <select id="selRegion" name="selRegion"></select>
							        <span style="color:Red;"></span>
							        </td>							        
							   </tr> 
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>��ʼʱ�䣺</td>
							        <td align="left" style="width:300px;">
							            <input type="text" id="txtStartTime" name="txtStartTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"  />
							            <span style="color:Red;"></span>
							        </td>
							        <td style="width:90px; text-align:right;"><span>*</span>����ʱ�䣺</td>
							        <td align="left">
							            <input type="text" id="txtEndTime" name="txtEndTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"  />
							            <span style="color:Red;"></span>
							        </td>
							   </tr>
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>��ϸ��Ϣ��</td>
							        <td align="left" colspan="3">	
                                        <FCKeditorV2:FCKeditor ID="txtDetails" runat="server" Height="400">
                                        </FCKeditorV2:FCKeditor>					            
                                        <span style="color:Red;" id="yztxtDetails"></span>							            
							        </td>
							        </tr>
							   <tr>
							        <td style="width:90px;text-align:right;">�����ϴ���</td>
							        <td align="left" colspan="3">
							        <input type="file" id="txtAdjunctUrl" name="txtAdjunctUrl" /></td>							        
							   </tr> 
							</table>
						</div>						
						<div class="h_itemsBody h_item_bb">
						    <table cellpadding="0" cellspacing="0" border="0">
							       <tr>
							            <td style="width:300px; text-align:right;" >
							            <input type="button" class="h_buttun1"  value="����" id="BtnSubmit" name="BtnSubmit" /></td>
							            <td style="width:50px;"></td>
							            <td align="left"><input type="reset" class="h_buttun1"  value="����" /></td>
							       </tr>
							       <tr>
							            <td style="text-align:center;" >
							            <div id="Msg" style="color:Red; font-size:14px; font-weight:bolder;"></div>
							            </td>							            
							       </tr>
							    </table>
						    </div>
						</div>
						<div style="height:20px; width:100%;">
						    
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<input id="current_menu" type="hidden" value="menu_10" />
<input id="parent_menu" type="hidden" value="menu_4" />
<p id="h_footer">Copyright &copy; 2009 ����ú̿��ҵ�� ���죺�й�ú̿��ҵЭ�� ����֧�֣�������ú��ͨ�Ƽ����޹�˾</p>
    </form>
</body>
</html>
