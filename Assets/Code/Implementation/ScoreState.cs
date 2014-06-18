using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Implementation
{
    public interface IScoreState
    {
        void Add(int count);
        void Multiply(int multiplier);
        void Bonus(int bonus);
    }

    public class ScoreState: IScoreState
    {
        public Score Score;

        public void ScoreState(Score score)
        {
            this.Score = score;
        }

        public virtual void Add(int count)
        {
            throw new Exception("Not implemented!");
        }

        public virtual void Multiply(int multiplier)
        {
            throw new Exception("Not implemented!");
        }

        public virtual void Bonus(int bonus)
        {
            throw new Exception("Not implemented!");
        }
    }

    public class Added: ScoreState
    {
        public override void Add(int count)
        {
            this.Score.DoAdd(count);
        }
    }

    public interface IScore
    {
        int Value { get; protected set; }

        public void ChangeScoreState(ScoreState state)
        {

        }
    }

    public class Score : IScore
    {

        private int score;
        private int multiplier;
        private int bonus;
        private ScoreState state;

        public void ChangeScoreState(ScoreState state)
        {
            this.state = state;
        }

        public int Value
        {
            get
            {
                return score;
            }
            protected set
            {
                this.score = value;
            }
        }

        public void Add(int count)
        {
            state.Add(count);
        }

        public void Multiply(int multiplier)
        {
            state.Multiply(multiplier);
        }

        public void Bonus(int bonus)
        {
            state.Bonus(bonus);
        }

        public void DoBonus(int bonus)
        {
            this.bonus = bonus;
        }

        public void DoMultiply(int multiplier)
        {
            this.multiplier = multiplier;
        }

        public void DoAdd(int count)
        {
            this.score = count * this.multiplier + this.bonus;
        }
    }

}
