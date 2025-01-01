using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_404 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Btn_Home_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("HomePage_Route", null));
    }
}