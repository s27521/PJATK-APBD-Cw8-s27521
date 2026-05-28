namespace PJATK_APBD_Cw8_s27521.DTOs;

public class PatientDto
{
    public string Pesel { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Sex  { get; set; } = string.Empty;
    public ICollection<AdmissionDto> Admissions = [];
    public ICollection<BedAssignmentDto> BedAssignments = [];
}