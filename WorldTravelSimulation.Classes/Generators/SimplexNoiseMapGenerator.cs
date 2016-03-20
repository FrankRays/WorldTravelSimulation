using System;
using System.Collections.Generic;
using System.Drawing;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.Generators.SimplexNoise;
using WorldTravelSimulation.Classes.GUI;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulation.Classes.Generators
{
    public class SimplexNoiseMapGenerator : IMapGenerator
    {
        private readonly Position _currentPosition;        
        private readonly IList<Field> _map;
        private double[,] _simplexNoise;

        public int FieldsHorizontal { get; set; }
        public int FieldsVertical { get; set; }     
        public Size FieldSize { get; set; }        
        public Size MapSize { get; set; }

        public SimplexNoiseMapGenerator()
        {
            _map = new List<Field>();
            _currentPosition = new Position();

            MapSize = new Size();
            FieldSize = new Size();
        }

        public IList<Field> GenerateMap(Size mapSize, int fieldsHorizontal, int fieldsVertical)
        {
            MapSize = mapSize;
            Size fieldSize = FieldSizeFromFieldsAmount(fieldsHorizontal, fieldsVertical);
            return GenerateMap(mapSize, fieldSize);
        }

        public Size FieldSizeFromFieldsAmount(int fieldsHorizontal, int fieldsVertical)
        {
            Size fieldSize = new Size()
            {
                Height = MapSize.Height / fieldsVertical,
                Width = MapSize.Width / fieldsHorizontal
            };

            return fieldSize;
        }

        public IList<Field> GenerateMap(Size mapSize, Size fieldSize)
        {
            MapSize = mapSize;
            FieldSize = fieldSize;

            FieldsHorizontal = FieldAmountHorizontal();
            FieldsVertical = FieldAmountVertical();
            
            GenerateSimplexNoise();

            _currentPosition.X = 0;
            _currentPosition.Y = 0;

            for (_currentPosition.X = 0; _currentPosition.X <= mapSize.Width - FieldSize.Width; _currentPosition.X += FieldSize.Width)
            {
                _currentPosition.Y = 0;
                GenerateSegmentAndAddToMap();    
            }

            return _map;
        }

        private void GenerateSimplexNoise()
        {
            int seed = new Random().Next(1, int.MaxValue);

            double persistence = 0.7;
            int largestFeature = 850;

            var noise = new SimplexNoiseGenerator(largestFeature, persistence, seed);
            _simplexNoise = new double[FieldsHorizontal, FieldsVertical];

            int xStart = 0;
            int yStart = 0;

            for (int i = 0; i < FieldsHorizontal; i++)
            {
                for (int j = 0; j < FieldsVertical; j++)
                {
                    var x = (int)(xStart + i * ((FieldsHorizontal - xStart) / FieldsHorizontal));
                    var y = (int)(yStart + j * ((FieldsVertical - yStart) / FieldsVertical));
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
                pointX = (int)Math.Ceiling(x * FieldsHorizontal) - 1;
            }
            if (y > 0)
            {
                pointY = (int)Math.Ceiling(y * FieldsVertical) - 1;
            }                        

            return new Point(pointX, pointY);
        }

        private void GenerateSegmentAndAddToMap()
        {
            while (_currentPosition.Y <= MapSize.Height - FieldSize.Height)
            {
                if (IsWaterOnCurrentPosition())                
                    GenerateSpecificSegmentAndAddToMap(new Water());                
                if (IsGroundOnCurrentPosition())                
                    GenerateSpecificSegmentAndAddToMap(new Ground());                
            }
        }

        private void GenerateSpecificSegmentAndAddToMap(Field segment)
        {
            segment.Position = new Position() {X = _currentPosition.X, Y = _currentPosition.Y};
            segment.Size = new Size() {Height = FieldSize.Height, Width = FieldSize.Width};

            while (_currentPosition.Y <= MapSize.Height - FieldSize.Height && IsSpecificOnCurrentPosition(segment))
            {
                segment.Size.Height += FieldSize.Height;
                _currentPosition.Y += FieldSize.Height;
            }

            _map.Add(segment);
        }

        private bool IsSpecificOnCurrentPosition(Field field)
        {
            if (field.GetType() == typeof (Water))
                return IsWaterOnCurrentPosition();
            if (field.GetType() == typeof (Ground))
                return IsGroundOnCurrentPosition();
            return false;
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

        public int FieldAmountHorizontal()
        {
            return (int) (MapSize.Width / FieldSize.Width);
        }

        public int FieldAmountVertical()
        {
            return (int)(MapSize.Height / FieldSize.Height);
        }        
    }
}
