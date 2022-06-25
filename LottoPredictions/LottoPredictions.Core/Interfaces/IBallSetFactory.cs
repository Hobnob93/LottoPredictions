using LottoPredictions.Core.Enums;
using LottoPredictions.Core.Models;

namespace LottoPredictions.Core.Interfaces
{
    public interface IBallSetFactory
    {
        public (bool, BallSet) FromInput(BallSetType type, string input);
    }
}
