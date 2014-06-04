using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BallsLine.Interfaces
{
    public interface ILevelCore
    {
        IEnumerable<BallEntity> GenerateBalls(int ballsCount);
        IEnumerable<Position> ValidateLines(BallEntity ball);
        void ChangePosition(Position newPosition, Position prevPosition);
    }
}
