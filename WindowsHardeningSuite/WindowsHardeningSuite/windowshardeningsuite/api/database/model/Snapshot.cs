using System;

namespace WindowsHardeningSuite.windowshardeningsuite.api.database.model
{
    public class Snapshot : IDatabaseModel
    {
        public int Id { get; set; }
        public int TrackedChangeId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}