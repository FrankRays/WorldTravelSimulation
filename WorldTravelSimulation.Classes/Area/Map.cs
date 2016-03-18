using System.Collections.Generic;
using System.Linq;
using WorldTravelSimulation.Classes.Format;

namespace WorldTravelSimulation.Classes.Area
{
    public class Map
    {
        private IList<Field> Fields;
        public Size Size { get; set; }

        public Map()
        {
            Fields = new List<Field>();
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
    }
}
