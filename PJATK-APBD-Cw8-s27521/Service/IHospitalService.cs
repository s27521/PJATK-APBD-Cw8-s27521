using PJATK_APBD_Cw8_s27521.DTOs;

namespace PJATK_APBD_Cw8_s27521.Service;

public interface IHospitalService
{
    Task<IEnumerable<PatientDto>> GetAllAsync(string? search, CancellationToken cancellationToken);
}