using NUnit.Framework;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;

namespace WorldTravelSimulation.Tests
{
    public class AreaTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(0.5, 0.5)]
        public void FieldCoverPosition(double pointPositionX, double pointPositionY)
        {
            Field f = new Field()
            {
                Position = new Position
                {
                    X = 0,
                    Y = 0
                },
                Size = new Size
                {
                    Width = 1,
                    Height = 1
                }
            };
            Assert.AreEqual(true, f.DoesFieldCoverPosition(new Position()
            {
                X = pointPositionX,
                Y = pointPositionY
            }));
        }
        [Test]
        public void FieldNotCoverPosition()
        {
            Field f = new Field()
            {
                Position = new Position
                {
                    X = 0,
                    Y = 0
                },
                Size = new Size
                {
                    Width = 1,
                    Height = 1
                }
            };
            Assert.AreEqual(false, f.DoesFieldCoverPosition(new Position()
            {
                X = 1.1,
                Y = 0
            }));
        }                

        [TestCase(1,1)]
        [TestCase(11,11)]
        [TestCase(100,100)]
        [TestCase(11,13)]
        [TestCase(13,11)]
        [TestCase(331,111)]
        public void GeneratedMapSizeTest(int fieldsHorizontal, int fieldsVertical)
        {
            Map map = new Map(fieldsHorizontal,fieldsVertical);
            map.GenerateMap();

            int mapSize = fieldsHorizontal*fieldsVertical;

            Assert.AreEqual(mapSize, map.GetAllFields().Count);
        }
    }
}
