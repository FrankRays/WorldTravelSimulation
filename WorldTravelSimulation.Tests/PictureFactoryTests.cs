using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.GUI;
using Size = System.Drawing.Size;

namespace WorldTravelSimulation.Tests
{
    public class PictureFactoryTests
    {
        [Test]
        public void GeneralFieldPictureCreate()
        {
            PictureFactory.FormSize = new Size(100,100);
            Field field = new Field()
            {
                Position = new Position() {X = 0.5, Y = 0.5},
                Size = new Classes.Format.Size() {Height = 0.1, Width = 0.1}
            };

            PictureBox picture = PictureFactory.CreatePicture(field);

            Assert.AreEqual(new Point(50,50), picture.Location);
            Assert.AreEqual(new Size(10,10), picture.Size);
            Assert.AreEqual(Color.White,picture.BackColor);
        }

        [Test]
        public void SpecificFieldPictureCreate()
        {
            PictureFactory.FormSize = new Size(100, 100);
            Field field = new Water();
            field.Position = new Position() { X = 0.5, Y = 0.5 };
            field.Size = new Classes.Format.Size() {Height = 0.1, Width = 0.1};

            PictureBox picture = PictureFactory.CreatePicture(field);

            Assert.AreEqual(new Point(50, 50), picture.Location);
            Assert.AreEqual(new Size(10, 10), picture.Size);
            Assert.AreEqual(Color.Blue, picture.BackColor);
        }

        public void SizeAndPositionConversion()
        {
            
        }
    }
}
