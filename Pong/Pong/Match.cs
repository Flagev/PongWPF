using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    
    class Match
    {
        public  int leftScore;
        public int rightScore;
        int pointsToVictory;
        public Match(int pointsToVictory)
        {
            leftScore = 0;
            rightScore = 0;
            this.pointsToVictory = pointsToVictory;
        }
        public (bool, bool) CheckVictory()
        {
            if (leftScore>=pointsToVictory)
            {
                leftScore = 0;
                rightScore = 0;
                return (true, false);
            }
            if (rightScore >= pointsToVictory)
            {
                leftScore = 0;
                rightScore = 0;
                return (false, true);
            }
            return (false,false);
        }
        

    }
}
