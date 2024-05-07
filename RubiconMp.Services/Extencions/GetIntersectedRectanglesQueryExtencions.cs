using NetTopologySuite.Geometries;
using RubiconMp.Services.Queries;

namespace RubiconMp.Services.Extencions
{
    public static class GetIntersectedRectanglesQueryExtencions
    {
        public static Polygon ToPolygon(this GetIntersectedRectanglesQuery request)
        {
            var rectangleCoordinates = new Coordinate[]
                {
                new Coordinate(request.X1, request.Y1),  // Bottom-left vertex
                new Coordinate(request.X2, request.Y1), // Top-left vertex
                new Coordinate(request.X2, request.Y2),// Top-right vertex
                new Coordinate(request.X1, request.Y2), // Bottom-right vertex
                new Coordinate(request.X1, request.Y1)   // Closing vertex to complete the polygon
                };

            // Create a linear ring from the rectangle coordinates
            var linearRing = new LinearRing(rectangleCoordinates);

            // Create a polygon from the linear ring
            var rectangle = new Polygon(linearRing);

            return rectangle;
        }
    }
}
