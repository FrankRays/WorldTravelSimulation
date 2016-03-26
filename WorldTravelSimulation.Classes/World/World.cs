using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.Generators;
using WorldTravelSimulation.Classes.SimulationObjects;
using WorldTravelSimulation.Classes.SimulationObjects.StaticSimulationObjects;
using WorldTravelSimulation.Classes.SimulationObjects.Terrain;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulation.Classes.World
{
    public class World
    {
        private static World _instance;
        public static World Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new World();
                }
                return _instance;
            }
        }

        public List<StaticSimulationObject> StaticSimulationObjects { get; }
        public List<Terrain> Terrain { get; }
        public Size WorldAreaSize { get; set; }
        public ITerrainGenerator TerrainGenerator { get; set; }
        public IAirPortGenerator AirPortGenerator { get; set; }

        private World()
        {
            Terrain = new List<Terrain>();
            StaticSimulationObjects = new List<StaticSimulationObject>();
            WorldAreaSize = new Size(0,0);
        }        

        public void GenerateTerrain()
        {
            IList<Terrain> terrain = TerrainGenerator.GenerateTerrain();
            Terrain.AddRange(terrain);
        }

        public void GenerateAirPorts(int amount)
        {
            IList<AirPort> airPorts = AirPortGenerator.GenerateAirPorts(amount);
            StaticSimulationObjects.AddRange(airPorts);
        }

        public IList<SimulationObject> GetObjectsInPosition(Position position)
        {
            List<SimulationObject> simulationObjects = new List<SimulationObject>();

            var terrain = Terrain.Where(x => x.DoesSimulationObjectCoverPosition(position)).ToList();
            var staticSimulationObjects = StaticSimulationObjects.Where(x => x.DoesSimulationObjectCoverPosition(position)).ToList();

            simulationObjects.AddRange(terrain);
            simulationObjects.AddRange(staticSimulationObjects);

            return simulationObjects;
        }
    }
}
