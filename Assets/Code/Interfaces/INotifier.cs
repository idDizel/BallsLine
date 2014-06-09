using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Interfaces
{
    public interface INotifier
    {
        void Selected();
        void Unselected();
        void PositionChanged();
        void Removed();
        void Added();
    }
}
