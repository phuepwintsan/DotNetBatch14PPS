using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConsoleApp4
{
    public static class AppSetting
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder { get; } = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-73Q5S0H6",
            InitialCatalog = "Testdb",
            UserID = "sa",
            Password = "p@ssw0rd",
            TrustServerCertificate = true
        };
    }
}
