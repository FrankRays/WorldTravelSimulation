using System;
using System.Drawing;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using Size = System.Drawing.Size;

namespace WorldTravelSimulation.Classes.GUI
{
    public static class PictureFactory
    {
        public static Size FormSize { get; set; }

        public static PictureBox CreatePicture(Field field)
        {
            PictureBox picture = new PictureBox()
            {
                BackColor = GetPictureColor(field),
                Location = GetPicturePointFromFieldPosition(field.Position,field.Size),
                Size = GetPictureSizeFromFieldSize(field.Size)
            };
            
            return picture;
        }

        public static Color GetPictureColor(Field field)
        {
            if (field.GetType() == typeof (Water))
            {
                return Color.Blue;
            }
            if (field.GetType() == typeof (Ground))
            {
                return Color.Green;
            }
            return Color.White;
        }


        public static Point GetPicturePointFromFieldPosition(Position position, Format.Size size)
        {
            int x = (int)Math.Round(position.X * FormSize.Width,0);
            int y = (int)Math.Round(position.Y * FormSize.Height,0);

            y = Math.Abs(y - FormSize.Height);
            y -= GetPictureSizeFromFieldSize(size).Height; 

            return new Point() { X = x, Y = y };
        }

        public static Size GetPictureSizeFromFieldSize(Format.Size size)
        {
            int width = (int)Math.Round(size.Width * FormSize.Width,0);
            int height = (int)Math.Round(size.Height * FormSize.Height,0);

            return new Size() { Height = height, Width = width };
        }
    }
}
