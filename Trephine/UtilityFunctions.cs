﻿namespace Trephine
{
    internal static class UtilityFunctions
    {
        /// <summary>
        ///     find direction to turn given where bot is and where it will turn
        /// </summary>
        /// <param name="original"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public static int GetDirection(int original, int turn)
        {
            var diff = turn - original;

            if (diff < -180)
                diff += 360;
            if (diff > 180)
                diff -= 360;

            if (diff < 0)
                return -1;

            return 1;
        }

        //
        /// <summary>
        /// </summary>
        /// <param name="original"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public static int GetDestination(int original, int turn)
        {
            var val = original + turn;

            if (val > 360)
                val -= 360;
            else if (val < 0)
                val += 360;

            return val;
        }

        public static int GetDistance(int orgin, int currentPos)
        {
            var val = orgin - currentPos;
            var dir = GetDirection(orgin, currentPos);
            if (dir == -1)
                val *= -1;

            if (val < 0)
                val += 360;
            else if (val > 360)
                val -= 360;
            val *= dir;
            return val;
        }
    }
}