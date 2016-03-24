using System.Drawing;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Area;
using WorldTravelSimulation.Classes.Generators;

namespace WorldTravelSimulationGUI
{
    public partial class MainWindow : Form
    {
        private readonly Map _map;

        public MainWindow()
        {
            ClientSize = new Size(311,111);

            _map = new Map(311, 111);            
            _map.GenerateMap();

            MapDraw();            
            
            InitializeComponent();
        }

        public void MapDraw()
        {
            PictureBox mapPictureBox = new PictureBox
            {
                Image = BitmapGenerator.GenerateFieldMapBitmap(_map),
                Location = new Point(0, 0),
                Size = ClientSize
            };
            Controls.Add(mapPictureBox);
        }        
    }
}
