using TestForHH.ORM;

namespace TestForHH.DTO
{
    public class PatientUpdateDTO
    {
        public int Id { get; set; }
        //public string SecondName { get; set; }
        //public string Name { get; set; }
        //public string Patronymic { get; set; }
        //public string Address { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string Sex { get; set; }
        //public AreaDTO Area { get; set; }
        public int AreaId { get; set; }
    }
}