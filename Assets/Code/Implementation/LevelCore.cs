using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BallsLine.Interfaces;
using BallsLine.Entities;
using BallsLine.Enums;
using UnityEngine;
using UnityEditor;
using BallsLine.Code.Implementation;

namespace BallsLine.Implementation
{
    public class LevelCore: ILevelCore, IPrefabMapping
    {
        private Dictionary<Position, BallType> LevelGrid;
        private Dictionary<BallType, GameObject> mappedPrefabs;
        private int levelXSize;
        private int levelYSize;

        public EventHandler<PositionEventArgs> OnPositionChanged;

        public int LevelXSize
        {
            get
            {
                return this.levelXSize;
            }
        }

        public int LevelYSize
        {
            get
            {
                return this.levelYSize;
            }
        }

        public int LevelSize
        {
            get
            {
                return this.levelXSize * this.levelYSize;
            }
        }

        public int EmptyCellCount 
        {
            get
            {
                return this.LevelSize - this.LevelGrid.Count;
            }
        }

        public LevelCore(int lvlSize)
        {
            this.levelXSize = lvlSize;
            this.levelYSize = lvlSize;
            this.LevelGrid = new Dictionary<Position, BallType>();
            this.mappedPrefabs = new Dictionary<BallType, GameObject>();
        }

        public IEnumerable<BallEntity> GenerateBalls(int ballsCount)
        {
            int count = 0;
            bool finish = false;
            do
            {
                System.Random rnd = new System.Random();
                Position position = new Position(rnd.Next(0, this.levelXSize), rnd.Next(0, this.levelYSize));
                if(!this.LevelGrid.ContainsKey(position))
                {
                    BallType ballType = (BallType)rnd.Next(1, Enum.GetValues(typeof(BallType)).Length);
                    this.LevelGrid.Add(position, ballType);
                    count++;
                    yield return new BallEntity(position, ballType);
                }
                if (count == ballsCount || this.EmptyCellCount == 0) finish = true;
            } while (!finish);
        }

        public IEnumerable<Position> ValidateLines(BallEntity ball)
        {
            return null;
        }

        public void ChangePosition(Position newPosition, Position prevPosition)
        {
            BallType prevBallType;
            this.LevelGrid.TryGetValue(prevPosition, out prevBallType);
            if(!this.LevelGrid.ContainsKey(newPosition))
            {
                this.LevelGrid.Add(newPosition, prevBallType);
                this.LevelGrid.Remove(prevPosition);
                this.OnPositionChanged(this, new PositionEventArgs(newPosition));
            }
        }

        public void MapPrefab(GameObject gameObject, BallType type)
        {
            this.mappedPrefabs.Add(type, gameObject);
        }

        public void GetBallByType(BallType type, out GameObject gameObject)
        {
            this.mappedPrefabs.TryGetValue(type, out gameObject);
        }
    }
}
