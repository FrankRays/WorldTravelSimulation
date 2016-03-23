using System.Drawing;
using NUnit.Framework;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulation.Tests
{
    public class PerlinNoiseGeneratorTests
    {
        readonly SimplexNoiseMapGenerator _generator = new SimplexNoiseMapGenerator
        {
            MapSize = new Size() { Height = 1, Width = 1 }
        };
        
        [TestCase(1,1,1,1)]
        [TestCase(100,100,0.01,0.01)]
        [TestCase(16,16,0.0625,0.0625)]
        public void CorrectFieldSizeFromFieldsAmountTest(int fieldsHorizontal, int fieldsVertical, double width, double height)
        {            
            Size fieldSize = _generator.FieldSizeFromFieldsAmount(fieldsHorizontal, fieldsVertical);

            Assert.AreEqual(width,fieldSize.Width);
            Assert.AreEqual(height, fieldSize.Height);
        }

        [TestCase(0.1, 10)]
        [TestCase(0.11, 9)]
        [TestCase(1, 1)]
        [TestCase(0.5, 2)]
        [TestCase(0.51, 1)]
        public void CorrectFieldAmountFromFiedSizeTest(double fieldSize, int amount)
        {
            _generator.MapSize.Width = 1;
            _generator.FieldSize.Width = fieldSize;

            int fieldAmount = _generator.FieldAmountHorizontal();

            Assert.AreEqual(amount, fieldAmount);
        }

        [TestCase(1)]
        [TestCase(11)]
        [TestCase(111)]
        [TestCase(100)]
        public void FieldAmountFromFieldSizeFromFieldsAmountTest(int fieldsHorizontal)
        {
            _generator.MapSize.Width = 1;

            Size fieldSize = _generator.FieldSizeFromFieldsAmount(fieldsHorizontal, 1);

            _generator.FieldSize.Width = fieldSize.Width;

            int fieldAmount = _generator.FieldAmountHorizontal();

            Assert.AreEqual(fieldsHorizontal,fieldAmount);
        }        

        [TestCase(0,0,0,0)]
        [TestCase(0.1,0.1,0,0)]
        [TestCase(1,1,9,9)]
        [TestCase(0.2,0.2,1,1)]
        [TestCase(0.21,0.21,2,2)]
        public void PerlinNoisePointFromFieldPositionTest(double x, double y, int perlinX, int perlinY)
        {
            _generator.FieldsVertical = 10;
            _generator.FieldsHorizontal = 10;

            Point perlinPoint = _generator.SimplexNoisePointFromFieldPosition(x, y);
            Assert.AreEqual(perlinX,perlinPoint.X);
            Assert.AreEqual(perlinY,perlinPoint.Y);
        }    
    }
}
