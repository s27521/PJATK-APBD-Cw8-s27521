using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Repository;

namespace PJATK_APBD_Cw8_s27521.Service;

public class HospitalService(IHospitalRepository hospitalRepository) : IHospitalService
{
    public async Task<IEnumerable<PatientDto>> GetAllAsync(string? search, CancellationToken cancellationToken)
    {
        return await hospitalRepository.GetAllAsync(search, cancellationToken);
    }
}