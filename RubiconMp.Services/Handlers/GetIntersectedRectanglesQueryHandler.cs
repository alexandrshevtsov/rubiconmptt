using MediatR;
using RubiconMp.Core.Data;
using RubiconMp.Domain;
using RubiconMp.Services.Extencions;
using RubiconMp.Services.Queries;

namespace RubiconMp.Services.Handlers
{
    public class GetIntersectedRectanglesQueryHandler : IRequestHandler<GetIntersectedRectanglesQuery, IEnumerable<Rectangle>>
    {
        private readonly IRepositoryKeyInteger<Rectangle> _rectangleRepository;

        public GetIntersectedRectanglesQueryHandler(IRepositoryKeyInteger<Rectangle> rectangleRepository)
        {
            _rectangleRepository = rectangleRepository ?? throw new ArgumentNullException(nameof(rectangleRepository));
        }

        public async Task<IEnumerable<Rectangle>> Handle(GetIntersectedRectanglesQuery request, CancellationToken cancellationToken)
        {
            var result = _rectangleRepository.Items.Where(r => r.Area.Intersects(request.ToPolygon()));
            return await Task.FromResult(result.ToList());
        }
    }
}
