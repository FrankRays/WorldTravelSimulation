using System.Collections.Generic;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.GUI;
using Size = WorldTravelSimulation.Classes.Format.Size;

namespace WorldTravelSimulationGUI
{
    public partial class MainWindow : Form
    {
        private Map map;

        public MainWindow()
        {
            map = new Map
            {
                Size = new Size() {Height = 1, Width = 1}
            };
            ClientSize = new System.Drawing.Size(1024,512);

            map.GenerateMap(300,600);
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
