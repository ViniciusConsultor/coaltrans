// JavaScript Document
	//���˵�
	function showSubMenu(ss,ii)
	{
		var menuobjedt=document.getElementById(ss);
		if (menuobjedt)
		{
			if (menuobjedt.style.display=="none") 
			{
				menuobjedt.style.display="";
				document.getElementById(ii).className = "on";
				document.getElementById(ii).title="�رղ˵�";
			}
			else
			{
				menuobjedt.style.display="none"; 
				document.getElementById(ii).className = "off";
				document.getElementById(ii).title="չ���˵�";
			}
		}
	}
	function reloadpage()
	{
		parent.window_left.location.reload();
	}
