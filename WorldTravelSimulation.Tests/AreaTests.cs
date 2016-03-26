using NUnit.Framework;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.Generators.SimplexNoiseTerrainGenerator;
using WorldTravelSimulation.Classes.SimulationObjects;
using WorldTravelSimulation.Classes.World;

namespace WorldTravelSimulation.Tests
{
    public class AreaTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(0.5, 0.5)]
        public void SimulationObjectCoverPosition(double pointPositionX, double pointPositionY)
        {
            SimulationObject f = new SimulationObject()
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
            Assert.AreEqual(true, f.DoesSimulationObjectCoverPosition(new Position()
            {
                X = pointPositionX,
                Y = pointPositionY
            }));
        }
        [Test]
        public void SimulationObjectNotCoverPosition()
        {
            SimulationObject f = new SimulationObject()
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
            Assert.AreEqual(false, f.DoesSimulationObjectCoverPosition(new Position()
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
        public void GeneratedMapSizeTest(int simulationObjectsHorizontal, int simulationObjectsVertical)
        {
            World world = new World(simulationObjectsHorizontal,simulationObjectsVertical);
            world.TerrainGenerator = new SimplexNoiseTerrainGenerator();
            world.GenerateTerrain();

            int mapSize = simulationObjectsHorizontal*simulationObjectsVertical;

            Assert.AreEqual(mapSize, world.Terrain.Count);
        }
    }
}
