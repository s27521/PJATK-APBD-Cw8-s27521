using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw8_s27521.DTOs;
using PJATK_APBD_Cw8_s27521.Exceptions;
using PJATK_APBD_Cw8_s27521.Service;

namespace PJATK_APBD_Cw8_s27521.Controllers;

[ApiController]
[Route("api/patients")]
public class HospitalController(IHospitalService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? search, CancellationToken cancellationToken)
    {
        return Ok(await service.GetAllAsync(search, cancellationToken));
    }

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> CreateBedAssignment(
        [FromRoute] string pesel, 
        [FromBody] CreatePatientBedAssignmentDto dto, 
        CancellationToken cancellationToken)
    {
        try
        {
            var bedAssignment = await service.CreateBedAssignmentAsync(pesel, dto, cancellationToken);
            return Created(bedAssignment.Id.ToString(), bedAssignment);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}