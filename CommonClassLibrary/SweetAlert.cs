using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CommonClassLibrary
{
    public static class SweetAlert
    {
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



        public static void GetSweet_ModaL(Page page, string icontype, string title, string message, string redirectUrl = null, bool closeModal = false, bool reloadPage = false)
        {
            string confirmButtonText = "OK";
            string allowOutsideClick = "false";

            string sweetAlertScript;

            if (string.IsNullOrEmpty(redirectUrl))
            {
                // If no redirect, optionally close the modal and reload the page after showing the alert
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
                            {(closeModal ? "window.parent.closeModal();" : "")}
                            {(reloadPage ? "window.parent.location.reload();" : "")}
                        }}
                    }});
                </script>";
            }
            else
            {
                // If a redirect URL is provided, redirect after showing the alert
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
                            {(closeModal ? "window.parent.closeModal();" : "")}
                            window.location.href = '{resolvedUrl}';
                            {(reloadPage ? "window.parent.location.reload();" : "")}
                        }}
                    }});
                </script>";
            }

            ScriptManager.RegisterStartupScript(page, page.GetType(), "sweetAlert", sweetAlertScript, false);
        }

    }
}
