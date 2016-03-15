using System.Collections.Generic;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Format;
using WorldTravelSimulation.Classes.GUI;

namespace WorldTravelSimulationGUI
{
    public partial class MainWindow : Form
    {
        private Map map;

        public MainWindow()
        {
            map = new Map();
            map.Size = new Size() {Height = 1, Width = 1};

            Field water = new Water()
            {
                Position = new Position() { X=0.5,Y=0.5},
                Size = new Size() { Height = 0.1,Width = 0.1}
            };

            DrawField(water);

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
            PictureFactory.FormSize = this.Size;
            PictureBox fieldPicture = PictureFactory.CreatePicture(field);
            Controls.Add(fieldPicture);
        }
    }
}
