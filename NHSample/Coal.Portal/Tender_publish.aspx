<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tender_publish.aspx.cs" Inherits="InfoPublish_Tender_publish" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>����Ͷ����Ϣ</title>
<link href="css/admin_style.css" type="text/css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/uc.js"></script>
<script type="text/javascript" src="js/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="js/jquery.validate.js"></script>
<script type="text/javascript">
$(document).ready(function(){
    //���ص�һ�������б�
    $.post("Handler/RegionHandler.ashx", { parent_id: 9000 }, function(data, status) {
        $("#selProvince").append("<option value='-1' >---��ѡ��ʡ��---</option>");
        $("#selCity").append("<option value='-1' >---��ѡ�����---</option>");
        for (var i = 0; i < data.regions.length; i++) {
            $("#selProvince").append('<option value="' + data.regions[i].id + '">' + data.regions[i].name + '</option>');
        }
    }, "json");
    
    $("#selProvince").change(function() {
        var provinceId = $(this).val();
        if (provinceId > -1) {
            $.post("Handler/RegionHandler.ashx", { parent_id: provinceId }, function(data, status) {
                $("#selCity").html("<option value='-1'>---��ѡ�����---</option>");
                for (var i = 0; i < data.regions.length; i++) {
                    $("#selCity").append('<option value="' + data.regions[i].id + '">' + data.regions[i].name + '</option>');
                }
            }, "json");
        }
        else if (provinceId == -1) {
            $("#selCity").html("");
            $("#selCity").append("<option value='-1'>---��ѡ�����---</option>");
        }
    });
    
    $("#selIsTransport").change(function(){
        if($(this).val()=="1")
        {
            $("#Price").show();
        }
        else
        {
            $("#Price").hide();
        }
    });
    
    function check()
    {
        var Flag=true;
        if($("#txtDemandTitle").val()=="")
        {
            $("#txtDemandTitle").next().html("���ⲻ��Ϊ�գ�");
            Flag=false;
        }
        else
        {
            $("#txtDemandTitle").next().html("");
        }
        if($("#selCoalType").val()=="0")  
        {  
            $("#selCoalType").next().html("��ѡ��ú�֣�");
            Flag=false;
        }
        else
        {
            $("#selCoalType").next().html("");
        }
        if($("#selGranularity").val()=="0")
        {
            $("#selGranularity").next().html("��ѡ��ú̿���ȷ�Χ��");
            Flag=false;
        } 
        else
        {
            $("#selGranularity").next().html("");
        } 
        if($("#txtDemandQuantity").val()=="")
        {
            $("#txtDemandQuantity").next().html("����������Ϊ�գ�");
            Flag=false;
        }
        else
        {
            $("#txtDemandQuantity").next().html("");
        }
        if(($("#selProvince").val()=="-1")&& ($("#selCity").val()=="-1"))
        {
            $("#selCity").next().html("����ѡ��������ϸ��ַ��");
            Flag=false;
        }
        else
        {
            $("#selCity").next().html("");
        }
        if($("#txtInfoIndate").val()=="")
        {
            $("#txtInfoIndate").next().html("������д��Ϣ����Ч���ޣ�");
            Flag=false;
        }
        else
        {
            $("#txtInfoIndate").next().html("");
        }
        if($("#selCalorificPower").val()=="0")
        {
            $("#selCalorificPower").next().html("����ѡ��������");
            Flag=false;
        }  
        else
        {
            $("#selCalorificPower").next().html("");
        }    
        return Flag;
    }
    
    $("#BtnSubmit").click(function(){
      if(check())
      {
        var RequestStr="({";
        $("input[type='text']").each(function(){
            var name=$(this).attr("name");
            var val=$(this).val();
            RequestStr+="'"+name+"':'"+val+"',";
        });
        $("select").each(function(){
            var name=$(this).attr("name");
            var val=$(this).children("option:selected").val();
            RequestStr+="'"+name+"':'"+val+"',";
        });
        $("textarea").each(function(){
            var name=$(this).attr("name");
            var val=$(this).val();
            RequestStr+="'"+name+"':'"+val+"',";
        });
        RequestStr+="'action':'1'})";
        RequestStr=eval(RequestStr);
        $.ajax({
           type: "POST",
           url: "coal_demand_publish.aspx",
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
							<li class="active"><a href="javascript:void(0);">ú̿Ͷ��</a></li>
						</ul>
					</div>
					<div id="tabMenu_Content0">
						<div class="h_itemsBody h_item_bb">
							<table cellpadding="0" cellspacing="0" border="0">
							   <tr>
							        <td colspan="4"><span style="color:Red;font-size:14px; font-weight:bolder;">������Ϣ��</span></td>
							   </tr>
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>���⣺</td>
							        <td align="left" colspan="3"><input type="text" size="50" id="txtDemandTitle" name="txtDemandTitle" /><span style="color:Red;"></span></td>							        
							   </tr> 
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>ú�֣�</td>
							        <td align="left">
							            <select id="selCoalType" name="selCoalType">
							                <option value="0">--��ѡ��ú��--</option>
							                <option value="����ú">����ú</option>
										    <option value="����ú">����ú</option>
										    <option value="�紵ú">�紵ú</option>
										    <option value="����ú">����ú</option>
										    <option value="ϴ��ú">ϴ��ú</option>
										    <option value="������">������</option>
							            </select>
							            <span style="color:Red;"></span>
							        </td>
							        <td style="width:90px; text-align:right;"><span>*</span>���ȣ�</td>
							        <td align="left">
							            <select id="selGranularity" name="selGranularity">
							                <option value="0">---��ѡ�����ȷ�Χ---</option>
							                <option value="20~50����">20~50����</option>
							            </select>
							            <span style="color:Red;"></span>
							        </td>
							   </tr>
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>��������</td>
							        <td align="left">
							            <input type="text" id="txtDemandQuantity" name="txtDemandQuantity" />���֣�<span style="color:Red;"></span>
							        </td>
							        <td style="width:90px; text-align:right;"><span>*</span>�����أ�</td>
							        <td align="left">
							            <select id="selProvince" name="selProvince"></select>-
							            <select id="selCity" name="selCity"></select><span style="color:Red;"></span>
							        </td>
							   </tr>
							   <tr>
							        <td style="width:90px;text-align:right;"><span>*</span>��Ϣ��Ч�ڣ�</td>
							        <td align="left" colspan="3"><input type="text" id="txtInfoIndate" name="txtInfoIndate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"  /><span style="color:Red;"></span></td>							        
							   </tr> 
							</table>
						</div>
						<div class="h_itemsBody h_item_bb">
							<table cellpadding="0" cellspacing="0" border="0">
							   <tr>
							        <td colspan="6"><span style="color:Red;font-size:14px; font-weight:bolder;">���ָ�꣺</span></td>
							   </tr>
							    
							   <tr>
							        <td style="text-align:right;" class="style2"><span>*</span>��������</td>
							        <td align="left" class="style3">
							            <select id="selCalorificPower" name="selCalorificPower">
							                <option value="0">---��ѡ��Χ---</option>
							                <option value="5000��">5000��</option>
							            </select>
							            <span style="color:Red;"></span>
							        </td>
							        <td style="text-align:right;" class="style4">�ӷ��ݣ�</td>
							        <td align="left" class="style6">
							            <input type="text" size="5" id="txtVolatilize" name="txtVolatilize" />%
							        </td>
							        <td style="text-align:right;" class="style5">�ҷ֣�</td>
							        <td align="left">
							            <input type="text" size="5" id="txtAsh" name="txtAsh" />%
							        </td>
							   </tr>
							   <tr>
							        <td style="text-align:right;" class="style2">��ݣ�</td>
							        <td align="left" class="style3">
							            <input type="text" size="5" id="txtSulphur" name="txtSulphur" />%
							        </td>
							        <td style="text-align:right;" class="style4">ˮ�֣�</td>
							        <td align="left" class="style6">
							             <input type="text" size="5" id="txtWater" name="txtWater"/>%
							        </td>
							        <td style="text-align:right;" class="style5">���ȶ��ԣ�</td>
							        <td align="left" style="width:100px;">
							            <input type="text" size="5" id="txtHotStability" name="txtHotStability"/>
							        </td>
							   </tr>
							   <tr>
							        <td style="text-align:right;" class="style2">�������ԣ�</td>
							        <td align="left" class="style3">
							            <input type="text" size="5" id="txtAshFusing" name="txtAshFusing" />
							        </td>
							        <td style="text-align:right;" class="style4">��ĥ�ԣ�</td>
							        <td align="left" class="style6">
							             <input type="text" size="5" id="txtWearproof" name="txtWearproof"/>%
							        </td>
							        <td style="text-align:right;" class="style5">�̶�̼��</td>
							        <td align="left" style="width:100px;">
							            <select id="selCarbon" name="selCarbon">
							                <option value="0">---��ѡ��Χ---</option>
							                <option value="20%~50%">20%~50%</option>
							            </select>
							        </td>
							   </tr>
							   <tr>
							        <td style="text-align:right;" class="style2">��еǿ�ȣ�</td>
							        <td align="left" class="style3">
							            <input type="text" size="5" id="txtMaStrength" name="txtMaStrength" />%
							        </td>
							        <td style="text-align:right;" class="style4">ճ��ָ����</td>
							        <td align="left" class="style6">
							             <input type="text" size="5" id="txtBinderMark" name="txtBinderMark"/>%
							        </td>
							        <td colspan="2"></td>							        
							   </tr>							   
							</table>
						</div>
						<div class="h_itemsBody h_item_bb">
						    <table cellpadding="0" cellspacing="0" border="0">
							   <tr>
							        <td colspan="2"><span style="color:Red;font-size:14px; font-weight:bolder;">������Ϣ��</span></td>
							   </tr>
							    
							   <tr>
							        <td style="text-align:right; width:150px;">�Ƿ�Ҫ�����ṩ���䣺</td>
							        <td align="left">
							            <select id="selIsTransport" name="selIsTransport">
							                <option value="0">---��ѡ��---</option>
							                <option value="0">����Ҫ</option>
							                <option value="1">��Ҫ</option>
							            </select>
							        </td>
							   </tr>
							   <tr id="Price" style="display:none;">
							        <td style="text-align:right; width:150px;">�۸�Ҫ��</td>
							        <td align="left">
							            <input type="text" id="txtTransportPrice" name="txtTransportPrice" />
							        </td>
							   </tr>
							   <tr>
							        <td style="text-align:right; width:150px;">���㷽����</td>
							        <td align="left">
							            <textarea id="txtEstimateStyle" name="txtEstimateStyle" rows="5" cols="50"></textarea>
							        </td>
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
<input id="current_menu" type="hidden" value="menu_5" />
<input id="parent_menu" type="hidden" value="menu_4" />
<p id="h_footer">Copyright &copy; 2009 ����ú̿��ҵ�� ���죺�й�ú̿��ҵЭ�� ����֧�֣�������ú��ͨ�Ƽ����޹�˾</p>
    </form>
</body>
</html>
