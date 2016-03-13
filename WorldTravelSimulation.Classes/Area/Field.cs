using WorldTravelSimulation.Classes.Format;

namespace WorldTravelSimulation.Classes.Area
{
    public class Field
    {
        public Position Position { get; set; }
        public Size Size { get; set; }

        public bool DoesFieldCoverPosition(Position position)
        {
            return position.X >= Position.X &&
                   position.Y >= Position.Y &&
                   position.X <= Position.X + Size.Width &&
                   position.Y <= Position.Y + Size.Height;
        }
    }
}
