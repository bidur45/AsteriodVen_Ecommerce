using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        public StatusController() { }
        [HttpGet]
        public async Task<Response<List<EnDropdownDTO>>> GetEnums()
        {
            var list = Enum.GetValues(typeof(EnStatus))
            .Cast<EnStatus>()
                .Select(x => new EnDropdownDTO() { Id = (int)x, Name = x.ToString() }).ToList();
            return new Response<List<EnDropdownDTO>>(list);
        }
    }
}
