using System.Collections.Generic;
using System.Drawing;
using WorldTravelSimulation.Classes.Area;

namespace WorldTravelSimulation.Classes.GUI
{
    public class BitmapGenerator
    {
        public static Bitmap GenerateFieldMapBitmap(Map fieldMap)
        {
            int fieldsHorizontal = fieldMap.FieldsHorizontal;
            int fieldsVertical = fieldMap.FieldsVertical;

            Bitmap bitmap = new Bitmap(fieldsHorizontal, fieldsVertical);

            IList<Field> fields = fieldMap.GetAllFields();

            foreach (var f in fields)
            {
                Color color = GetColor(f);

                int x = (int)(f.Position.X * fieldsHorizontal);
                int y = (int)(f.Position.Y * fieldsVertical);

                bitmap.SetPixel(x, y, color);
            }

            return bitmap;
        }

        private static Color GetColor(Field field)
        {
            if (field.GetType() == typeof(Water))
                return Color.Blue;
            if (field.GetType() == typeof(Ground))
                return Color.Green;
            return Color.Black;
        }
    }
}
