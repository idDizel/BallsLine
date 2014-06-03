using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Implementation
{
    public class LevelState
    {
        private static readonly LevelState instance = new LevelState();
        private LevelCore core;

        static LevelState()
        {

        }

        private LevelState()
        {
            this.core = new LevelCore(7);
        }

        public static LevelState Instance
        {
            get
            {
                return instance;
            }
        }

        public void GenerateBalls(Action<IEnumerable<BallEntity>> uiGenerator)
        {
            uiGenerator.Invoke(core.Generate(3));
        }
    }
}
