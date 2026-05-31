using Microsoft.AspNetCore.Mvc;
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
}