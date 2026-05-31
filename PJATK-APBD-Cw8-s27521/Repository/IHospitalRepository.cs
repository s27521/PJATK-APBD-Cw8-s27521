using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Models;

namespace PJATK_APBD_Cw8_s27521.Repository;

public interface IHospitalRepository
{
    Task<List<PatientResponseDto>> GetAllAsync(string? search, CancellationToken cancellationToken);
    Task<bool> ExistsPatientAsync(string pesel, CancellationToken cancellationToken);
    Task<bool> ExistsBedByTypeAndWardAsync(CreatePatientBedAssignmentDto dto, CancellationToken cancellationToken);
    Task<int?> GetBedByTypeAndWardAsync(CreatePatientBedAssignmentDto dto, CancellationToken cancellationToken);
    Task AddAsync(BedAssignment bedAssignmentDto, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}