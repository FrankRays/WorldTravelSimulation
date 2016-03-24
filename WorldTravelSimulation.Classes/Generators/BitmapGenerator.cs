using System;
using System.Collections.Generic;
using System.Drawing;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.GUI;

namespace WorldTravelSimulation.Classes.Generators
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

                int x = (int)Math.Round(f.Position.X * fieldsHorizontal,0);
                int y = (int)Math.Round(f.Position.Y * fieldsVertical,0);                

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
