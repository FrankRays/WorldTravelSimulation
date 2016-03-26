using System.Drawing;
using System.Windows.Forms;
using WorldTravelSimulation.Classes.Generators;
using WorldTravelSimulation.Classes.Generators.Bitmap;
using WorldTravelSimulation.Classes.Generators.SimplexNoiseTerrainGenerator;
using WorldTravelSimulation.Classes.World;

namespace WorldTravelSimulationGUI
{
    public partial class MainWindow : Form
    {        
        public MainWindow()
        {
            ClientSize = new Size(600,500);

            World.Instance.WorldAreaSize = new WorldTravelSimulation.Classes.Format.Size(600,500);
            World.Instance.TerrainGenerator = new SimplexNoiseTerrainGenerator();
            World.Instance.AirPortGenerator = new RandomAirPortGenerator();

            World.Instance.GenerateTerrain();
            World.Instance.GenerateAirPorts(100);                       

            DrawTerrain();
            DrawStaticSimulationObjects();          
            
            InitializeComponent();
        }

        private void DrawStaticSimulationObjects()
        {
            foreach (var s in World.Instance.StaticSimulationObjects)
            {
                PictureBox objectPictureBox = new PictureBox()
                {
                    BackColor = Color.Crimson,
                    Location = new Point(s.Position.X,s.Position.Y),
                    Size = new Size(s.Size.Width,s.Size.Height)
                };                
                Controls.Add(objectPictureBox);
                objectPictureBox.BringToFront();
            }
        }

        private void DrawTerrain()
        {
            Bitmap terrainBitmap = TerrainBitmapGenerator.GenerateAndGetTerrainBitmap(World.Instance.Terrain, World.Instance.WorldAreaSize);
            PictureBox terrainPictureBox = new PictureBox
            {
                Image = terrainBitmap,
                Location = new Point(0, 0),
                Size = ClientSize
            };
            Controls.Add(terrainPictureBox);
        }        
    }
}
