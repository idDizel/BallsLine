using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Code.Implementation
{
    public class PositionEventArgs: EventArgs
    {
        public PositionEventArgs(Position position)
        {
            this.position = position;
        }
        public Position position { get; set; }
    }
}
