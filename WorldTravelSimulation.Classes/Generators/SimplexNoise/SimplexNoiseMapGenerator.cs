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
        private readonly IList<Field> _map;
        private double[,] _simplexNoise;
        private int _fieldsHorizontal;
        private int _fieldsVertical;
        private Size _fieldSize;
        private Point _currentPoint;

        public SimplexNoiseMapGenerator()
        {
            _map = new List<Field>();
            _currentPoint = new Point();
            _fieldSize = new Size();
        }

        public IList<Field> GenerateMap(int fieldsHorizontal, int fieldsVertical)
        {
            _fieldsHorizontal = fieldsHorizontal;
            _fieldsVertical = fieldsVertical;

            CalculateFieldSizeFromFieldsAmount(fieldsHorizontal, fieldsVertical);

            GenerateSimplexNoise();

            GenerateFieldsAndAddToMap();

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

        private void GenerateFieldsAndAddToMap()
        {
            for (_currentPoint.X = 0; _currentPoint.X < _fieldsHorizontal; _currentPoint.X++)
            {
                for (_currentPoint.Y = 0; _currentPoint.Y < _fieldsVertical; _currentPoint.Y++)
                {
                    if (IsGroundOnCurrentPoint())
                        GenerateSpecificFieldAndAddToMap(new Ground());
                    if (IsWaterOnCurrentPoint())
                        GenerateSpecificFieldAndAddToMap(new Water());
                }
            }
        }

        private void GenerateSpecificFieldAndAddToMap(Field field)
        {
            field.Size = new Size() {Height = _fieldSize.Height, Width = _fieldSize.Width};
            field.Position = GetFieldPositionFromCurrentPoint();
            _map.Add(field);
        }

        private Position GetFieldPositionFromCurrentPoint()
        {           
            Position fieldPosition = new Position()
            {
                X = _fieldSize.Width * _currentPoint.X,
                Y = _fieldSize.Height * _currentPoint.Y
            };

            return fieldPosition;
        }

        private bool IsGroundOnCurrentPoint()
        {
            return _simplexNoise[_currentPoint.X, _currentPoint.Y] >= 0.5;
        }        

        private bool IsWaterOnCurrentPoint()
        {            
            return _simplexNoise[_currentPoint.X, _currentPoint.Y] < 0.5;
        }
    }
}
