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
        [Test]
        public void SomeFieldInMapCoverPosition()
        {
            Map m = new Map();            

            m.AddField(new Field()
            {
                Position = new Position
                {
                    X = 0,
                    Y = 0
                },
                Size = new Size
                {
                    Width = 0.1,
                    Height = 0.1
                }
            });

            Assert.AreNotEqual(null, m.GetFieldByPosition(new Position
            {
                X = 0.05,
                Y = 0.05
            }));
        }
        [Test]
        public void AnyFieldInMapCoverPosition()
        {
            Map m = new Map();
            
            m.AddField(new Field()
            {
                Position = new Position
                {
                    X = 0,
                    Y = 0
                },
                Size = new Size
                {
                    Width = 0.1,
                    Height = 0.1
                }
            });

            Assert.AreEqual(null, m.GetFieldByPosition(new Position
            {
                X = 0.3,
                Y = 0.3
            }));
        }

        [TestCase(1,1)]
        [TestCase(11,11)]
        [TestCase(100,100)]
        [TestCase(11,13)]
        [TestCase(13,11)]
        public void GeneratedMapSizeTest(int fieldsHorizontal, int fieldsVertical)
        {
            Map map = new Map();
            map.GenerateMap(fieldsHorizontal, fieldsVertical);

            int mapSize = fieldsHorizontal*fieldsVertical;

            Assert.AreEqual(mapSize, map.GetAllFields().Count);
        }
    }
}
