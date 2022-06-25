using LottoPredictions.Core.Interfaces;
using LottoPredictions.Core.Models;
using System;
using System.Linq;

namespace LottoPredictions.Core.Factories
{
    public class SetsContainerFactory : ISetsContainerFactory
    {
        public SetsContainer FromIndividualSets(params BallSet[] sets)
        {
            if (sets is null || sets.Length == 0)
                throw new InvalidOperationException($"The parameter '{nameof(sets)}' is either NULL or EMPTY!");

            if (sets.Any(bs => bs is null || bs.Balls is null || bs.Balls.Count == 0))
                throw new InvalidOperationException($"At least one of the provided sets is NULL or contains an EMPTY collection!");

            return new SetsContainer
            {
                BallSets = sets.ToList(),
            };
        }
    }
}
