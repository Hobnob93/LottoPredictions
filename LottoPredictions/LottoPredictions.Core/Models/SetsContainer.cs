namespace LottoPredictions.Core.Models
{
    public record SetsContainer
    {
        public BallSet[] BallSets { get; init; } = Array.Empty<BallSet>();
    }
}
