using SFML.Audio;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Roomie.Audio.Sfml
{
    public class AudioPlayer : IPlayer
    {
        private List<PlayingMusic> _music = new List<PlayingMusic>();
        private List<PlayingSound> _sounds = new List<PlayingSound>();
        private Thread _collecter;

        public void Initialize() {

        }

        private void CreateAudioChecker() {
            if (_collecter == null || _collecter.ThreadState != ThreadState.Running) {
                _collecter = new Thread(AudioChecker);
                _collecter.Start();
            }
        }

        private void AudioChecker() {
            bool done = false;

            do {
                for (int i = _music.Count - 1; i >= 0; i--) {
                    if (_music[i].Finished()) {
                        _music[i].Stop();
                        _music.RemoveAt(i);
                    }
                }
                for (int i = _sounds.Count - 1; i >= 0; i--) {
                    if (_sounds[i] != null) {
                        if (_sounds[i].Finished()) {
                            _sounds[i].Stop();
                            _sounds.RemoveAt(i);
                        }
                    }
                }

                if (_music.Count == 0 && _sounds.Count == 0) {
                    done = true;
                }
            } while (!done);
        }

        public void PlayMusic(string name, bool loop = false, float volume = 100.0f) {
            if (AudioFileExists(AudioManager.MusicDir + name)) {
                _music.Add(new PlayingMusic(name, new Music(AudioManager.MusicDir + name), loop, volume));
                CreateAudioChecker();
            }
        }
        public void PlaySound(string name, bool loop = false, float volume = 100.0f) {
            if (AudioFileExists(AudioManager.SoundDir + name)) {
                _sounds.Add(new PlayingSound(name, new Sound(new SoundBuffer(AudioManager.SoundDir + name)), loop, volume));
                CreateAudioChecker();
            }
        }
        public void StopMusics(string name) {
            for (int i = _music.Count - 1; i >= 0; i--) {
                if (_music[i].Tag == name) {
                    _music[i].Destroy = true;
                }
            }
        }
        public void StopSounds(string name) {
            for (int i = _sounds.Count - 1; i >= 0; i--) {
                if (_sounds[i].Tag == name) {
                    _sounds[i].Destroy = true;
                }
            }
        }
        public void StopAllMusics() {
            for (int i = _music.Count - 1; i >= 0; i--) {
                _music[i].Destroy = true;
            }
        }
        public void StopAllSounds() {
            for (int i = _sounds.Count - 1; i >= 0; i--) {
                _sounds[i].Destroy = true;
            }
        }

        private bool AudioFileExists(string path) {
            if (File.Exists(path)) {
                if (path.EndsWith(".wav") || path.EndsWith(".ogg") || path.EndsWith(".flac")) {
                    return true;
                }
            }
            return false;
        }
    }
}
