using System;

namespace DemoConsole.EntityFrameworkPractice
{
    public class Experience
    {
        public int ID { get; set; }
        public short UserLevel { get; set; }
        public int Exp { get; set; }
        public DateTime CreatedDate { get; set; }
        public short CreatedUserID { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public short LastUpdatedUserID { get; set; }
    }
}
