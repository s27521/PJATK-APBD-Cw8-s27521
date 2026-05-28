namespace PJATK_APBD_Cw8_s27521.DTOs;

public class RoomDto
{
    public string Id { get; set; } = string.Empty;
    public bool HasTv { get; set; }
    public WardDto Ward { get; set; } = new();
}