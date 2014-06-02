using System;
using System.Linq;

namespace GameLevel
{
    public class Level
    {
        public byte[,] StateArray = new byte[7,7];

        public int FreeCells
        {
            get
            {
                int count = 0;
                foreach(var a in StateArray)
                {
                    if (a == 0) count++;
                }
                return count;
            }
        }

        public void Generate()
        {
            Random random = new Random();
            
            int count = 0;
            bool finished = false;

            do
            {
                int x = random.Next(0, 7);
                int y = random.Next(0, 7);
                if(StateArray[x,y]!=1)
                {
                    StateArray[x, y] = 1;
                    count++;
                }
                if (count == 3) finished = true;
            }
            while (!finished);
        }
    }
}
