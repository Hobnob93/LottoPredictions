using LottoPredictions.Core.Enums;

namespace LottoPredictions.Core.Models
{
    public record BallSet
    {
        public BallSetType Type { get; init; } = BallSetType.Normal;
        public int[] Balls { get; init; } = Array.Empty<int>();

        public override string ToString()
        {
            return $"{Type} - {string.Join(',', Balls)}";
        }
    }
}
