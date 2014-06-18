using BallsLine.Entities;
using BallsLine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Implementation
{
    public class Level
    {
        ScoreState _scoreState;
        private Dictionary<Position, IElementNotifier> levelGrid = new Dictionary<Position, IElementNotifier>();





        private List<ICommand> commands;

        public void Generate(IGeneratorStrategy generator)
        {
            generator.Generate(this.levelGrid);
            generator.Instantiate();
        }
    }


public class ScoreState
{
    public virtual void Add()
    { }

    public virtual void Remove()
    { }

    public virtual void Multiply()
    { }

    public virtual void Bonus()
    { }
}

public interface ICommand
{
    void Execute();
}

public class FirstLevel: Level
{
    public void G()
    {
        
    }
}

    public class Generator
    {
        private Level level;
        public Generator(Level level)
        {
            this.level = level;
        }
        public void Generate()
        {
            
        }
    }


    public class LevelState
    {
        private Level level;

        public void Generate()
        {

        }
    }

    public class MapGenerator : IGeneratorStrategy
    {
        private Level context;
        public MapGenerator(Level context)
        {
            
        }
        public IEnumerable<IElement> Generate()
        {

        }

        public IEnumerable<IElementNotifier> Instantiate()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

}