﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coal.Util;
using Coal.BLL;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.email.Text) && !string.IsNullOrEmpty(this.password.Text))
        {
            string nickName = string.Empty;
            int userId = -1;
            if (UserManager.ValidLogin(this.email.Text, this.password.Text, ref nickName, ref userId))
            {
                //写cookies
                string key = nickName + "|" + this.email.Text + "|" + userId.ToString();
                string validKey = CryptoHelper.Encrypt(key, "coalchina");
                HttpCookie cookie = new HttpCookie("login_info");
                cookie.Value = validKey;
                //cookie.Expires = DateTime.Now.AddDays(1);
                Response.SetCookie(cookie);
                Response.Redirect("index.html");
            }
            else
            {
                this.errorMsg.Visible = true;
            }
        }
    }
}
