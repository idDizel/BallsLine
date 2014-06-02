using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game1
{
    class Core
    {
        public CellType[,] LevelArray = new CellType[7, 7];

        public CellType[,] PubArray 
        {
            get
            {
                return this.LevelArray;
            }
        }

        public int EmptyCount
        {
            get
            {
                int count = 0;
                foreach(var e in this.LevelArray)
                {
                    if (e == CellType.Empty) count++;
                }
                return count;
            }
        }

        public IEnumerable<Pos> Generate()
        {
            int count = 0;
            bool finish = false;
            do
            {
                System.Random rnd = new System.Random();
                int x = rnd.Next(0, 7);
                int y = rnd.Next(0, 7);
                if (this.LevelArray[x, y] == CellType.Empty)
                {
                    this.LevelArray[x,y] = (CellType)rnd.Next(1, Enum.GetValues(typeof(CellType)).Length);
                    count++;
                    yield return new Pos(x, y);
                }
                if (count == 3 || this.EmptyCount == 0) finish = true;
            } while (!finish);
        }

        private bool CheckCount(List<Pos> list)
        {
            if (list.Count >= 5)
            {
                foreach (var cl in list)
                {
                    this.LevelArray[cl.X, cl.Y] = CellType.Empty;
                }
				return true;
            }
            return false;
        }

        public IEnumerable<Pos> CheckLine(int x, int y, CellType ballType)
        {
            List<Pos> checkListX = new List<Pos>() { new Pos(x,y)};
            List<Pos> checkListY = new List<Pos>() { new Pos(x,y)};
            List<Pos> checkListSum = new List<Pos>();

            CheckXLine(1, x, y, ballType, checkListX);
            CheckXLine(-1, x, y, ballType, checkListX);
            CheckYLine(1, x, y, ballType, checkListY);
            CheckYLine(-1, x, y, ballType, checkListY);

            if (CheckCount(checkListX)) checkListSum.AddRange(checkListX);
            if (CheckCount(checkListY)) checkListSum.AddRange(checkListY);
            return checkListSum.Distinct();
        }

        private void CheckXLine(int increment, int x, int y, CellType ballType, List<Pos> checkListX)
        {
            bool finish = false;
            int axisX = x;
            do
            {
                axisX += increment;
                if (axisX < 0 || axisX >= this.LevelArray.GetLength(1)) break;
                if (this.LevelArray[axisX, y] != ballType) finish = true;
                else
                {
                    checkListX.Add(new Pos(axisX, y));
                }
            } while (!finish);
        }

        private void CheckYLine(int increment, int x, int y, CellType ballType, List<Pos> checkListY)
        {
            bool finish = false;
            int axisY = y;
            do
            {
                axisY += increment;
                if (axisY < 0 || axisY >= this.LevelArray.GetLength(0)) break;
                if (this.LevelArray[x, axisY] != ballType) finish = true;
                else
                {
                    checkListY.Add(new Pos(x, axisY));
                }
            } while (!finish);
        }

        public Color ColorTransformer(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Empty: return Color.clear;
                case CellType.Red: return Color.red;
                case CellType.Green: return Color.green;
                case CellType.Yellow: return Color.yellow;
                case CellType.Blue: return Color.blue;
                case CellType.Purple: return Color.cyan;
                case CellType.Orange: return Color.magenta;
                default: return Color.clear;
            }
        }
    }
}
