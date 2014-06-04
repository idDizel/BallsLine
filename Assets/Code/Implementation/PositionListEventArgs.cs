using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Code.Implementation
{
    public class PositionListEventArgs: EventArgs
    {
        public PositionListEventArgs(List<Position> positions)
        {
            this.Positions = positions;
        }
        public List<Position> Positions { get; set; }
    }
}
