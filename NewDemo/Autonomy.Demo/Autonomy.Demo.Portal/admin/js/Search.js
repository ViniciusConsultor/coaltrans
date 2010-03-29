﻿var key_word;
var date_select;
var condition_sort;
var a_query_type;
var title_list=["关注事件","重大社会事件","重大案件"];
/* 分页初始化 */
function GetPager(page_count,query_type) {
    
    var display_page_count = 10;
    if (page_count < 10) {
        display_page_count = page_count;
    }
    
    var pager_content=new StringBuffer();
    //pager_content.append("<span>上一页</span>");
    pager_content.append("<span class=\"current\" id=\"Pager" + 1 + "\" style=\"margin-left:10px;\" >[" + 1 + "]</span>");

    var keyword = key_word;
    for (var i = 2; i <= display_page_count; i++)
    {
        pager_content.append("<span id=\"Pager" + i + "\" style=\"margin-left:10px;\" onclick=\"GetFocus('Pager'," + i + "," + page_count + ",'" + keyword + "',"+query_type+")\"><a href=\"javascript:void(null);\" >[" + i + "]</a></span>");
    }

    //pager_content.append("<span><a href=\"javascript:void(null);\">下一页</a></span>");
    $("#PagerList").html(pager_content.toString());
}
/* 分页按钮事件 */
function GetFocus(obj,current_page,page_count,keyword,query_type) {
    var start = (current_page - 1) * 10 + 1;
    $.get("../Handler/SearchResult.ashx", { 'keyword': keyword.toString(), 'query_type': query_type, 'start': start, 'RangeDate':date_select,'sort':condition_sort},
        function(data) {
            var a_data = data.split('※');
            $("#SearchResult").html(a_data[0]);
        });

        var start_index = 1;

        if (current_page >= 6 && current_page <= page_count - 4) {
            start_index = current_page - 5;
            end_index = current_page + 4;
        }
        else if (current_page < 6) {
            start_index = 1;
            if(page_count<=10)
            {
                end_index=page_count;
            }
            else
            {
                end_index = 10;
            }
        }
        else{
            start_index = page_count - 9;
            end_index = page_count;
        }
        
        var pager_content = new StringBuffer();
        for (var page_index = start_index; page_index <= end_index; page_index++) {
            pager_content.append("<span id=\"Pager" + page_index + "\" style=\"margin-left:10px;\" onclick=\"GetFocus('Pager'," + page_index + "," + page_count + ",'" + keyword + "',"+query_type+")\"><a href=\"javascript:void(null);\" >[" + page_index + "]</a></span>");
        }
        $("#PagerList").empty().html(pager_content.toString());
        $("#" + obj + current_page).attr("class", "current");
        $("#choose_all").attr("checked","");
}
/* 加载左边菜单 */
function GetMenu(query_type,keyword_value,cate_type)
{
    $.post("../Handler/GetInfo.ashx",
            { "type": "Menu","cate_type":cate_type,"cate_id":keyword_value },
            function(data, textStatus) {
                var Content = new StringBuffer();               
                if (data != null) {
                    for (var item in data) {                   
                        var cate_list=data[item];
                        for(var i=0;i<cate_list.length;i++)
                        {
                            if(i==0)
                            {
                                Content.append("<dt>"+title_list[parseInt(cate_list[i]["CateType"])-1]+"</dt>");
                            }
                            if(cate_list[i]["MainCateID"]==keyword_value)
                            {
                                $("#DisKeyWord").html("\""+cate_list[i]["CateDisplay"].substring(0,10)+"\"");
                                Content.append("<dd pid=\""+cate_list[i]["MainCateID"]+","+query_type+"\" class=\"current\"><a href=\"javascript:void(null);\" title=\""+cate_list[i]["CateDisplay"]+"\">" +cate_list[i]["CateDisplay"]+ "</a></dd>");
                            }
                            else
                            {
                                Content.append("<dd pid=\""+cate_list[i]["MainCateID"]+","+query_type+"\"><a href=\"javascript:void(null);\" title=\""+cate_list[i]["CateDisplay"]+"\">" +cate_list[i]["CateDisplay"]+ "</a></dd>");
                            }
                        }
                    }
                }
                $("#PublicLeftMenu").html(Content.toString());
                //为PublicLeftMenu一下的dd标签增加了点击事件
                setClickofPublicLeftMenu();
            }, "json");       
}
/*  */
function setClickofPublicLeftMenu()
{
    var dd_list=$("#PublicLeftMenu").find("dd");

    $(dd_list).each(function() {

        var search_param = $(this).attr("pid").split(',');
        $(this).click(function() {
            $(dd_list).attr("class", "");
            $(this).attr("class", "current");
            $("#DisKeyWord").html("\"" + $(this).children("a").html().substring(0, 10) + "\"");
            LoadData(search_param[0], search_param[1]);
        });
    });
}
/* 页面数据加载  keyword_value查询关键字或分类ID,query_type为查询类型 */
function LoadData(keyword_value,query_type)
{
    GetCondition();
    key_word=keyword_value;
    a_query_type=query_type;
    //$("#keyword").val(keyword_value);
    $.get("../Handler/SearchResult.ashx", { 'keyword': keyword_value, 'query_type': query_type, 'start': 1,'RangeDate':date_select,'sort':condition_sort },
            function(data) {
                var a_data = data.split('※');
                //搜索结果
                $("#SearchResult").html(a_data[0]);
                //记录总数
                var count = a_data[1];
                $("#TotalCount").html(count + "条");
                //页数
                var page_count = a_data[2];

                if (count > 0) {
                    GetPager(page_count,query_type);
                }
                else {
                    $("#PagerList").html("对不起，没有数据！");
                }
            });
}
/* 获取查询条件 */
/* 为全局变量date_select和condition_sort赋值 */
function GetCondition()
{
    date_select=$("#DataSelect").children(".current").attr("date"); 
    $("#date_condition").html($("#DataSelect").children(".current").children("a").html());    
    condition_sort=$("#CorreSort").children(".current").attr("sort");
}
/* 为条件增加按钮事件 */
function BtnConditionClick(object_name,curren_class_name)
{
    $("#"+object_name).children().each(function(){        
        $(this).click(function(){
            if($(this).attr("class")!="current")
            {                
                $("#"+object_name).children().removeClass(curren_class_name);
                $(this).addClass(curren_class_name);
                GetCondition();            
                LoadData(key_word,a_query_type);
            }
        });
    });    
}
/* 条件按钮初始化 */
function BtnInnit()
{
    BtnConditionClick("DataSelect","current");
    BtnConditionClick("CorreSort","current");
}