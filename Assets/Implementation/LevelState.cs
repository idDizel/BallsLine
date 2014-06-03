using BallsLine.Entities;
using BallsLine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BallsLine.Implementation
{
    public class LevelState
    {
        private static readonly LevelState instance = new LevelState();
        private LevelCore core;
        GameObject selectedBall;

        static LevelState()
        {

        }

        private LevelState()
        {
            
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

        public void GenerateLevel()
        {
            this.core = new LevelCore(7);
        }

        public void MapPrefab(GameObject go, BallType type)
        {
            this.core.MapPrefab(go, type);
        }

        public void GetBallByType(BallType type, out GameObject gameObject)
        {
            this.core.GetBallByType(type, out gameObject);
        }

        public void SelectBall(GameObject selectedBall)
        {
            if (this.selectedBall != null)
            {
                if (this.selectedBall.Equals(selectedBall.gameObject))
                {
                    this.selectedBall.transform.Translate(0, 0, 1.0f);
                    this.selectedBall = null;
                }
                else
                {
                    this.selectedBall.transform.Translate(0, 0, 1.0f);
                    this.selectedBall = selectedBall.gameObject;
                    this.selectedBall.transform.Translate(new Vector3(0, 0, -1.0f));
                }
            }
            else
            {
                this.selectedBall = selectedBall.gameObject;
                this.selectedBall.transform.Translate(new Vector3(0, 0, -1.0f));
            }
        }
    }
}
