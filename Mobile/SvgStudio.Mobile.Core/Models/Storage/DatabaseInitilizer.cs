using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Storage
{
    public static class DatabaseInitilizer
    {
        public static void Init(SQLiteConnectionWithLock connection)
        {
            connection.CreateTable<CompatibilityTag>();
            connection.CreateTable<ContentLicense>();
            connection.CreateTable<Design>();
            connection.CreateTable<DesignRegion>();
            connection.CreateTable<DesignRegion_CompatibilityTag>();
            connection.CreateTable<Fill>();
            connection.CreateTable<License>();
            connection.CreateTable<MarkupFragment>();
            connection.CreateTable<Palette>();
            connection.CreateTable<Shape>();
            connection.CreateTable<Shape_CompatibilityTag>();
            connection.CreateTable<Stroke>();
            connection.CreateTable<Template>();
        }
    }
}
