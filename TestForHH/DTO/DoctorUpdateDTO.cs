using TestForHH.ORM;

namespace TestForHH.DTO
{
    public class DoctorUpdateDTO
    {
        public int Id { get; set; }
        public int CabinetId { get; set; } 
        public int SpecializationId { get; set; } 
        public int AreaId { get; set; }
    }
}