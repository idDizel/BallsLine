using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Code.Interfaces
{
    public interface IPosition<T> where T: Position
    {
        T Position { get; set; }
    }
}
