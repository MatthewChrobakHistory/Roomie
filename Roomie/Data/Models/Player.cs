using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roomie.Data.Models
{
    public enum Dirs
    {
        North,
        East,
        South,
        West
    }

    public class Player : Location
    {
        public Dirs Dir;
        public bool TakePhoto = false;
        public bool FlashRecharging = false;
        public bool ProcessPhoto = false;

        public Player() {
            this.X = 3;
            this.Z = 6;
        }

        public char getDir() {
            switch (this.Dir) {
                case Dirs.North:
                    return 'N';
                case Dirs.South:
                    return 'S';
                case Dirs.East:
                    return 'E';
                case Dirs.West:
                    return 'W';
                default:
                    return 'N';
            }
        }

        public void Rotate(bool right) {
            switch (this.Dir) {
                case Dirs.North:
                    this.Dir = (right) ? Dirs.East : Dirs.West;
                    break;
                case Dirs.South:
                    this.Dir = (right) ? Dirs.West : Dirs.East;
                    break;
                case Dirs.East:
                    this.Dir = (right) ? Dirs.South : Dirs.North;
                    break;
                case Dirs.West:
                    this.Dir = (right) ? Dirs.North : Dirs.South;
                    break;
            }
        }

        public void Move(bool forward) {
            if (this.Dir == Dirs.North) {
                this.Z += (forward) ? -1 : 1;
            }
            if (this.Dir == Dirs.South) {
                this.Z += (forward) ? 1 : -1;
            }
            if (this.Dir == Dirs.East) {
                this.X += (forward) ? 1 : -1; 
            }
            if (this.Dir == Dirs.West) {
                this.X += (forward) ? -1 : 1;
            }
            Audio.AudioManager.PlaySound("step" + Utilities.RNG.Get(0, 8) + ".flac");
        }
    }
}
