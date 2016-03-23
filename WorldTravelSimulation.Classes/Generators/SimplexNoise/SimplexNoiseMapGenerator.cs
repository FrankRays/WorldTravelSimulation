using System;
using System.Collections.Generic;
using System.Drawing;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.GUI;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulation.Classes.Generators.SimplexNoise
{
    public class SimplexNoiseMapGenerator : IMapGenerator
    {
        private readonly Position _currentPosition;        
        private readonly IList<Field> _map;
        private double[,] _simplexNoise;
        private int _fieldsHorizontal;
        private int _fieldsVertical;
        private Size _fieldSize;   

        public SimplexNoiseMapGenerator()
        {
            _map = new List<Field>();
            _currentPosition = new Position();
        }

        public IList<Field> GenerateMap(int fieldsHorizontal, int fieldsVertical)
        {
            _fieldsHorizontal = fieldsHorizontal;
            _fieldsVertical = fieldsVertical;

            CalculateFieldSizeFromFieldsAmount(fieldsHorizontal, fieldsVertical);

            GenerateSimplexNoise();

            _currentPosition.X = 0;
            _currentPosition.Y = 0;

            for (_currentPosition.X = 0; _currentPosition.X < 1; _currentPosition.X += _fieldSize.Width)
            {
                for (_currentPosition.Y = 0;  _currentPosition.Y < 1; _currentPosition.Y += _fieldSize.Height)
                {
                    if (IsGroundOnCurrentPosition())
                        GenerateSpecificFieldAndAddToMap(new Ground());
                    if (IsWaterOnCurrentPosition())
                        GenerateSpecificFieldAndAddToMap(new Water());
                }
            }

            return _map;
        }

        public void CalculateFieldSizeFromFieldsAmount(int fieldsHorizontal, int fieldsVertical)
        {
            _fieldSize = new Size()
            {
                Height = (double) 1/fieldsVertical,
                Width = (double) 1/fieldsHorizontal
            };
        }        

        private void GenerateSpecificFieldAndAddToMap(Field field)
        {
            field.Size = new Size() {Height = _fieldSize.Height, Width = _fieldSize.Width};
            field.Position = new Position() {X = _currentPosition.X, Y = _currentPosition.Y};
            _map.Add(field);
        }

        private void GenerateSimplexNoise()
        {
            int seed = new Random().Next(1, int.MaxValue);

            double persistence = 0.7;
            int largestFeature = 850;

            var noise = new SimplexNoiseGenerator(largestFeature, persistence, seed);
            _simplexNoise = new double[_fieldsHorizontal, _fieldsVertical];

            int xStart = 0;
            int yStart = 0;

            for (int i = 0; i < _fieldsHorizontal; i++)
            {
                for (int j = 0; j < _fieldsVertical; j++)
                {
                    int x = (xStart + i * ((_fieldsHorizontal - xStart) / _fieldsHorizontal));
                    int y = (yStart + j * ((_fieldsVertical - yStart) / _fieldsVertical));
                    _simplexNoise[i, j] = 0.5 * (1 + noise.GetNoise(x, y));
                }
            }
        }        

        public Point SimplexNoisePointFromFieldPosition(double x, double y)
        {
            int pointX = 0;
            int pointY = 0;

            if (x > 0)
            {
                pointX = (int)Math.Ceiling(x * _fieldsHorizontal) - 1;
            }
            if (y > 0)
            {
                pointY = (int)Math.Ceiling(y * _fieldsVertical) - 1;
            }                        

            return new Point(pointX, pointY);
        }                
        
        private bool IsGroundOnCurrentPosition()
        {
            Point simplexNoisePoint = SimplexNoisePointFromFieldPosition(_currentPosition.Y, _currentPosition.X);
            return _simplexNoise[simplexNoisePoint.X, simplexNoisePoint.Y] >= 0.5;
        }        

        private bool IsWaterOnCurrentPosition()
        {
            Point simplexNoisePoint = SimplexNoisePointFromFieldPosition(_currentPosition.Y, _currentPosition.X);
            return _simplexNoise[simplexNoisePoint.X, simplexNoisePoint.Y] < 0.5;
        }
    }
}
