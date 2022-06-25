using LottoPredictions.Core.Enums;
using System;
using System.Collections.Generic;

namespace LottoPredictions.Core.Models
{
    public record BallSet
    {
        public Guid Id { get; init; }
        public BallSetType Type { get; init; } = BallSetType.Normal;
        public List<int> Balls { get; init; } = new();

        public override string ToString()
        {
            return $"{Type} - {string.Join(',', Balls)}";
        }
    }
}
