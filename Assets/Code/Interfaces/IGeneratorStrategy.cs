using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Interfaces
{
    public interface IGeneratorStrategy
    {
        void Generate(Dictionary<Position, IElementNotifier> levelGrid);
        void Instantiate();
    }
}
