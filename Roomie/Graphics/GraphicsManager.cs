namespace Roomie.Graphics
{
    public enum SurfaceType
    {
        Rooms,
        Length
    }

    public class GraphicsManager
    {
        // Surface paths
        public static string SurfacePath = Program.StartupPath + "data\\surfaces\\";
        public static string GuiPath = SurfacePath + "Gui\\";
        public static string RoomsPath = SurfacePath + "Rooms\\";

        private static IGraphics _graphics;

        public static void Initialize() {
            _graphics = new Sfml.Sfml();
            _graphics.Initialize();
        }

        public static void Destroy() {
            _graphics.Destroy();
        }

        public static void Draw() {
            _graphics.Draw();
        }
    }
}
