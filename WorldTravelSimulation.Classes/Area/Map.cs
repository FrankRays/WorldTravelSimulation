using System.Collections.Generic;
using WorldTravelSimulation.Classes.Generators;
using WorldTravelSimulation.Classes.Generators.SimplexNoise;

namespace WorldTravelSimulation.Classes.Area
{
    public class Map
    {
        private IList<Field> _fields;
        public int FieldsHorizontal { get; }
        public int FieldsVertical { get; }

        public IMapGenerator MapGenerator { get; set; }        

        public Map(int fieldsHorizontal, int fieldsVertical)
        {
            FieldsHorizontal = fieldsHorizontal;
            FieldsVertical = fieldsVertical;
            _fields = new List<Field>();            

            MapGenerator = new SimplexNoiseMapGenerator();
        }

        public IList<Field> GetAllFields()
        {
            return _fields;
        } 
       
        public void GenerateMap()
        {
            _fields = MapGenerator.GenerateMap(FieldsHorizontal, FieldsVertical);
        }
    }
}
