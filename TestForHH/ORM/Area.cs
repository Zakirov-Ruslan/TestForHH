using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TestForHH.ORM
{
    public class Area
    {
        public int Id { get; set; } 
        public int Number { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}