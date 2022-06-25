using LottoPredictions.Core.Enums;

namespace LottoPredictions.Core.Models
{
    public record BallSet
    {
        public BallSetType Type { get; set; } = BallSetType.Normal;
        public int[] Balls { get; set; } = Array.Empty<int>();

        public override string ToString()
        {
            return $"{Type} - {string.Join(',', Balls)}";
        }
    }
}
