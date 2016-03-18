using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.GUI;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulation.Classes.Generators
{
    public class MapGenerator
    {
        private Random Random;

        public IList<Field> Map { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public Position StartPosition { get; set; }

        public MapGenerator()
        {
            Random = new Random();
            Map = new List<Field>();
            StartPosition = new Position();
        }

        public void GenerateMap()
        {
            float[,] noise = NoiseGenerator.CreateAdvancedPerlinNoise(new Point() { X = MapWidth, Y = MapHeight }, 8, 8, 5);
            double fieldWidthPerMapWidth = FieldWidthPerMapWidth();
            double fieldHeightPerMapHeight = FieldHeightPerMapHeight();
            
            double segmentY = 0;
            double segmentHeight = fieldHeightPerMapHeight;

            bool water = noise[0, 0] < 0.5;
            bool added = false;

            for (double i = 0; i < 1; i += fieldWidthPerMapWidth)
            {
                water = noise[0, (int) (i*MapWidth)] < 0.5;
                segmentY = 0;
                segmentHeight = fieldHeightPerMapHeight;
                int noiseX = (int)(i * MapWidth);

                for (double j = fieldHeightPerMapHeight; j < 1; j += fieldHeightPerMapHeight)
                {
                    int noiseY = (int)(j * MapHeight);

                    if (noise[noiseY, noiseX] < 0.5)
                    {
                        if (water)
                        {
                            segmentHeight += fieldHeightPerMapHeight;
                            added = false;
                        }
                        else
                        {                            

                            AddGround(new Position() {X=i,Y=segmentY}, new Size() {Height = segmentHeight, Width = fieldWidthPerMapWidth});

                            added = true;
                            water = true;
                            segmentY = j;
                            segmentHeight = fieldHeightPerMapHeight;
                        }
                    }
                    else
                    {
                        if (water)
                        {
                            AddWater(new Position() { X = i, Y = segmentY }, new Size() { Height = segmentHeight, Width = fieldWidthPerMapWidth });

                            added = true;
                            water = false;
                            segmentY = j;
                            segmentHeight = fieldHeightPerMapHeight;
                        }
                        else
                        {
                            segmentHeight += fieldHeightPerMapHeight;
                            added = false;
                        }
                    }
                }
                if (!added)
                {
                    if (water)
                    {
                        AddWater(new Position() { X = i, Y = segmentY }, new Size() { Height = segmentHeight, Width = fieldWidthPerMapWidth });
                    }
                    else
                    {
                        AddGround(new Position() { X = i, Y = segmentY }, new Size() { Height = segmentHeight, Width = fieldWidthPerMapWidth });
                    }
                }
            }
        }

        private void AddWater(Position position, Size size)
        {
            Field field = new Water()
            {
                Position = new Position() { X = position.X, Y = position.Y },
                Size = new Size() { Height = size.Height, Width = size.Width }
            };

            Map.Add(field);
        }

        private void AddGround(Position position, Size size)
        {
            Field field = new Ground()
            {
                Position = new Position() { X=position.X,Y=position.Y},
                Size = new Size() { Height = size.Height,Width = size.Width}
            };

            Map.Add(field);
        }

        private double FieldHeightPerMapHeight()
        {
            return 1 / (double)MapHeight;
        }

        private double FieldWidthPerMapWidth()
        {
            return 1 / (double)MapWidth;
        }

        private double GetRandomNumber(double minimum, double maximum)
        {
            return Random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
