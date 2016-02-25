using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;

namespace Modelo
{
    public class EFChange
    {
        public static string GenerarConnection(string database = "DBFinca", string server = "DESKTOP-QI4E0BK", string res = "DBFinca")
        {
            EntityConnectionStringBuilder conn = new EntityConnectionStringBuilder();


            conn.Provider = "System.Data.SqlClient";
            conn.ProviderConnectionString = String.Format("data source={0};initial catalog={1};persist security info=True;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework", server, database);

            conn.Metadata = String.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", res);


            return conn.ToString();
        }
    }
}
