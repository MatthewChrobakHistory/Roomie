using Roomie.Data;
using Roomie.Graphics.Sfml.Scenes;
using SFML.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Roomie.Graphics.Sfml
{
    public class Sfml : IGraphics
    {
        public static RenderWindow BackBuffer { private set; get; }
        public static SceneSystem Scene { private set; get; }
        public static Font GameFont { private set; get; }
        private List<GraphicalSurface>[] _surface;
        private Color _bgColor;
        private IInput _input;

        #region Core SFML
        public void Initialize() {
            LoadSurfaces();

            LoadFont();

            BackBuffer = new RenderWindow(new BackBuffer(Program.Window,
                Program.Window.getTrueWidth(),
                Program.Window.getTrueHeight(),
                0, 0).GetHandle());
            _bgColor = new Color(0, 0, 0);

            _input = new Input();
            BackBuffer.MouseButtonPressed += _input.MouseDown;
            BackBuffer.MouseButtonReleased += _input.MouseUp;
            BackBuffer.MouseMoved += _input.MouseMove;
            BackBuffer.KeyPressed += _input.KeyPress;
            BackBuffer.KeyReleased += _input.KeyRelease;
            BackBuffer.SetKeyRepeatEnabled(true);

            Scene = new SceneSystem();
        }
        public void Destroy() {

        }
        public void Draw() {
            BackBuffer.DispatchEvents();
            BackBuffer.Clear(_bgColor);
            DrawGame();
            Scene.Draw();
            BackBuffer.Display();
        }

        private void LoadSurfaces() {
            _surface = new List<GraphicalSurface>[(int)SurfaceType.Length];

            for (int i = 0; i < (int)SurfaceType.Length; i++) {
                _surface[i] = new List<GraphicalSurface>();
            }

            // Example Usage:

            foreach (string file in Directory.GetFiles(GraphicsManager.RoomsPath, "*.png")) {
                _surface[(int)SurfaceType.Rooms].Add(new GraphicalSurface(file));
            }

            // foreach (string file in Directory.GetFiles(GraphicsManager.ExamplePath, "*.png")) {
            //     _surface[(int)SurfaceType.Sprite].Add(new GraphicalSurface(file));
            // }
        }
        private void LoadFont() {

        }

        public GraphicalSurface GetSurface(string tagName, SurfaceType type) {
            for (int i = 0; i < _surface[(int)type].Count; i++) {
                if (_surface[(int)type][i].tag.ToLower() == tagName.ToLower()) {
                    return _surface[(int)type][i];
                }
            }
            return null;
        }
        public int GetSurfaceIndex(string tagName, SurfaceType type) {
            for (int i = 0; i < _surface[(int)type].Count; i++) {
                if (_surface[(int)type][i].tag.ToLower() == tagName.ToLower()) {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        int nextphoto = 0;
        int fadetick = 0;
        bool fullphoto = false;
        bool fadephoto = false;
        bool jump = false;
        private Color color = new Color(0, 0, 0);

        private void DrawGame() {
            var player = DataManager.Player;

            
            int tick = System.Environment.TickCount;

            if (nextphoto < tick && player.ProcessPhoto) {
                player.ProcessPhoto = false;
                jump = false;
            }

            if (player.TakePhoto) {
                player.ProcessPhoto = true;
                int rng = Utilities.RNG.Get(0, 100);
                if (rng < 30) {
                    jump = true;
                    Audio.AudioManager.PlaySound("clang.flac");
                }
                fullphoto = true;
                color = new Color(255, 255, 255, 150);
                fadetick = tick + 125;
                nextphoto = tick + 3500;
                player.TakePhoto = false;
            }

            if (player.ProcessPhoto) {
                if (fadetick < tick) {
                    fullphoto = false;
                    fadephoto = true;
                }
            }

            if (fadephoto) {
                byte val = 7;
                if (color.A - val < 0) {
                    val = 0;
                } else {
                    val = (byte)(color.A - val);
                }
                color = new Color(color.R, color.G, color.B, val);

                if (color.A == 0) {
                    fadephoto = false;
                }
            }

            if (fullphoto) {
                string name = player.X + "," + player.Z + " " + player.getDir();
                if (jump) {
                    name = "jump";
                }
                var surf = GetSurface(name, SurfaceType.Rooms);
                if (surf != null) {
                    var sprite = surf.sprite;
                    sprite.Scale = new SFML.System.Vector2f(Program.Window.getTrueWidth() / (float)sprite.Texture.Size.X, Program.Window.getTrueHeight() / (float)sprite.Texture.Size.Y);
                    BackBuffer.Draw(sprite);
                }
            }

            if (fadephoto) {
                var surf = GetSurface(player.X + "," + player.Z + " " + player.getDir() + "F", SurfaceType.Rooms);
                if (surf != null) {
                    var sprite = surf.sprite;
                    sprite.Color = color;
                    sprite.Scale = new SFML.System.Vector2f(Program.Window.getTrueWidth() / (float)sprite.Texture.Size.X, Program.Window.getTrueHeight() / (float)sprite.Texture.Size.Y);
                    BackBuffer.Draw(sprite);
                }
            }
        }
    }
}
