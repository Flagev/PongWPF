using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{

    class Pallet
    {
        double speed = 40;
        public double width = 20;
        public double height = 100;
        public double x = 0;
        public double y = 0;
        public double ballDistance=0;
        public static double maxY=500;
        public Pallet(double startPosX)
        {
            x = startPosX;
            y = 0;
        }
        public Pallet(double startPosX, double startPosY)
        {
            x = startPosX;
            y = startPosY;
        }
        public double MoveUp()
        {
            y += speed;
            //Ograniczenie wymiarow planszy
            if ((y + height) > maxY)
            {
                y = maxY - height;
            }
            return y;
        }
        public double MoveDown()
        {
            y -= speed;
            //Ograniczenie wymiarow planszy
            if ((y - height) < -1 * maxY)
            {
                y = -1 * maxY + height;
            }
            return y;
        }
    }

}
