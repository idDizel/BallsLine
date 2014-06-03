using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BallsLine.Enums;

namespace BallsLine.Entities
{
    public class BallEntity
    {
        public BallEntity(Position position, BallType ballType)
        {
            this.BallPosition = position;
            this.BallType = ballType;
        }
        public Position BallPosition { get; set; }
        public BallType BallType { get; set; }
    }
}
