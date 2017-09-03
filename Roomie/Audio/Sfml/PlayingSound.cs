using SFML.Audio;

namespace Roomie.Audio.Sfml
{
    public class PlayingSound
    {
        public string Tag { get; set; }
        public Sound Sound { get; private set; }
        public bool Destroy { get; set; }

        public PlayingSound(string tag, Sound sound, bool loop, float volume) {
            this.Sound = sound;
            this.Sound.Loop = loop;
            this.Sound.Volume = volume;
            this.Sound.Play();
            this.Tag = tag;
        }

        public void Stop() {
            Sound.Stop();
            Sound.Dispose();
        }

        public bool Finished() {
            return (Sound.Status == SoundStatus.Stopped || Destroy) ? true : false;
        }
    }
}
