using BallsLine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Interfaces
{
    public abstract class Generator<T> where T: IElementNotifier
    {
        private Dictionary<Position, IElementNotifier> levelGrid;
        private int levelXSize;
        private int levelYSize;

        private IEnumerable<T> Instantiated { get; set; }

        private void SetupData(Dictionary<Position, IElementNotifier> levelGrid, int xSize, int ySize)
        {

        }


        protected abstract IEnumerable<T> Generate(int count);

        protected abstract IEnumerable<T> Instantiate();

        protected abstract void Save();

        protected virtual int EmptyCellsCount()
        {
            return this.levelXSize * this.levelYSize - this.levelGrid.Count;
        }

        public void Execute(Dictionary<Position, IElementNotifier> levelGrid, int xSize, int ySize)
        {
            this.levelGrid = levelGrid;
            this.levelXSize = xSize;
            this.levelYSize = ySize;
            var a  = this.Generate(3);
            var b = this.Instantiate();
            this.Save();
        }
    }
}
