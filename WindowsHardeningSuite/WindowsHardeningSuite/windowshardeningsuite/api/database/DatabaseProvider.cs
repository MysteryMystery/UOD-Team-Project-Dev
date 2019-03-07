using System;
using System.CodeDom;
using WindowsHardeningSuite.windowshardeningsuite.api.database.model;
using LiteDB;

namespace WindowsHardeningSuite.windowshardeningsuite.api.database
{
    public class DatabaseProvider
    {
        public LiteDatabase LiteDatabase => new LiteDatabase(@"ChangeTracking.db");

        public void Insert<T>(T row) where T : IDatabaseModel
        {
            using (var db = LiteDatabase)
            {
                var rows = LiteDatabase.GetCollection<T>(row.GetType().Name);
                rows.Insert(row);
            }
        }

        public LiteCollection<T> Get<T>() where T : IDatabaseModel
        {
            using (var db = LiteDatabase)
            {
                return LiteDatabase.GetCollection<T>(typeof(T).Name);
            } 
        }
    }
}