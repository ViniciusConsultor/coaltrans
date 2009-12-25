<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="coal_supply_publish.aspx.cs" Inherits="coal_supply_publish" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>����ú̿��Ӧ��Ϣ</title>
<link href="css/admin_style.css" type="text/css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/uc.js"></script>
<script type="text/javascript" src="js/jquery.validate.js"></script>
<script type="text/javascript">
    $(document).ready(function() {

        //���ص�һ�������б�
        $.post("Handler/RegionHandler.ashx", { parent_id: 9000 }, function(data, status) {
            $("#selProvince").append("<option value='-1'>��ѡ��ʡ��</option>");
            $("#selCity").append("<option value='-1'>��ѡ�����</option>");
            for (var i = 0; i < data.regions.length; i++) {
                $("#selProvince").append('<option value="' + data.regions[i].id + '">' + data.regions[i].name + '</option>');
            }
        }, "json");

        //�������б�
        $("#selProvince").change(function() {
            var provinceId = $(this).val();
            if (provinceId > -1) {
                $.post("Handler/RegionHandler.ashx", { parent_id: provinceId }, function(data, status) {
                    $("#selCity").html("<option value='-1'>��ѡ�����</option>");
                    for (var i = 0; i < data.regions.length; i++) {
                        $("#selCity").append('<option value="' + data.regions[i].id + '">' + data.regions[i].name + '</option>');
                    }
                }, "json");
            }
            else if (provinceId == -1) {
                $("#selCity").html("");
                $("#selCity").append("<option value='-1'>��ѡ�����</option>");
            }
        });

        //        $.ajax({
        //            type: "POST",
        //            url: "Handler/GetCorpInfoHandler.ashx",
        //            dataType: "json",
        //            success: function(data) {
        //                for (var field in data) {
        //                    if (field.indexOf("sel") > -1) {
        //                        $("#" + field).children("option").each(function() {
        //                            if ($(this).val() == data[field]) {
        //                                $(this).attr("selected", "true");
        //                            }
        //                        });
        //                    }
        //                    else {
        //                        $("#" + field).val(data[field]);
        //                    }
        //                }
        //            }
        //        });

        var validator = $("#form1").validate({
            rules: {
                txtTitle: {
                    required: true
                },
                txtValidDate: {
                    required: true
                },
                txtQuantity: {
                    required: true
                },
                txtPrice: {
                    required: true
                },
                txtShipAddr: {
                    required: true
                },
                txtContact: {
                    required: true
                },
                txtFixTel: {
                    required: true
                },
                txtMobileTel: {
                    required: true
                }
            },
            messages: {
                txtTitle: {
                    required: "�����Ϊ��"
                },
                txtValidDate: {
                    required: "�����Ϊ��"
                },
                txtQuantity: {
                    required: "�����Ϊ��"
                },
                txtPrice: {
                    required: "�����Ϊ��"
                },
                txtShipAddr: {
                    required: "�����Ϊ��"
                },
                txtContact: {
                    required: "�����Ϊ��"
                },
                txtFixTel: {
                    required: "�����Ϊ��"
                },
                txtMobileTel: {
                    required: "�����Ϊ��"
                }
            },
            errorPlacement: function(error, element) {
                var error_container = element.next("div");
                error.appendTo(error_container.empty());
                error_container.addClass("h_alert");
            },
            success: function(label) {
                label.parent("div").empty().removeClass("h_alert");
            }
        });

        $("#submit").click(function() {
            if (validator.form()) {
                var reqeustString = "{";
                $("input[type='text']").each(function() {
                    var name = $(this).attr("name");
                    var value = $(this).val();
                    reqeustString += "'" + name + "':'" + value + "',";
                });

                $("select").each(function() {
                    var name = $(this).attr("name");
                    var value = $(this).children("option:selected").val();
                    reqeustString += "'" + name + "':'" + value + "',";
                });

                $("textarea").each(function() {
                    var name = $(this).attr("name");
                    var value = $(this).val();
                    reqeustString += "'" + name + "':'" + value + "',";
                });

                reqeustString = reqeustString.substr(0, reqeustString.length - 1);
                reqeustString += "}";
                var reqJson = eval("(" + reqeustString + ")");
                alert(reqeustString);
                //                $.ajax({
                //                    type: "POST",
                //                    url: "company_info.aspx",
                //                    data: reqJson,
                //                    dataType: "json",
                //                    success: function(data) {
                //                        if (data.statusCode == 1) {
                //                            $("#msg").empty().html("�ύ�ɹ�");
                //                        }
                //                        else {
                //                            $("#msg").empty().html("�ύʧ��");
                //                        }
                //                    }
                //                });
            }
            else {
                return false;
            }
        });
    });
</script>
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
							<li class="active"><a href="javascript:void(0);">ú̿��Ӧ</a></li>
						</ul>
					</div>
					<div id="tabMenu_Content0">
							<div class="h_itemsBody h_item_bb">
								<table>
								    <tr>
										<th width="15%"><span>*</span>��  �⣺</th>
										<td width="30%">
										    <input id="txtTitle" name="txtTitle" type="text" class="h_text4" style="width:300px" />
										    <div></div>
										</td>
										<th><span>*</span>��Ϣ��Ч�ڣ�</th>
										<td>
											<input name="txtValidDate" type="text" class="h_text" /> 
											<div></div>
										</td>
									</tr>
									<tr>
										<th width="15%"><span>*</span>ú���֣�</th>
										<td width="30%">
										<select id="selCoalType" name="selCoalType">
										    <option value="����ú">����ú</option>
										    <option value="����ú">����ú</option>
										    <option value="�紵ú">�紵ú</option>
										    <option value="����ú">����ú</option>
										    <option value="ϴ��ú">ϴ��ú</option>
										    <option value="������">������</option>
										</select>
										</td>
										<th width="15%"><span>*</span>������ϸú�ࣺ</th>
										<td>
										<select id="selCoalCategory" name="selCoalCategory">
										    <option value="����ú">����ú</option>
										    <option value="����ú1��">����ú1��</option>
										    <option value="����ú2��">����ú2��</option>
										    <option value="����ú3��">����ú3��</option>
										    <option value="ƶú">ƶú</option>
										    <option value="ƶ��ú">ƶ��ú</option>
										    <option value="��ú">��ú</option>
										    <option value="��ú">��ú</option>
										    <option value="3/1��ú">3/1��ú</option>
										    <option value="��ú">��ú</option>
										</select>
										</td>
									</tr>
									<tr>
										<th width="15%"><span>*</span>�����ȣ�</th>
										<td width="30%">
										<select id="selLidu" name="selLidu" class="h_select3">
										    <option value="1">0-50���׻�ú</option>
										    <option value="2">0-25����ĭú</option>
										    <option value="3">0-6���׷�ú</option>
										    <option value="4">����100�����ش��ú</option>
										    <option value="5">50-100���״��ú</option>
										    <option value="6">25-50�����п�ú</option>
										    <option value="7">13-25����С��ú</option>
										    <option value="8">С��13���׺�25���׻��ú</option>
										    <option value="9">13-80���׻��п�ú</option>
										    <option value="10">6-13������ú</option>
										</select>
										</td>
										<th width="15%"></th>
										<td></td>
									</tr>
									<tr>
										<th><span></span>�����أ�</th>
										<td>
										    <select id="selProvince" name="selProvince"></select> -
							                <select id="selCity" name="selCity"></select>
										</td>
										<th><span>*</span>��������</th>
										<td>
											<input id="txtQuantity" name="txtQuantity" type="text" class="h_text" /> ��
											<div></div>
										</td>
									</tr>
									<tr>
										<th><span>*</span>�ۡ���</th>
										<td>
											<input id="txtPrice" name="txtPrice" type="text" class="h_text" /> Ԫ/��
											<div></div>
										</td>
										<th><span>*</span>����أ�</th>
										<td>
											<input id="txtShipAddr" name="txtShipAddr" type="text" class="h_text4" />
											<div></div>
										</td>
									</tr>
									<tr>
										<th>�ӷ��ݣ�</th>
										<td>
											<input id="txtHuiFa" name="txtHuiFa" type="text" value="0" class="h_text" /> %
											<div></div>
										</td>
										<th>�򡡷ݣ�</th>
										<td>
											<input id="txtLiuFen" name="txtLiuFen" type="text" value="0" class="h_text" /> %
											<div></div>
										</td>
									</tr>
									<tr>
										<th>��еǿ�ȣ�</th>
										<td>
											<input id="txtJixieQiangdu" name="txtJixieQiangdu" type="text" value="0" class="h_text" /> %
										</td>
										<th>����ǿ�ȣ�</th>
										<td>
											<input id="txtKangSuiQiangdu" name="txtKangSuiQiangdu" type="text" value="0" class="h_text" /> %
										</td>
									</tr>
									<tr>
										<th>�ҡ��ݣ�</th>
										<td>
											<input id="txtAsh" name="txtAsh" type="text" value="0" class="h_text" /> %
										</td>
										<th>ˮ���ݣ�</th>
										<td>
											<input id="txtWater" name="txtWater" type="text" value="0" class="h_text" /> %
										</td>
									</tr>
									<tr>
										<th>��ĥǿ�ȣ�</th>
										<td>
											<input id="txtNaiMo" name="txtNaiMo" type="text" value="0" class="h_text" /> %
										</td>
										<th>�����ʣ�</th>
										<td>
											<input id="txtQiKong" name="txtQiKong" type="text" value="0" class="h_text" /> %
										</td>
									</tr>
									<tr>
										<th>��Ӧ�ԣ�</th>
										<td>
											<input id="txtReflection" name="txtReflection" type="text" value="0" class="h_text" />
										</td>
										<th>��ĭ������</th>
										<td>
											<input id="txtJiaoMo" name="txtJiaoMo" type="text" value="0" class="h_text" /> %
										</td>
									</tr>
								</table>
							</div>
							<div id="user_info" class="h_itemsBody">
								<table>
									<tr>
										<th width="15%"><span>*</span>��ϵ�ˣ�</th>
										<td width="30%">
											<input id="txtContact" name="txtContact" type="text" class="h_text" />
											<div></div>
										</td>
										<th width="15%"> </th>
										<td> </td>
									</tr>
									<tr>
										<th><span>*</span>ְ����</th>
										<td>
											<select id="selOccupation" name="selOccupation">
											    <option value="ְԱ">ְԱ</option>
											    <option value="����">����</option>
											    <option value="����">����</option>
											    <option value="�ܼ�">�ܼ�</option>
											</select>
										</td>
										<th> </th>
										<td> </td>
									</tr>
									<tr>
										<th><span>*</span>��ϵ�绰��</th>
										<td>
											<input id="txtFixTel" name="txtFixTel" type="text" class="h_text" />
											<div></div>
										</td>
										<th><span></span>�����棺</th>
										<td>
											<input id="txtFax" name="txtFax" type="text" class="h_text" />
											<div></div>
										</td>
									</tr>
									<tr>
										<th><span>*</span>�֡�����</th>
										<td>
											<input id="txtMobileTel" name="txtMobileTel" type="text" class="h_text" />
											<div></div>
										</td>
										<th><span></span>�����ʼ���</th>
										<td>
											<input id="txtEmail" name="txtEmail" type="text" class="h_text3" />
										</td>
									</tr>
									<tr>
										<th>��˾��ַ��</th>
										<td>
											<input id="txtAddr" name="txtAddr" type="text" class="h_text4" />
										</td>
										<th> </th>
										<td>
										</td>
									</tr>
									<tr>
										<th> </th>
										<td>
                                            <input id="submit" name="submit" type="button" value="�� ��" class="h_buttun1" />
											<input id="reset" name="reset" type="button" value="�� ��" class="h_buttun1" />
										</td>
										<th> </th>
										<td> </td>
									</tr>
								</table>
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
