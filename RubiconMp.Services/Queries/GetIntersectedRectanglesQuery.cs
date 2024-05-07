using MediatR;
using RubiconMp.Domain;

namespace RubiconMp.Services.Queries
{
    public class GetIntersectedRectanglesQuery : IRequest<IEnumerable<Rectangle>>
    {
        public GetIntersectedRectanglesQuery(double x1, double y1, double x2, double y2)
        { 
            X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;
        }

        public double X1 { get; private set; }
        public double Y1 { get; private set;}
        public double X2 { get; private set;}
        public double Y2 { get; private set;}
    }
}
