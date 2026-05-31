using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Exceptions;
using PJATK_APBD_Cw8_s27521.Models;
using PJATK_APBD_Cw8_s27521.Repository;

namespace PJATK_APBD_Cw8_s27521.Service;

public class HospitalService(IHospitalRepository hospitalRepository) : IHospitalService
{
    public async Task<IEnumerable<PatientResponseDto>> GetAllAsync(string? search, CancellationToken cancellationToken)
    {
        return await hospitalRepository.GetAllAsync(search, cancellationToken);
    }

    public async Task<BedAssignmentResponseDto> CreateBedAssignmentAsync(string pesel, CreatePatientBedAssignmentDto dto, CancellationToken cancellationToken)
    {
        if (!await hospitalRepository.ExistsPatientAsync(pesel, cancellationToken))
            throw new NotFoundException("Patient does not exist");
        
        if (!await hospitalRepository.ExistsBedByTypeAndWardAsync(dto, cancellationToken))
            throw new NotFoundException($"No beds found with type {dto.BedType} and ward {dto.Ward}");
        
        var bedId = await hospitalRepository.GetBedByTypeAndWardAsync(dto, cancellationToken);
        
        if (bedId is null)
            throw new NotFoundException("No available beds found within set timeframe");

        var bedAssignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = (int)bedId,
            From = dto.From,
            To = dto.To
        };
        
        await hospitalRepository.AddAsync(bedAssignment, cancellationToken);
        await hospitalRepository.SaveChangesAsync(cancellationToken);

        return new BedAssignmentResponseDto()
        {
            Id = bedAssignment.Id,
            PatientPesel = bedAssignment.PatientPesel,
            BedId =  bedAssignment.BedId,
            From = bedAssignment.From,
            To = bedAssignment.To
        };
    }
}