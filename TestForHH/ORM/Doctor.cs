using Microsoft.AspNetCore.Mvc;

namespace TestForHH.ORM
{
    public class Doctor
    {
        public int Id { get; set; } 
        public string FullName { get; set; }

        public int CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public int AreaId { get; set; }  
        public Area Area { get; set; }
    }
}
