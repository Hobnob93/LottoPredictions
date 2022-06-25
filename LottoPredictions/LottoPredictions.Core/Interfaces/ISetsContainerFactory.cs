using LottoPredictions.Core.Models;

namespace LottoPredictions.Core.Interfaces
{
    public interface ISetsContainerFactory
    {
        SetsContainer FromIndividualSets(params BallSet[] sets);
    }
}
