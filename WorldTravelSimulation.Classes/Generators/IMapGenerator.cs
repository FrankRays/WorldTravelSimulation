using System.Collections.Generic;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;

namespace WorldTravelSimulation.Classes.Generators
{
    public interface IMapGenerator
    {
        IList<Field> GenerateMap(Size mapSize, Size fieldSize);
    }
}
