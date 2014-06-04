using BallsLine.Enums;
using UnityEngine;

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
