using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.TestHelpers
{
    public static class SvgStudioDataContextExtensions
    {
        public static void DisableConstraints(this SvgStudioDataContext db)
        {
            var command = db.Database.Connection.CreateCommand();
            command.CommandText = "EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'";
            EnsureOpenConnection(db);
            command.ExecuteNonQuery();
        }

        public static void EnableConstraints(this SvgStudioDataContext db)
        {
            var command = db.Database.Connection.CreateCommand();
            command.CommandText = "EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'";
            EnsureOpenConnection(db);
            command.ExecuteNonQuery();
        }

        public static void Reset(this SvgStudioDataContext db)
        {
            var checkpoint = new Respawn.Checkpoint();
            EnsureOpenConnection(db);
            checkpoint.Reset(db.Database.Connection);
        }

        private static void EnsureOpenConnection(SvgStudioDataContext db)
        {
            if (db.Database.Connection.State == ConnectionState.Closed)
            {
                db.Database.Connection.Open();
            }
        }
    }
}
