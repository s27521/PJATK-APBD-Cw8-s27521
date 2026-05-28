namespace PJATK_APBD_Cw8_s27521.DTOs;

public class AdmissionDto
{
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public WardDto Ward { get; set; } = new();
}