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
        GameObject SelectedBall { get; set; }
        IEnumerable<BallEntity> GenerateBalls(int ballsCount);
        IEnumerable<Position> ValidateLines(BallEntity ball);
        void ChangePosition(int newX, int newY);
    }
}
