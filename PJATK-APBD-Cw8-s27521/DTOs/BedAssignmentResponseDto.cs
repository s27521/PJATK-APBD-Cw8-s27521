namespace PJATK_APBD_Cw8_s27521.DTOs;

public class BedAssignmentResponseDto
{
    public int Id { get; set; }
    public string PatientPesel { get; set; } = string.Empty;
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public int BedId { get; set; }
}