using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Windows;
using System.Collections.ObjectModel;

namespace Webservice_test
{
    /// <summary>
    /// Summary description for WebService1Test
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1Test : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello Worldlolol";
        }


        [WebMethod]
        public string AddTextBox(string id, string name, string content, bool child, string parentId)
        {
            return id;
        }
    }
}
