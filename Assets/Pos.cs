using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    struct Pos
    {
        int x;
        int y;

        public int X 
        { 
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
