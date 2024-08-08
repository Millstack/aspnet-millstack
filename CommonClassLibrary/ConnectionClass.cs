using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClassLibrary
{
    public static class ConnectionClass
    {
        #region "Connections String"

        public static String connection_String_SA = $@"Data Source = 13.201.252.201;
                                                       Initial Catalog = PMSYEIDA_UAT;
                                                       Persist Security Info = True;
                                                       User Id = sa;
                                                       Password = Vvoxn%&^L4Azm3kFT4; 
                                                       Connection Timeout = 30; 
                                                       Max Pool Size = 500;
                                                       Pooling = true;";


        public static String connection_String_Local = $@"Data Source = MILIND\SQLEXPRESS;
                                                          Initial Catalog = MillStack;
                                                          Integrated Security = True;
                                                          Connection Timeout = 30;
                                                          Max Pool Size = 500;
                                                          Pooling = true;";

        #endregion
    }
}
