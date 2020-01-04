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
        public static double maxY = 500;
        public static double maxX = 500;
        bool moving = false;
        public Ball(double radius, double speed)
        {
            rnd = new Random();
            this.speed = speed;
            this.radius = radius;
        }
        public void Init()
        {
            x = 0;
            y = 0;
            ySpeed = 0;
            xSpeed = 0;
        }
        public void Start()
        {
            x = 0;
            y = 0;
            ySpeed = 0;
            if (rnd.Next(2).Equals(1))
            {
                xSpeed = 800;
            }
            else
            {
                xSpeed = -800; ; ;
            }
            moving = true;
        }
        public void CalcPos()
        {
            x += (xSpeed / 100.0);
            y += (ySpeed / 100.0);
        }
        public void CheckPalletTouched(Pallet pallet)
        {
            double pWidth=0;
            double pY = 0;
            pY = y;
            if ((pallet.y - y) > pallet.height)
            {
                pY = pallet.y - pallet.height;
            }
            if ((pallet.y - y) < -pallet.height)
            {
                pY = pallet.y + pallet.height;
            }

            if (pallet.x > 0)
            {
                pWidth = -pallet.width;
            }
            else
            {
                pWidth = pallet.width;
            }
            double distance = Math.Sqrt(Math.Pow(((pY) - y), 2) + Math.Pow(((pallet.x+pWidth) - x), 2));
            pallet.ballDistance = distance;
            //Odbicie od paletki
            if (distance < radius)
            {
                xSpeed = -xSpeed;
                ySpeed = 10*(y- pallet.y);
            }
            
            //odleglosc okregu od prostej
        }
        public void CheckBoundary(Match match)
        {
            if (y+radius >= maxY)
            {
                y = maxY-radius;
                ySpeed = -ySpeed;
            }
            if (y -radius<= -maxY)
            {
                y = -maxY+radius;
                ySpeed = -ySpeed;
            }
            if (x + radius >= maxX)
            {
                match.leftScore++;
                Init();
                
            }
            if (x - radius <= -maxX)
            {
                match.rightScore++;
                Init();
                
            }
        }
    }
}
