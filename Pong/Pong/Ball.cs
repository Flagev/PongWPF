using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Ball
    {
        Random rnd;
        int speed;
        int radius=20;
        public int x=0;
        public int y=0;
        public int xSpeed=0;
        int ySpeed=0;
        bool moving = false;
        public Ball(int speed)
        {
            rnd = new Random();
            this.speed = speed; ; ;
        }
        public void Start()
        {
            x = 0;
            y = 0;
            if (rnd.Next(2).Equals(1))
            {
                xSpeed = 100;
            }
            else
            {
                xSpeed = -100; ; ;
            }
            moving = true;
        }
        public void CalcPos()
        {
            x += (int)(xSpeed / 100.0);
            y += (int)(ySpeed / 100.0);
        }
    }
}
