using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Infrastructure;
using PJATK_APBD_Cw8_s27521.Models;

namespace PJATK_APBD_Cw8_s27521.Repository;

public class HospitalRepository(MasterContext context) : IHospitalRepository
{
    private IHospitalRepository _hospitalRepositoryImplementation;

    public Task<List<PatientResponseDto>> GetAllAsync(string? search, CancellationToken cancellationToken)
    {
        return context.Patients
            .AsNoTracking()
            /*.Include(patient => patient.Admissions)
            .ThenInclude(admission => admission.Ward)
            .Include(patient => patient.BedAssignments)
            .ThenInclude(assignment => assignment.Bed)
            .ThenInclude(bed => bed.BedType)
            .Include(patient => patient.BedAssignments)
            .ThenInclude(assignment => assignment.Bed)
            .ThenInclude(bed => bed.Room)*/
            .Where(p => 
                EF.Functions.Like(p.FirstName, $"%{search}%") ||
                EF.Functions.Like(p.LastName, $"%{search}%"))
            .AsSplitQuery()
            .Select(p => new PatientResponseDto()
            {
                Pesel = p.Pesel,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Sex = p.Sex ? "Male" : "Female",
                Admissions = p.Admissions.Select(a => new AdmissionDto
                {
                    Id = a.Id,
                    AdmissionDate = a.AdmissionDate,
                    DischargeDate = a.DischargeDate,
                    Ward = new WardDto()
                    {
                        Id = a.Ward.Id,
                        Name = a.Ward.Name,
                        Description = a.Ward.Description
                    }
                }).ToList(),
                BedAssignments = p.BedAssignments.Select(b => new BedAssignmentDto()
                {
                    Id = b.Id,
                    From = b.From,
                    To = b.To,
                    Bed = new BedDto()
                    {
                        Id = b.Bed.Id,
                        BedType = new BedTypeDto()
                        {
                            Id = b.Bed.BedTypeId,
                            Name = b.Bed.BedType.Name,
                            Description = b.Bed.BedType.Description
                        },
                        Room = new RoomDto()
                        {
                            Id = b.Bed.Room.Id,
                            HasTv = b.Bed.Room.HasTv,
                            Ward = new WardDto()
                            {
                                Id = b.Bed.Room.Ward.Id,
                                Name = b.Bed.Room.Ward.Name,
                                Description = b.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            })
            .OrderByDescending(p => p.Pesel)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsPatientAsync(string pesel, CancellationToken cancellationToken)
    {
        return context.Patients.AnyAsync(e => e.Pesel == pesel, cancellationToken);
    }

    public Task<bool> ExistsBedByTypeAndWardAsync(CreatePatientBedAssignmentDto dto, CancellationToken cancellationToken)
    {
        return context.Beds.AnyAsync(e => e.BedType.Name == dto.BedType && e.Room.Ward.Name == dto.Ward, cancellationToken);
    }

    public Task<int?> GetBedByTypeAndWardAsync(CreatePatientBedAssignmentDto dto, CancellationToken cancellationToken)
    {
        return context.Beds
            .Where(e => 
                e.BedType.Name == dto.BedType && e.Room.Ward.Name == dto.Ward &&
                e.BedAssignments.Any(c => c.To.HasValue && c.To < dto.From))
            .Select(e => (int?)e.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(BedAssignment bedAssignmentDto, CancellationToken cancellationToken)
    {
        await context.BedAssignments.AddAsync(bedAssignmentDto, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}