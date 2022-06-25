using System;

namespace LottoPredictions.Core.Models
{
    public record Ticket
    {
        public int[] Balls { get; init; } = Array.Empty<int>();
        public int Thunderball { get; init; }

        public override string ToString()
        {
            return $"Ticket: {string.Join(' ', Balls)} ({Thunderball})";
        }
    }
}
