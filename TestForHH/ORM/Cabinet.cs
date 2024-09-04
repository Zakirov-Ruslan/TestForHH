using System.Diagnostics;

namespace TestForHH.ORM
{
    public class Cabinet
    {
        public int Id { get; set; } 
        public int Number { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}