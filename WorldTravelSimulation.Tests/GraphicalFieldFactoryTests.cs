using System.Drawing;
using NUnit.Framework;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.GUI;
using Size = System.Drawing.Size;

namespace WorldTravelSimulation.Tests
{
    public class GraphicalFieldFactoryTests
    {
        [Test]
        public void GeneralFieldCreate()
        {
            GraphicalFieldFactory.GraphicalMapSize = new Size(100,100);
            Field field = new Field()
            {
                Position = new Position() {X = 0.5, Y = 0.5},
                Size = new Classes.Format.Size() {Height = 0.1, Width = 0.1}
            };

            GraphicalField graphicalField = GraphicalFieldFactory.CreateGraphicalField(field);

            Assert.AreEqual(field.Position,graphicalField.Position);
            Assert.AreEqual(field.Size,graphicalField.Size);
            Assert.AreEqual(new Point(50,50), graphicalField.Picture.Location);
            Assert.AreEqual(new Size(10,10), graphicalField.Picture.Size);
            Assert.AreEqual(Color.White,graphicalField.Picture.BackColor);
        }

        [Test]
        public void SpecificFieldCreate()
        {
            GraphicalFieldFactory.GraphicalMapSize = new Size(100, 100);
            Field field = new Water();
            field.Position = new Position() { X = 0.5, Y = 0.5 };
            field.Size = new Classes.Format.Size() {Height = 0.1, Width = 0.1};

            GraphicalField graphicalField = GraphicalFieldFactory.CreateGraphicalField(field);

            Assert.AreEqual(field.Position, graphicalField.Position);
            Assert.AreEqual(field.Size, graphicalField.Size);
            Assert.AreEqual(new Point(50, 50), graphicalField.Picture.Location);
            Assert.AreEqual(new Size(10, 10), graphicalField.Picture.Size);
            Assert.AreEqual(Color.Blue, graphicalField.Picture.BackColor);
        }

        public void SizeAndPositionConversion()
        {
            
        }
    }
}
