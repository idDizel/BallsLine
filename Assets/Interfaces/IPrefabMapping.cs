using BallsLine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace BallsLine.Interfaces
{
    public interface IPrefabMapping
    {
        void MappPrefab<PrefabType,BallType>();
    }
}
