using System.Collections.Generic;

namespace LottoPredictions.Core.Models
{
    public record SetsContainer
    {
        public SetsContainer()
        {

        }

        public SetsContainer(SetsContainer other)
        {
            BallSets = new List<BallSet>(other.BallSets);

            for (int i = 0; i < BallSets.Count; i++)
            {
                var ballSetBalls = BallSets[i].Balls;
                BallSets[i] = other.BallSets[i] with { Balls = new List<int>(ballSetBalls) };
            }
        }

        public List<BallSet> BallSets { get; init; } = new();
    }
}
