using System;
using System.Drawing;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using Size = System.Drawing.Size;

namespace WorldTravelSimulation.Classes.GUI
{
    public static class GraphicalFieldFactory
    {
        public static Size GraphicalMapSize { get; set; }

        public static GraphicalField CreateGraphicalField(Field field)
        {
            GraphicalField graphicalField = new GraphicalField
            {
                Position = field.Position,
                Size = field.Size,
                Picture = new PictureBox()
                {
                    BackColor = GetFieldColor(field),
                    Location = GetPicturePointFromFieldPosition(field.Position),
                    Size = GetPictureSizeFromFieldSize(field.Size)
                }
            };

            return graphicalField;
        }

        public static Color GetFieldColor(Field field)
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


        public static Point GetPicturePointFromFieldPosition(Position position)
        {
            int x = (int)Math.Round(position.X * GraphicalMapSize.Width, 0);
            int y = (int)Math.Round(position.Y * GraphicalMapSize.Height, 0);

            return new Point() { X = x, Y = y };
        }

        public static Size GetPictureSizeFromFieldSize(Format.Size size)
        {
            int width = (int)Math.Round(size.Width * GraphicalMapSize.Width, 0);
            int height = (int)Math.Round(size.Height * GraphicalMapSize.Height, 0);

            return new Size() { Height = height, Width = width };
        }
    }
}
