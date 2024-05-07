using RubiconMp.Core.Data;
using RubiconMp.Domain;
using RubiconMp.Services.Handlers;
using RubiconMp.Services.Queries;
using Moq;
using NetTopologySuite.Geometries;
using RubiconMp.UnitTests.Utility;

namespace RubiconMp.UnitTests
{
    public class GetIntersectedRectanglesQueryHandlerTests
    {
        [Theory]
        [InlineData(1.5, 1.5, 4.5, 8, new int[] { 1, 2, 3 })] // alle drei
        [InlineData(1.5, 1.5, 4.5, 6, new int[] { 1, 2 })] // two lower
        [InlineData(3, 9, 4, 10, new int[] { 3 })] // upper one
        [InlineData(1, 9, 4, 10, new int[] { 3 })] // partial interception
        [InlineData(3, 10, 6, 11, new int[] { 3 })] // within
        [InlineData(1.5, 7.5, 7.5, 13.5, new int[] { 3 })] // full cover
        [InlineData(3, 14, 5, 16, new int[] { })] // no interception
        public async Task Handle_Result(double x1, double y1, double x2, double y2, int[] ids)
        {
            var repoMock = new Mock<IRepositoryKeyInteger<Rectangle>>();
            var collection = GetCollection();
            var dbAsyncEnumerable = new TestDbAsyncEnumerable<Rectangle>(collection);
            repoMock.SetupGet(m => m.Items).Returns(dbAsyncEnumerable);
            var handler = new GetIntersectedRectanglesQueryHandler(repoMock.Object);
            var request = new GetIntersectedRectanglesQuery(x1, y1, x2, y2);
            var result = await handler.Handle(request, default);
            
            // check that the number and id's of rectangles in the result corresponds to reference id's
            Assert.Empty(ids.Where(i => !result.Any(r => r.Id == i)));
            Assert.Empty(result.Where(r => !ids.Any(i => r.Id == i)));
        }

        private IEnumerable<Rectangle> GetCollection()
        {
            Rectangle[] items = [
                new Rectangle() { Id = 1, Name = "Item 1", Area = GetPolygon(1, 1, 2, 2) },
                new Rectangle() { Id = 2, Name = "Item 2", Area = GetPolygon(1, 4, 4, 7) },
                new Rectangle() { Id = 3, Name = "Item 3", Area = GetPolygon(2, 8, 7, 13) },
            ];
            return items;
        }

        private Polygon GetPolygon(double x1, double y1, double x2, double y2)
        {
            var rectangleCoordinates = new Coordinate[] { new Coordinate(x1, y1), new Coordinate(x2, y1), new Coordinate(x2, y2), new Coordinate(x1, y2), new Coordinate(x1, y1) };
            var linearRing = new LinearRing(rectangleCoordinates);
            return new Polygon(linearRing);
        }
    }
}