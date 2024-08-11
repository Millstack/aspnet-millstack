using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// class for calling sweet alert modal
/// </summary>
public class SweetAlert
{
    public SweetAlert()
    {
        // constructor
    }

    public static void GetSweet(Page page, string icontype, string title, string message, string redirectUrl = null)
    {
        string confirmButtonText = "OK";
        string allowOutsideClick = "false";

        string sweetAlertScript;

        if (string.IsNullOrEmpty(redirectUrl))
        {
            sweetAlertScript = $@"
                <script>
                    Swal.fire({{
                        title: '{title}',
                        html: '{message}',
                        icon: '{icontype}',
                        confirmButtonText: '{confirmButtonText}',
                        allowOutsideClick: {allowOutsideClick}
                    }});
                </script>";
        }
        else
        {
            string resolvedUrl = page.ResolveUrl(redirectUrl);

            sweetAlertScript = $@"
                <script>
                    Swal.fire({{
                        title: '{title}',
                        html: '{message}',
                        icon: '{icontype}',
                        confirmButtonText: '{confirmButtonText}',
                        allowOutsideClick: {allowOutsideClick}
                    }}).then((result) => {{
                        if (result.isConfirmed) {{
                            window.location.href = '{resolvedUrl}';
                        }}
                    }});
                </script>";
        }

        ScriptManager.RegisterStartupScript(page, page.GetType(), "sweetAlert", sweetAlertScript, false);
    }


}