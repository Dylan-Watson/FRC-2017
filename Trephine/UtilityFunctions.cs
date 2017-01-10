using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trephine
{
    static class UtilityFunctions
    {
        const int ANGLE_CHECK_RANGE = 3;

        
         /// <summary>
         /// find direction to turn given where bot is and where it will turn
         /// </summary>
         /// <param name="original"></param>
         /// <param name="turn"></param>
         /// <returns></returns>
        public static int GetDirection(int original, int turn)
        {
            int diff = turn - original;

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
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newA"></param>
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
            int dir = GetDirection(orgin, currentPos);
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
