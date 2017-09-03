using Roomie.Data;
using Roomie.IO;
using Roomie.Forms;
using Roomie.Graphics;
using Roomie.Audio;
using System.Windows.Forms;

namespace Roomie
{
    public static class Program
    {
        // Public accessors
        public static GameWindow Window;

        public static string StartupPath;
        public static bool Running;

        static void Main(string[] args) {
            // Set up the startup path of the application.
            StartupPath = System.AppDomain.CurrentDomain.BaseDirectory;

            // Check the folders and files in the system.
            FolderSystem.Check();

            // Initialize the audio system.
            AudioManager.Initialize();

            // Initialize the game form.
            Window = new GameWindow(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            // Window = new GameWindow(30 * 32, 20 * 32);

            // Initialize the event-handlers and properties.
            Window.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Window.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Window.WindowState = FormWindowState.Maximized;
            Window.MaximizeBox = false;
            Window.FormClosing += (sender, e) => {
                Running = false;
                e.Cancel = true;
            };

            // Load the game data.
            DataManager.Load();

            // Initialize the game graphics.
            GraphicsManager.Initialize();

            // Start the gameloop.
            GameLoop();
        }

        private static void GameLoop() {
            int tick = 0, tmr16 = 0;

            Running = true;
            Window.Show();
            Window.Focus();

            AudioManager.PlayMusic("ambiance.flac", true);

            while (Running) {
                tick = System.Environment.TickCount;

                if (tmr16 < tick) {
                    GraphicsManager.Draw();
                    tmr16 = tick + 1;
                }

                System.Windows.Forms.Application.DoEvents();
            }

            Destroy();
        }

        private static void Destroy() {
            if (Running) {
                return;
            }
            DataManager.Save();
            System.Environment.Exit(1);
        }
    }
}
