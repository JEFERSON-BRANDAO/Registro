﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Compras
{
    public partial class sair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["id"] = "0";
            //Response.Redirect("Login.aspx");
            Response.RedirectToRoute("login");

        }
    }
}