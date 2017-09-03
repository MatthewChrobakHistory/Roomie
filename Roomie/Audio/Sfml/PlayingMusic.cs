using SFML.Audio;

namespace Roomie.Audio.Sfml
{
    public class PlayingMusic
    {
        public string Tag { get; set; }
        public Music Music { get; private set; }
        public bool Destroy { get; set; }

        public PlayingMusic(string tag, Music music, bool loop, float volume) {
            this.Music = music;
            this.Music.Loop = loop;
            this.Music.Volume = volume;
            this.Music.Play();
            this.Tag = tag;
        }

        public void Stop() {
            Music.Stop();
            Music.Dispose();
        }

        public bool Finished() {
            return (Music.Status == SoundStatus.Stopped || Destroy) ? true : false;
        }
    }
}
