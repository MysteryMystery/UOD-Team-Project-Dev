using System;

namespace WindowsHardeningSuite.windowshardeningsuite.api.database.model
{
    public class TrackedChange : IDatabaseModel
    {
        public int Id { get; set; }
        public string RegKeyId { get; set; }
        public string SetValue { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}