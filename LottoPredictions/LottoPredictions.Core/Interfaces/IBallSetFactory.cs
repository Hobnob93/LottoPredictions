using LottoPredictions.Core.Enums;
using LottoPredictions.Core.Models;

namespace LottoPredictions.Core.Interfaces
{
    public interface IBallSetFactory
    {
        public BallSet Empty { get; }
        public (bool, BallSet) FromInput(BallSetType type, string input);
    }
}
