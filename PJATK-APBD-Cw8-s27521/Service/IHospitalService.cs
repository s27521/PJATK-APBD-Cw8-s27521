using PJATK_APBD_Cw8_s27521.DTOs;

namespace PJATK_APBD_Cw8_s27521.Service;

public interface IHospitalService
{
    Task<IEnumerable<PatientResponseDto>> GetAllAsync(string? search, CancellationToken cancellationToken);

    Task<BedAssignmentResponseDto> CreateBedAssignmentAsync(string pesel, CreatePatientBedAssignmentDto dto,
        CancellationToken cancellationToken);
}