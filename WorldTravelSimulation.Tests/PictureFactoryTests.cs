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

            Assert.AreEqual(Color.Blue, picture.BackColor);
        }

        /*[TestCase(0,0,0,11)]
        [TestCase(1,1,11,11)]
        [TestCase(0.09,0.09,0,0)]
        [TestCase(0.091,0.091,1,1)]
        [TestCase(0.181817,0.181817,1,1)]
        [TestCase(0.1819,0.1819,2,2)]
        public void SizeAndPositionConversion(double fieldX, double fieldY, int pictureX, int pictureY)
        {
            PictureFactory.FormSize = new Size() {Height = 11,Width = 11};
            Position position = new Position() {X = fieldX, Y = fieldY};
            Point point = PictureFactory.GetPicturePointFromFieldPosition(position);
            Assert.AreEqual(pictureX,point.X);
            Assert.AreEqual(pictureY,point.Y);
        }*/
    }
}
