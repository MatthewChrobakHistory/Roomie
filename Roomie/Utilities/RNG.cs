using System;

namespace Roomie.Utilities
{
    public static class RNG
    {
        private static Random _rand = new Random();

        public static int Get(int low, int high) {
            return _rand.Next(low, high + 1);
        }
    }
}
