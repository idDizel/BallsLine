using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BallsLine.Interfaces
{
    public interface IElementNotifier
    {
        Position Position { get; set; }
        void Selected();
        void Unselected();
        void PositionChanged();
        void Removed();
    }
}
