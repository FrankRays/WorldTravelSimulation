using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.Generators;
using WorldTravelSimulation.Classes.GUI;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulationGUI
{
    public partial class MainWindow : Form
    {
        private Map map;

        public MainWindow()
        {
            map = new Map();
            map.Size = new Size() {Height = 1, Width = 1};
            ClientSize = new System.Drawing.Size(512,512);

            MapGenerator generator = new MapGenerator
            {
                MapHeight = ClientSize.Height,
                MapWidth = ClientSize.Width,
                StartPosition = new Position() {X = 0, Y = 0}
            };
            generator.GenerateMap();

            map.AddFields(generator.Map);
            MapDraw();            
            
            InitializeComponent();
        }

        public void MapDraw()
        {
            IList<Field> fields = map.GetAllFields();
            foreach (var f in fields)
            {
                DrawField(f);
            }
        }

        public void DrawField(Field field)
        {
            PictureFactory.FormSize = ClientSize;
            PictureBox fieldPicture = PictureFactory.CreatePicture(field);
            Controls.Add(fieldPicture);
        }
    }
}
