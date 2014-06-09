using BallsLine.Entities;
using BallsLine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BallsLine.Interfaces
{
    public interface IElementNotifier: INotifier
    {
        Position Position { get; set; }
        ElementType Type { get; set; }
    }
}
