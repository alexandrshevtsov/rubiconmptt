using NetTopologySuite.Geometries;
using RubiconMp.Core.Domain;

namespace RubiconMp.Domain
{
    public class Rectangle: BaseEntityWithIntegerKey
    {
        public string Name { get; set; }
        public Polygon Area { get; set; }
    }
}
