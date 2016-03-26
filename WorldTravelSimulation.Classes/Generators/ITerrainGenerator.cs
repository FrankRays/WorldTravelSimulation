using System.Collections.Generic;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.SimulationObjects.Terrain;

namespace WorldTravelSimulation.Classes.Generators
{
    public interface ITerrainGenerator
    {
        IList<Terrain> GenerateTerrain();
    }
}
