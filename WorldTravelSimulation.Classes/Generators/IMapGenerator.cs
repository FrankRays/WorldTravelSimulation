using System.Collections.Generic;
using WorldTravelSimulation.Classes.Area;

namespace WorldTravelSimulation.Classes.Generators
{
    public interface IMapGenerator
    {
        IList<Field> GenerateMap(int fieldsHorizontal, int fieldsVertical);
    }
}
