using MediatR;
using Microsoft.AspNetCore.Mvc;
using RubiconMp.Services.Queries;
using RubiconMp.Web.Extensions;

namespace RubiconMp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegmentsController : ControllerBase
    {
        private readonly ILogger<SegmentsController> _logger;
        private readonly IMediator _mediator;

        public SegmentsController(ILogger<SegmentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("intersections/{x1}/{y1}/{x2}/{y2}")]
        public async Task<IActionResult> GetIntersections(double x1, double y1, double x2, double y2)
            => Ok((await _mediator.Send(new GetIntersectedRectanglesQuery(x1, y1, x2, y2))).ToModel());
    }
}
