using System;
using System.Drawing;

namespace WorldTravelSimulation.Classes.Generators
{
    public static class NoiseGenerator
    {
        private static Random Random = new Random();

        public static float[] GenerateRandom(float min, float max, int numberOfValues)
        {
            float[] values = new float[numberOfValues];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (float) (Random.NextDouble()*(max - min) + min);
            }

            return values;
        }

        public static float[,] Create3DNoise(int xValues, int yValues)
        {
            float[] values = GenerateRandom(0.0000f, 1.0000f, xValues*yValues);
            float[,] values2D = new float[yValues, xValues];

            for (int y = 0; y < values.Length/xValues; y++)
            {
                for (int x = 0; x < values.Length/yValues; x++)
                {
                    values2D[y, x] = values[x + (y*xValues)];
                }
            }

            return values2D;
        }

        public static float[,] CreateAdvancedPerlinNoise(Point dimentions, int initialxValues, int initialyValues,
            byte steps)
        {
            float[,] values = new float[dimentions.X, dimentions.Y];

            for (int i = 0; i < steps; i++)
            {
                float[,] currentValues =
                    Interpolate(
                        Create3DNoise(initialxValues*(int) Math.Pow(2, i), initialyValues*(int) Math.Pow(2, i)),
                        dimentions);

                for (int y = 0; y < currentValues.GetLength(0); y++)
                {
                    for (int x = 0; x < currentValues.GetLength(1); x++)
                    {
                        if (i == 0)
                            values[y, x] += (float) ((currentValues[y, x])*(1f/(Math.Pow(2, i))));
                        else
                            values[y, x] += (float) ((currentValues[y, x] - 0.5f)*(1f/(Math.Pow(2, i))));
                    }
                }
            }

            return values;
        }

        public static float[,] Interpolate(float[,] values, Point dimentions)
        {
            float[,] newPoints = new float[dimentions.X, dimentions.Y];

            if (dimentions.Y%(values.GetLength(0)) == 0
                || dimentions.X%(values.GetLength(1)) == 0)
            {
                int xCount = 0;
                int yCount = 0;
                int lastSolidX = 0;
                int lastSolidY = 0;
                for (int y = 0; y < dimentions.Y; y++)
                {
                    if (y%(dimentions.Y/values.GetLength(0)) == 0)
                    {
                        for (int x = 0; x < dimentions.X; x++)
                        {
                            if (x%(dimentions.X/(values.GetLength(1) - 1)) == 0)
                            {
                                //place point                                    
                                newPoints[y, x] = values[yCount, xCount];
                                lastSolidX = x;
                                xCount++;
                            }
                            else
                            {
                                //interpolate
                                if (xCount < values.GetLength(1))
                                {
                                    newPoints[y, x] =
                                        TrigPosition(
                                            values[yCount, xCount - 1],
                                            values[yCount, xCount],
                                            (float) (x - (float) lastSolidX)/(dimentions.X/(values.GetLength(1) - 1)));
                                }
                            }
                        }
                        lastSolidY = y;
                        lastSolidX = 0;
                        yCount++;
                        xCount = 0;
                    }
                }


                for (int y = 0; y < dimentions.Y; y++)
                {
                    if (y%(dimentions.Y/values.GetLength(0)) == 0)
                    {
                        lastSolidY = y;
                    }
                    else
                    {
                        for (int x = 0; x < dimentions.X; x++)
                        {

                            if (lastSolidY < newPoints.GetLength(0) - (dimentions.Y/values.GetLength(0)))
                            {
                                newPoints[y, x] = TrigPosition(newPoints[lastSolidY, x],
                                    newPoints[(int) lastSolidY + (dimentions.Y/values.GetLength(0)), x],
                                    (float) (y - (float) lastSolidY)/(dimentions.Y/values.GetLength(0)));
                            }
                            else
                            {
                                newPoints[y, x] = TrigPosition(newPoints[lastSolidY, x],
                                    newPoints[values.GetLength(0) - 1, x],
                                    (float) (y - (float) lastSolidY)/(dimentions.Y/values.GetLength(0)));
                            }
                        }
                    }
                }
                return newPoints;
            }
            else
            {

                throw new System.InvalidOperationException(
                    "Dimentions of values array must be divisible into dimentions.");
            }
        }

        public static float TrigPosition(float a, float b, float x)
        {
            float ft = x*3.1415927f;
            float f = (float) (1f - Math.Cos(ft))*.5f;

            return a*(1f - f) + b*f;
        }
    }
}
