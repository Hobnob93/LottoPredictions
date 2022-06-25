using LottoPredictions.Core.Enums;
using LottoPredictions.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LottoPredictions.Core.Extensions
{
    public static class SetsContainerExtensions
    {
        public static IEnumerable<BallSet> AllBallSetsOfType(this SetsContainer container, BallSetType type)
        {
            if (container is null || container.BallSets is null)
                return Enumerable.Empty<BallSet>();

            return container.BallSets.Where(bs => bs.Type == type);
        }
    }
}
