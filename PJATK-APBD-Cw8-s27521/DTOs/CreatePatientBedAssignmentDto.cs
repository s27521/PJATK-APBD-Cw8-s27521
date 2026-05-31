using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw8_s27521.DTOs;

public class CreatePatientBedAssignmentDto
{
    [Required]
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    
    [Required]
    public string BedType { get; set; } = string.Empty;
    
    [Required]
    public string Ward { get; set; } = string.Empty;
}