using System;
using System.Collections.Generic;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.SimulationObjects.Terrain;

namespace WorldTravelSimulation.Classes.Generators.SimplexNoiseTerrainGenerator
{
    public class SimplexNoiseTerrainGenerator : ITerrainGenerator
    {
        private readonly IList<Terrain> _terrain;
        private double[,] _simplexNoise;
        private readonly Position _currentPosition;

        public SimplexNoiseTerrainGenerator()
        {
            _terrain = new List<Terrain>();
            _currentPosition = new Position();
        }

        public IList<Terrain> GenerateTerrain()
        {
            GenerateSimplexNoise();

            GenerateFieldsAndAddToTerrain();

            return _terrain;
        }        

        private void GenerateSimplexNoise()
        {
            int seed = new Random().Next(1, int.MaxValue);

            double persistence = 0.7;
            int largestFeature = 850;

            var noise = new SimplexNoiseGenerator(largestFeature, persistence, seed);
            _simplexNoise = new double[World.World.Instance.WorldAreaSize.Width, World.World.Instance.WorldAreaSize.Height];

            int xStart = 0;
            int yStart = 0;

            for (int i = 0; i < World.World.Instance.WorldAreaSize.Width; i++)
            {
                for (int j = 0; j < World.World.Instance.WorldAreaSize.Height; j++)
                {
                    int x = (xStart + i * ((World.World.Instance.WorldAreaSize.Width - xStart) / World.World.Instance.WorldAreaSize.Width));
                    int y = (yStart + j * ((World.World.Instance.WorldAreaSize.Height - yStart) / World.World.Instance.WorldAreaSize.Height));
                    _simplexNoise[i, j] = 0.5 * (1 + noise.GetNoise(x, y));
                }
            }
        }

        private void GenerateFieldsAndAddToTerrain()
        {
            for (_currentPosition.X = 0; _currentPosition.X < World.World.Instance.WorldAreaSize.Width; _currentPosition.X++)
            {
                for (_currentPosition.Y = 0; _currentPosition.Y < World.World.Instance.WorldAreaSize.Height; _currentPosition.Y++)
                {
                    if (IsGroundOnCurrentPosition())
                        GenerateFieldAndAddToTerrain(new Ground());
                    if (IsWaterOnCurrentPosition())
                        GenerateFieldAndAddToTerrain(new Water());
                }
            }
        }

        private void GenerateFieldAndAddToTerrain(Terrain field)
        {
            field.Size = new Size() { Height = 1, Width = 1};
            field.Position = new Position(_currentPosition.X,_currentPosition.Y);
            _terrain.Add(field);
        }        

        private bool IsGroundOnCurrentPosition()
        {
            return _simplexNoise[_currentPosition.X, _currentPosition.Y] >= 0.5;
        }        

        private bool IsWaterOnCurrentPosition()
        {            
            return _simplexNoise[_currentPosition.X, _currentPosition.Y] < 0.5;
        }
    }
}
