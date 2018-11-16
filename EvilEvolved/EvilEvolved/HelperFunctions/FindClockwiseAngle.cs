using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// Finds the rotation angle in degrees that the arrow image needs to be rotated by
    /// </summary>
    public class FindClockwiseAngle
    {
        public FindClockwiseAngle(float directionX, float directionY)
        {
            DirectionX = directionX;
            DirectionY = directionY;
        }
        public float FindAngle()
        {
            if (0.0f == DirectionX)
            {
                if(DirectionY < 0)
                {
                    return 0.0f;
                }
                else
                {
                    return 180.0f;
                }
            }
            else if(DirectionX > 0.0f)
            {
                float angle = 90.0f + (float)Math.Atan2((double)DirectionY, (double)DirectionX);
                return angle;
            }
            else
            {
                float angle = 270.0f + (float)Math.Atan2((double)DirectionY, (double)DirectionX);
                return angle;
            }

        }

        float DirectionX;
        float DirectionY;
}
}
