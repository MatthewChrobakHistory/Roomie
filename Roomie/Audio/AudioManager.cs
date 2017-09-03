namespace Roomie.Audio
{
    public static class AudioManager
    {
        // Public directories.
        public static string AudioDir = Program.StartupPath + "audio\\";
        public static string MusicDir = AudioDir + "music\\";
        public static string SoundDir = AudioDir + "sounds\\";


        private static IPlayer _player;

        public static void Initialize() {
            _player = new Sfml.AudioPlayer();
            _player.Initialize();
        }

        public static void PlayMusic(string name, bool loop = false, float volume = 100.0f) {
            _player.PlayMusic(name, loop, volume);
        }

        public static void PlaySound(string name, bool loop = false, float volume = 100.0f) {
            _player.PlaySound(name, loop, volume);
        }

        public static void StopMusics(string name) {
            _player.StopMusics(name);
        }

        public static void StopSounds(string name) {
            _player.StopSounds(name);
        }

        public static void StopAllMusics() {
            _player.StopAllMusics();
        }

        public static void StopAllSounds() {
            _player.StopAllSounds();
        }
    }
}
