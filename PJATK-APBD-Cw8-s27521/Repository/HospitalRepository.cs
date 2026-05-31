using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Infrastructure;
using PJATK_APBD_Cw8_s27521.Models;

namespace PJATK_APBD_Cw8_s27521.Repository;

public class HospitalRepository : IHospitalRepository
{
    private readonly MasterContext _context;

    public HospitalRepository(MasterContext context)
    {
        _context = context;
    }
    
    public Task<List<PatientDto>> GetAllAsync(string? search, CancellationToken cancellationToken)
    {
        return _context.Patients
            .AsNoTracking()
            .Where(e => !search.IsNullOrEmpty() && (e.FirstName.Contains(search) || e.LastName.Contains(search)))
            .Include(patient => patient.Admissions)
            .ThenInclude(admission => admission.Ward)
            .Include(patient => patient.BedAssignments)
            .ThenInclude(assignment => assignment.Bed)
            .ThenInclude(bed => bed.BedType)
            .Include(patient => patient.BedAssignments)
            .ThenInclude(assignment => assignment.Bed)
            .ThenInclude(bed => bed.Room)
            .Select(p => new PatientDto()
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
                    Ward =
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
                    Bed =
                    {
                        Id = b.Bed.Id,
                        BedType =
                        {
                            Id = b.Bed.BedTypeId,
                            Name = b.Bed.BedType.Name,
                            Description = b.Bed.BedType.Description
                        },
                        Room =
                        {
                            Id = b.Bed.Room.Id,
                            HasTv = b.Bed.Room.HasTv,
                            Ward =
                            {
                                Id = b.Bed.Room.Ward.Id,
                                Name = b.Bed.Room.Ward.Name,
                                Description = b.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync(cancellationToken);
    }
}