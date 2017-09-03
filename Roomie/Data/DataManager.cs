using System.IO;
using Roomie.IO;
using System.Collections.Generic;
using Roomie;
using Roomie.Data.Models;

namespace Roomie.Data
{
    public static class DataManager
    {
        // Data directories
        private static string _data = Program.StartupPath + "data\\";
        public static Player Player = new Player();

        public static void Load() {

        }

        public static void Save() {

        }
    }
}
