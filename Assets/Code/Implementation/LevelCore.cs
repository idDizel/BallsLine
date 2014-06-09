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

        #region Private Fields
        private int levelXSize;
        private int levelYSize;
        private int score;
        private IElementNotifier selectedEelement;
        private Dictionary<Position, IElementNotifier> LevelGrid;
        private Dictionary<ElementType, IElementNotifier> mappedPrefabs;
        private Func<Position, ElementType, IElementNotifier> instantiator;

        #endregion

        #region Events
        public EventHandler<PositionEventArgs> OnPositionChanged;
        public EventHandler<PositionListEventArgs> OnBallDelete;
        public EventHandler<EventArgs> OnScroeChanged;
        #endregion


        #region Properties
        public int Score
        {
            get
            {
                return this.score;
            }

            private set
            {
                this.score = value;
                if (this.OnScroeChanged != null)
                {
                    this.OnScroeChanged(this, new EventArgs());
                }
            }
        }

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

        //public IElementNotifier SelectedEelement 
        //{
        //    get
        //    {
        //        return this.selectedEelement;
        //    }
        //    set
        //    {
        //        if(value==null)
        //        {
        //            this.selectedEelement = value;
        //            return;
        //        }
        //        if (this.selectedEelement != null)
        //        {
        //            if (this.selectedEelement.Position.X==value.Position.X && this.selectedEelement.Position.Y == value.Position.Y)
        //            {
        //                this.selectedEelement.Unselected();
        //                this.selectedEelement = null;
        //            }
        //            else
        //            {
        //                this.selectedEelement.Unselected();
        //                this.selectedEelement = value;
        //                this.selectedEelement.Selected();
        //            }
        //        }
        //        else
        //        {
        //            this.selectedEelement = value;
        //            this.selectedEelement.Selected();
        //        }
        //    }
        //}
        #endregion

        public LevelCore(int lvlSize)
        {
            this.levelXSize = lvlSize;
            this.levelYSize = lvlSize;
            this.LevelGrid = new Dictionary<Position, IElementNotifier>();
            this.mappedPrefabs = new Dictionary<ElementType, IElementNotifier>();
        }

        private IElementNotifier Instantiator(Position position, ElementType ballType)
        {
            return instantiator.Invoke(position, ballType);
        }

        public void GenerateElements(int elementsCount)
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
                    ElementType ballType = (ElementType)rnd.Next(1, Enum.GetValues(typeof(ElementType)).Length);
                    IElementNotifier ee = this.Instantiator(position, ballType);
                    ee.Added();
                    this.LevelGrid.Add(position, ee);
                    count++;
                    //yield return ee;
                }
                if (count == elementsCount || this.EmptyCellCount == 0) finish = true;
            } while (!finish);
            if (this.EmptyCellCount == 0) Application.LoadLevel("Finish");
        }

        public bool ValidateOfAxis(Position position)
        {
            List<Position> validList = new List<Position>();
            IElementNotifier element;
            this.LevelGrid.TryGetValue(position, out element);

            var axisArrayX = this.LevelGrid.Where(val => val.Key.X == position.X && val.Value.Type == element.Type).Select(val => val.Key.Y).OrderBy(val => val).ToArray();
            int offsetX = position.Y - Array.IndexOf(axisArrayX, position.Y);
            var validX = axisArrayX.Where(val => val - Array.IndexOf(axisArrayX, val) == offsetX).Select(val => new Position(position.X, val)).ToList();

            var axisArrayY = this.LevelGrid.Where(val => val.Key.Y == position.Y && val.Value.Type == element.Type).Select(val => val.Key.X).OrderBy(val => val).ToArray();
            int offsetY = position.X - Array.IndexOf(axisArrayY, position.X);
            var validY = axisArrayY.Where(val => val - Array.IndexOf(axisArrayY, val) == offsetY).Select(val => new Position(val, position.Y)).ToList();

            //To do: refactor constants
            if (validX.Count >= 5)
            {
                validList.AddRange(validX);
                this.Score += 10;
            }
            if (validY.Count >= 5)
            {
                validList.AddRange(validY);
                this.Score += 10;
            }

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

        public void SelectElement(IElementNotifier element)
        {
            if(this.selectedEelement != null)
            {
                if (this.selectedEelement.Position.Equals(element.Position))
                {
                    this.selectedEelement.Unselected();
                    this.selectedEelement = null;
                }
                else
                {
                    this.selectedEelement.Unselected();
                    this.selectedEelement = element;
                    this.selectedEelement.Selected();
                }
            }
            else
            {
                this.selectedEelement = element;
                this.selectedEelement.Selected();
            }
        }

        public void ChangePosition(Position newPosition)
        {
            //BallType prevBallType;
            //this.LevelGrid.TryGetValue(prevPosition, out prevBallType);
            //if (this.selectedEelement.Position.X == newPosition.X && this.selectedEelement.Position.Y == newPosition.Y) ;
            //{
            //    this.selectedEelement.Unselected();
            //    this.selectedEelement = null;
            //    return;
            //}
            
            if(this.selectedEelement == null)
            {
                return;
            }
            if (this.selectedEelement.Position.Equals(newPosition))
            {
                this.selectedEelement.Unselected();
                this.selectedEelement = null;
                return;
            }
            IElementNotifier ee;
            this.LevelGrid.TryGetValue(this.selectedEelement.Position, out ee);
            if (!this.LevelGrid.ContainsKey(newPosition))
            {
                this.LevelGrid.Remove(this.selectedEelement.Position);
                this.selectedEelement.Position = newPosition;
                this.LevelGrid.Add(newPosition, ee);
                this.selectedEelement.PositionChanged();
                if (!this.ValidateOfAxis(this.selectedEelement.Position))
                {
                    this.GenerateElements(3);
                }
                this.selectedEelement = null;
                //this.OnPositionChanged(this, new PositionEventArgs(newPosition));
            }
        }

        public void RegisterInstantiator(Func<Position, ElementType, IElementNotifier> method)
        {
            this.instantiator = method;
        }

        public void MapPrefab(IElementNotifier gameObject, ElementType type)
        {
            this.mappedPrefabs.Add(type, gameObject);
        }

        public void GetBallByType(ElementType type, out IElementNotifier gameObject)
        {
            this.mappedPrefabs.TryGetValue(type, out gameObject);
        }

        internal void RegisterInstantiator()
        {
            throw new NotImplementedException();
        }
    }
}
