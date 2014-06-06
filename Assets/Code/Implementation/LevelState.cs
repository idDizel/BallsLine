using BallsLine.Code.Implementation;
using BallsLine.Entities;
using BallsLine.Enums;
using BallsLine.Interfaces;
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
        //IElementNotifier selectedBall;
        IElementNotifier ballInstanse;
        MonoBehaviour scoreContext;

        static LevelState()
        {

        }

        private LevelState()
        {
            
        }

        //public IElementNotifier SelectedElement
        //{
        //    get
        //    {
        //        return this.core.SelectedEelement;
        //    }
        //    set
        //    {
        //        this.core.SelectedEelement = value;
        //    }
        //}

        public static LevelState Instance
        {
            get
            {
                return instance;
            }
        }

        public int Score
        {
            get
            {
                return this.core.Score;
            }
        }

        public void GenerateCells(GameObject cellInstance)
        {
            for (int x = 0; x < this.core.LevelXSize; x++)
            {
                for (int y = 0; y < this.core.LevelYSize; y++)
                {
                    GameObject newCell = (GameObject)GameObject.Instantiate(cellInstance, new Vector3(x * 1.1f, y * 1.1f, 0), Quaternion.identity);
                    newCell.GetComponent<CellBehaviour>().position = new Position(x, y);
                }
            }
        }

        public void GenerateBalls()
        {
            core.GenerateElements(3);
            //foreach (var ball in core.GenerateElements(3))
            //{
            //    this.core.GetBallByType(ball.BallType, out ballInstanse);
            //    var newBall = (GameObject)GameObject.Instantiate(ballInstanse, new Vector3(ball.BallPosition.X * 1.1f, ball.BallPosition.Y * 1.1f, -1), Quaternion.identity);
            //    newBall.GetComponent<BallBehaviour>().Position.X = ball.BallPosition.X;
            //    newBall.GetComponent<BallBehaviour>().Position.Y = ball.BallPosition.Y;
            //}
        }

        public void GenerateLevel()
        {
            this.core = new LevelCore(7);
            this.core.RegisterInstantiator(this.Inst);
            //this.core.OnPositionChanged += this.MoveBall;
            this.core.OnBallDelete += this.BallDelete;
            this.core.OnScroeChanged += this.ScoreChanged;
        }

        private IElementNotifier Inst(Position position, BallType ballType)
        {
            this.core.GetBallByType(ballType, out ballInstanse);
            var ee = (IElementNotifier)GameObject.Instantiate((MonoBehaviour)ballInstanse, new Vector3(position.X * 1.1f, position.Y * 1.1f, -1), Quaternion.identity);
            ee.Position = position;
            return ee;
        }

        private void ScoreChanged(object sender, EventArgs e)
        {
            this.scoreContext.guiText.text = string.Format("Score: {0}", this.core.Score);
        }

        public void MapPrefab(IElementNotifier go, BallType type)
        {
            this.core.MapPrefab(go, type);
        }

        public void GetBallByType(BallType type, out IElementNotifier gameObject)
        {
            this.core.GetBallByType(type, out gameObject);
        }

        public void SelectElement(IElementNotifier element)
        {
            this.core.SelectElement(element);
        }

        //public void SelectBall(IElementNotifier selectedBall)
        //{
        //    if (this.selectedBall != null)
        //    {
        //        if (this.selectedBall.Equals(selectedBall))
        //        {
        //            this.selectedBall.Unselected();
        //            this.selectedBall = null;
        //        }
        //    }
        //    else
        //    {
        //        this.selectedBall = selectedBall;
        //        this.selectedBall.Selected();
        //    }
        //}

        public void ChangePosition(Position newPosition)
        {
            //if (this.SelectedElement != null)
            //{
                this.core.ChangePosition(newPosition);
            //    this.SelectedElement = null;
            //}
            
        }

        //public void MoveBall(object sender, PositionEventArgs args)
        //{
        //    this.selectedBall.transform.position = new Vector3(args.position.X * 1.1f, args.position.Y * 1.1f, -1);
        //    this.selectedBall.GetComponent<BallBehaviour>().Position.X = args.position.X;
        //    this.selectedBall.GetComponent<BallBehaviour>().Position.Y = args.position.Y;
        //    if(!this.core.ValidateOfAxis(new Position(args.position.X, args.position.Y)))
        //    {
        //        this.GenerateBalls();
        //    }
        //    this.selectedBall = null;
        //}

        private void BallDelete(object sender, PositionListEventArgs args)
        {
            foreach (var element in args.Positions)
            {
                foreach (var desEl in GameObject.FindGameObjectsWithTag("Ball").Where(x => x.GetComponent<BallBehaviour>().Position.X == element.X && x.GetComponent<BallBehaviour>().Position.Y == element.Y))
                {
                    GameObject.Destroy(desEl);
                }
            }
        }

        public void RegisterScoreContext(MonoBehaviour context)
        {
            this.scoreContext = context;
        }
    }
}
