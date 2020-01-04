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
        double speed;
        double radius =20;
        public double x=0;
        public double y =0;
        public double xSpeed =0;
        public double ySpeed = 0;
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
            x += (xSpeed / 100.0);
            y += (ySpeed / 100.0);
        }
        public double CheckPalletTouched(int palNumber, double palHeight, double palX, double palY)
        {
            //odleglosc okregu od wierzcholka
            if (Math.Sqrt(Math.Pow(((palY + palHeight / 2) - y),2) + Math.Pow(((palX) - x),2)) < radius)
            {
                ySpeed = 10.0;
            }
            return Math.Sqrt(Math.Pow(((palY + palHeight / 2) - y), 2) + Math.Pow(((palX) - x), 2));
            //odleglosc okregu od prostej
        }
    }
}
