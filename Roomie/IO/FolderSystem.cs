﻿using System.IO;
using Roomie.Graphics;

namespace Roomie.IO
{
    public static class FolderSystem
    {
        public static void Check() {
            // Create an array of directories to check.
            string[] folders = {
                                 Program.StartupPath + "data\\",
                                 Program.StartupPath + "data\\fonts\\",

                                 GraphicsManager.SurfacePath,
                                 GraphicsManager.GuiPath
                             };

            // Loop through all the directories in the array, and see
            // if they exist. If not, create the directory.
            foreach (string folder in folders) {
                if (!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }
            }
        }

        public static bool FileExists(string file) {
            if (File.Exists(file)) {
                return true;
            } else {
                return false;
            }
        }
    }
}
