using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Models;

namespace PJATK_APBD_Cw8_s27521.Repository;

public interface IHospitalRepository
{
    Task<List<PatientDto>> GetAllAsync(string? search, CancellationToken cancellationToken);
}