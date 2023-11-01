using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class DbConnection
    {
        public string ConnString { get; } = "User ID=ADMIN; Password=#B1taSm1etana2; Data Source=pd_high";

        public static string ConnStringStatic { get; } = "User ID=ADMIN; Password=#B1taSm1etana2; Data Source=pd_high";

        //public string ConnString { get; } = "DATA SOURCE=localhost:1521/XEPDB1;;" +
        //"PERSIST SECURITY INFO=True;USER ID=admin; password=opov027; Pooling = True; ";

        //public static string ConnStringStatic { get; } = "DATA SOURCE=localhost:1521/XEPDB1;;" +
        // "PERSIST SECURITY INFO=True;USER ID=admin; password=opov027; Pooling = True; ";

    }


}