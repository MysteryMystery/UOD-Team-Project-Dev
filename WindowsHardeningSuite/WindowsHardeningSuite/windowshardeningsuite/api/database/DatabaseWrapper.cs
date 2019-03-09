using WindowsHardeningSuite.windowshardeningsuite.api.database.model;
using LiteDB;
using MahApps.Metro.Converters;

namespace WindowsHardeningSuite.windowshardeningsuite.api.database
{
    public class DatabaseWrapper
    {
        private static DatabaseWrapper _instance;
        public LiteDatabase LiteDatabase => new LiteDatabase(@"ChangeTracking.db");

        private DatabaseWrapper()
        {
            
        }

        public static DatabaseWrapper GetInstance()
        {
            if (_instance == null)
                _instance = new DatabaseWrapper();
            return _instance;
        }

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