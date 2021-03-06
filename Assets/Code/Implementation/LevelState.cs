﻿using BallsLine.Code.Implementation;
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
        GameObject ballInstanse;
        MonoBehaviour scoreContext;

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
                    newCell.GetComponent<CellBehaviour>().position.X = x;
                    newCell.GetComponent<CellBehaviour>().position.Y = y;
                }
            }
        }

        public void GenerateBalls()
        {
            foreach (var ball in core.GenerateBalls(3))
            {
                this.core.GetBallByType(ball.BallType, out ballInstanse);
                var newBall = (GameObject)GameObject.Instantiate(ballInstanse, new Vector3(ball.BallPosition.X * 1.1f, ball.BallPosition.Y * 1.1f, -1), Quaternion.identity);
                newBall.GetComponent<BallBehaviour>().Position.X = ball.BallPosition.X;
                newBall.GetComponent<BallBehaviour>().Position.Y = ball.BallPosition.Y;
            }
        }

        public void GenerateLevel()
        {
            this.core = new LevelCore(7);
            this.core.OnPositionChanged += this.MoveBall;
            this.core.OnBallDelete += this.BallDelete;
            this.core.OnScroeChanged += this.ScoreChanged;
        }

        private void ScoreChanged(object sender, EventArgs e)
        {
            this.scoreContext.guiText.text = string.Format("Score: {0}", this.core.Score);
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

        public void ChangePosition(Position newPosition)
        {
            if(this.selectedBall!=null)
            {
                Position prevPosition = this.selectedBall.GetComponent<BallBehaviour>().Position;
                this.core.ChangePosition(newPosition, prevPosition);
            }
        }

        public void MoveBall(object sender, PositionEventArgs args)
        {
            this.selectedBall.transform.position = new Vector3(args.position.X * 1.1f, args.position.Y * 1.1f, -1);
            this.selectedBall.GetComponent<BallBehaviour>().Position.X = args.position.X;
            this.selectedBall.GetComponent<BallBehaviour>().Position.Y = args.position.Y;
            if(!this.core.ValidateOfAxis(new Position(args.position.X, args.position.Y)))
            {
                this.GenerateBalls();
            }
            this.selectedBall = null;
        }

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
