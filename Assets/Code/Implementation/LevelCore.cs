using BallsLine.Code.Implementation;
using BallsLine.Entities;
using BallsLine.Enums;
using BallsLine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BallsLine.Implementation
{
    public class LevelCore: ILevelCore, IPrefabMapping
    {
        private Dictionary<Position, BallType> LevelGrid;
        private Dictionary<BallType, GameObject> mappedPrefabs;
        private int levelXSize;
        private int levelYSize;

        public EventHandler<PositionEventArgs> OnPositionChanged;
        public EventHandler<PositionListEventArgs> OnBallDelete;

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
                //To do: optimize randomizer
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

        public bool ValidateOfAxis(Position position)
        {
            List<Position> validList = new List<Position>();
            BallType ballType;
            this.LevelGrid.TryGetValue(position, out ballType);

            var axisArrayX = this.LevelGrid.Where(val => val.Key.X == position.X && val.Value == ballType).Select(val => val.Key.Y).OrderBy(val=>val).ToArray();
            int offsetX = position.Y - Array.IndexOf(axisArrayX, position.Y);
            var validX = axisArrayX.Where(val => val - Array.IndexOf(axisArrayX, val) == offsetX).Select(val => new Position(position.X, val)).ToList();

            var axisArrayY = this.LevelGrid.Where(val => val.Key.Y == position.Y && val.Value == ballType).Select(val => val.Key.X).OrderBy(val=>val).ToArray();
            int offsetY = position.X - Array.IndexOf(axisArrayY, position.X);
            var validY = axisArrayY.Where(val => val - Array.IndexOf(axisArrayY, val) == offsetY).Select(val => new Position(val, position.Y)).ToList();

            //To do: refactor constants
            if (validX.Count >= 5) validList.AddRange(validX);
            if (validY.Count >= 5) validList.AddRange(validY);

            if (validList.Count > 0)
            {
                this.OnBallDelete(this, new PositionListEventArgs(validList.Distinct().ToList()));
                foreach (var delBall in validList.Distinct())
                {
                    this.LevelGrid.Remove(delBall);
                }
                return true;
            }
            return false;
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
