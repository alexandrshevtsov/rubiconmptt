using RubiconMp.Domain;
using RubiconMp.Web.Models;

namespace RubiconMp.Web.Extensions
{
    public static class RectangleModelExtencions
    {
        public static IEnumerable<RectangleModel> ToModel(this IEnumerable<Rectangle> src)
            => src.Select(r => new RectangleModel() {
                Id = r.Id,
                Name = r.Name,
                X1 = r.Area.ExteriorRing.Coordinates.First().X,
                Y1 = r.Area.ExteriorRing.Coordinates.First().Y,
                X2 = r.Area.ExteriorRing.Coordinates[2].X,
                Y2 = r.Area.ExteriorRing.Coordinates[2].Y
            } );
    }
}
