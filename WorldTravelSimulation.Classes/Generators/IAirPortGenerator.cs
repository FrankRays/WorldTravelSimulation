using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelSimulation.Classes.SimulationObjects.StaticSimulationObjects;

namespace WorldTravelSimulation.Classes.Generators
{
    public interface IAirPortGenerator
    {
        IList<AirPort> GenerateAirPorts(int amount);
    }
}
