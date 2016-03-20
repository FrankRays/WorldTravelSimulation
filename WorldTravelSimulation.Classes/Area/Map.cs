using System.Collections.Generic;
using System.Linq;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.Generators;

namespace WorldTravelSimulation.Classes.Area
{
    public class Map
    {
        private IList<Field> Fields;

        public IMapGenerator MapGenerator { get; set; }
        public Size Size { get; set; }

        public Map()
        {
            Fields = new List<Field>();
            MapGenerator = new SimplexNoiseMapGenerator();
            Size = new Size() {Height = 1, Width = 1};
        }

        public void AddField(Field field)
        {
            Fields.Add(field);    
        }

        public void AddFields(IList<Field> fields)
        {
            foreach (var f in fields)
            {
                AddField(f);
            }
        }

        public IList<Field> GetAllFields()
        {
            return Fields;
        } 

        public Field GetFieldByPosition(Position position)
        {
            var field = Fields.FirstOrDefault(x => x.DoesFieldCoverPosition(position));
            return field;
        }

        public void GenerateMap(Size fieldSize)
        {
            Fields = MapGenerator.GenerateMap(Size,fieldSize);
        }

        public void GenerateMap(int fieldsHorizontal, int fieldsVertical)
        {
            Fields = MapGenerator.GenerateMap(Size, fieldsHorizontal, fieldsVertical);
        }
    }
}
