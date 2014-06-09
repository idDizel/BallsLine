using BallsLine.Enums;
using BallsLine.Interfaces;
using UnityEngine;

namespace BallsLine.Entities
{
    public class ElementEntity
    {
        public ElementEntity(ElementType ballType, IElementNotifier instance)
        {
            this.BallType = ballType;
            this.Instance = instance;
        }
        public ElementType BallType { get; set; }
        public IElementNotifier Instance { get; set; }
    }
}
