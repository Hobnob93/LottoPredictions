using LottoPredictions.Core.Enums;
using LottoPredictions.Core.Interfaces;
using LottoPredictions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoPredictions.Core.Factories
{
    public class BallSetFactory : IBallSetFactory
    {
        public (bool, BallSet) FromInput(BallSetType type, string input)
        {
            (var isValid, var rawNumbers) = SplitInput(input);
            var ballSet = new BallSet
            {
                Id = Guid.NewGuid()
            };

            if (!isValid)
                return (isValid, ballSet);

            (isValid, var numbers) = ParseInputArray(rawNumbers);
            ballSet = ballSet with { Type = type, Balls = numbers.ToList() };

            return (isValid, ballSet);
        }

        public BallSet Replenish(BallSet original, BallSet old)
        {
            return old with { Balls = new List<int>(original.Balls) };
        }

        private (bool, string[]) SplitInput(string input)
        {
            if (input == null)
                return (false, Array.Empty<string>());

            var components = input.Split('\u0020'); // Unicode character for 'space'

            return (components.Length > 0, components);
        }

        private (bool, int[]) ParseInputArray(string[] rawNumbers)
        {
            var isValid = true;
            var numbers = new List<int>();

            foreach (var rawNumber in rawNumbers)
            {
                isValid = int.TryParse(rawNumber, out var number);

                if (!isValid)
                    break;

                numbers.Add(number);
            }

            return (isValid, numbers.ToArray());
        }
    }
}
