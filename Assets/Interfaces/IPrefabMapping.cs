using BallsLine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace BallsLine.Interfaces
{
    public interface IPrefabMapping
    {
        void MapPrefab(GameObject gameObject, BallType type);

    }
}
