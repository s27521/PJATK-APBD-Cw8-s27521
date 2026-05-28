namespace PJATK_APBD_Cw8_s27521.DTOs;

public class BedAssignmentDto
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public BedDto Bed { get; set; } = new();
}