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
        void GenerateElements(int ballsCount);
        //bool ValidateOfAxis(Position position);
        //void ChangePosition(Position newPosition, Position prevPosition);
    }
}
