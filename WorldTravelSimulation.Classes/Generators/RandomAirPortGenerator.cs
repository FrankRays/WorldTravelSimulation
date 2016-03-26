using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.SimulationObjects;
using WorldTravelSimulation.Classes.SimulationObjects.StaticSimulationObjects;
using WorldTravelSimulation.Classes.SimulationObjects.Terrain;

namespace WorldTravelSimulation.Classes.Generators
{
    public class RandomAirPortGenerator : IAirPortGenerator
    {
        public Size AirPortSize { get; set; }
        private readonly Position _currentAirPortPosition;
        private IList<SimulationObject> _objectsInCurrentPosition;
        private readonly IList<AirPort> _airPorts;
        private readonly Random _random;

        public RandomAirPortGenerator()
        {
            _currentAirPortPosition = new Position();
            _objectsInCurrentPosition = new List<SimulationObject>();
            _airPorts = new List<AirPort>();
            _random = new Random();
            AirPortSize = new Size(5,5);
        }

        public IList<AirPort> GenerateAirPorts(int amount)
        {
            int i = 0;
            while (i < amount)
            {
                RandomCurrentPosition();
                GetObjectsInCurrentPosition();                                                

                if (IsNotStaticObjectInCurrentPosition() && IsGroundInCurrentPosition())
                {
                    AddAirPortInCurrentPosition();   
                    i++;
                }
            }

            return _airPorts;
        }

        private void RandomCurrentPosition()
        {
            int x = _random.Next(0, World.World.Instance.WorldAreaSize.Width - AirPortSize.Width);
            int y = _random.Next(0, World.World.Instance.WorldAreaSize.Height - AirPortSize.Height);
            _currentAirPortPosition.X = x;
            _currentAirPortPosition.Y = y;
        }

        private void GetObjectsInCurrentPosition()
        {
            _objectsInCurrentPosition = World.World.Instance.GetObjectsInPosition(_currentAirPortPosition);
        }

        private bool IsNotStaticObjectInCurrentPosition()
        {
            var staticObjectsInPosition =
                    _objectsInCurrentPosition.Where(o => o.GetType() == typeof(StaticSimulationObject)).ToList();
            return staticObjectsInPosition.Count == 0;
        }

        private bool IsGroundInCurrentPosition()
        {
            var groundInPosition =
                    _objectsInCurrentPosition.Where(o => o.GetType() == typeof(Ground)).ToList();
            return groundInPosition.Count > 0;
        }

        private void AddAirPortInCurrentPosition()
        {
            AirPort airPort = new AirPort
            {
                Position = new Position(_currentAirPortPosition.X,_currentAirPortPosition.Y),
                Size = new Size(AirPortSize.Width,AirPortSize.Height)
            };
            _airPorts.Add(airPort);
        }
    }
}
