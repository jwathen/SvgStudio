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
