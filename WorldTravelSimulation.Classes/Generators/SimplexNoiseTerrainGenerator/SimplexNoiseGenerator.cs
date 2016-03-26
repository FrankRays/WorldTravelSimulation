using System;

namespace WorldTravelSimulation.Classes.Generators.SimplexNoiseTerrainGenerator
{
    public class SimplexNoiseGenerator
    {
        private readonly SimplexNoiseOctave[] _octaves;
        private readonly double[] _frequencys;
        private readonly double[] _amplitudes;

        private int _largestFeature;
        private double _persistence;
        private int _seed;

        public SimplexNoiseGenerator(int largestFeature, double persistence, int seed)
        {
            _largestFeature = largestFeature;
            _persistence = persistence;
            _seed = seed;

            int numberOfOctaves = (int)Math.Ceiling(Math.Log10(largestFeature) / Math.Log10(2));

            _octaves = new SimplexNoiseOctave[numberOfOctaves];
            _frequencys = new double[numberOfOctaves];
            _amplitudes = new double[numberOfOctaves];

            Random rand = new Random(seed);

            for (int i = 0; i < numberOfOctaves; i++)
            {
                _octaves[i] = new SimplexNoiseOctave(rand.Next());

                _frequencys[i] = Math.Pow(2, i);
                _amplitudes[i] = Math.Pow(persistence, _octaves.Length - i);
            }
        }
        
        public double GetNoise(int x, int y)
        {
            double result = 0;

            for (int i = 0; i < _octaves.Length; i++)
                result = result + _octaves[i].Noise(x / _frequencys[i], y / _frequencys[i]) * _amplitudes[i];

            return result;
        }
    }
}
