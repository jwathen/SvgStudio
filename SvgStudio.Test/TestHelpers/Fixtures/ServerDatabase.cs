using SvgStudio.Shared.StorageModel;
using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.TestHelpers.Fixtures
{
    public static class ServerDatabase
    {
        public static void Reset()
        {
            Empty();

            using (var connection = CreateConnection())
            {
                connection.Open();
                string populate = GetPopulateScript();
                var command = new SqlCommand(populate, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void Empty()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var checkpoint = new Respawn.Checkpoint();
                checkpoint.TablesToIgnore = new[] { "Elmah_Error" };
                checkpoint.Reset(connection);
            }
        }

        private static SqlConnection CreateConnection()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SvgStudio"].ConnectionString;
            return new SqlConnection(connectionstring);
        }


        private static string GetPopulateScript()
        {
            using (var resourceStream = typeof(SvgStudio.Web.MvcApplication).Assembly.GetManifestResourceStream("SvgStudio.Web.App_Data.Populate.sql"))
            using (var streamReader= new StreamReader(resourceStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
