namespace PJATK_APBD_Cw8_s27521.DTOs;

public class BedDto
{
    public int Id { get; set; }
    public BedTypeDto BedType { get; set; } = new();
    public RoomDto Room { get; set; } = new();
}