using BallsLine.Enums;
using UnityEngine;

namespace BallsLine.Interfaces
{
    public interface IPrefabMapping
    {
        void MapPrefab(IElementNotifier gameObject, BallType type);

    }
}
