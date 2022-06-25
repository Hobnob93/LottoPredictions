using LottoPredictions.Core.Enums;
using LottoPredictions.Core.Interfaces;
using LottoPredictions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoPredictions.Core.Factories
{
    public class TicketGenerator : ITicketGenerator
    {
        private const int NumNormalBallsInATicket = 5;

        private readonly IBallSetFactory _ballSetFactory;

        private Queue<Ticket> _ticketQueue = new();
        private SetsContainer _originalContainer = new();

        public int NumGeneratedTickets { get; private set; }
        public bool HasNext => _ticketQueue.Count > 0;

        public TicketGenerator(IBallSetFactory ballSetFactory)
        {
            _ballSetFactory = ballSetFactory;
        }


        public void GenerateTickets(SetsContainer container, int numTickets)
        {
            Console.WriteLine($"Generating {numTickets} tickets...");

            _originalContainer = new SetsContainer(container);
            _ticketQueue = new();

            for (int i = 0; i < numTickets; i++)
            {
                RegenContainer(container);
                GenerateTicket(container);
            }

            Console.WriteLine($"All tickets generated!");
        }

        public Ticket Next()
        {
            if (!HasNext)
                throw new InvalidOperationException($"You tried to retrieve a ticket from an empty queue - please check value of '{nameof(HasNext)}' before retrieving from the queue.");

            return _ticketQueue.Dequeue();
        }

        private void RegenContainer(SetsContainer container)
        {
            for (var i = 0; i < container.BallSets.Count; i++)
            {
                var set = container.BallSets[i];

                Predicate<BallSet> countTooSmall = set.Type == BallSetType.Normal
                    ? ballSet => ballSet.Balls.Count < 3
                    : ballSet => ballSet.Balls.Count == 0;

                if (countTooSmall(set))
                {
                    var originalSet = _originalContainer.BallSets.Single(bs => bs.Id == set.Id);
                    var replenishedSet = _ballSetFactory.Replenish(originalSet, set);

                    container.BallSets[i] = replenishedSet;

                    Console.WriteLine("Regenerated container...");
                }
            }
        }

        private void GenerateTicket(SetsContainer container)
        {
            Console.WriteLine($"Generating ticket {_ticketQueue.Count + 1}...");

            var random = new Random(Guid.NewGuid().GetHashCode());
            var coinFlip = random.Next(0, 100);
            var numFromFirstGroup = coinFlip < 50 ? 2 : 3;
            var numFromSecondGroup = coinFlip < 50 ? 3 : 2;

            var pickedNumbers = new List<int>();
            Ticket ticket = new();

            do
            {
                pickedNumbers.Clear();

                pickedNumbers.AddRange(PickNumbersFromSet(container.BallSets[0], numFromFirstGroup, random));
                pickedNumbers.AddRange(PickNumbersFromSet(container.BallSets[1], numFromSecondGroup, random));
                pickedNumbers.AddRange(PickNumbersFromSet(container.BallSets[2], 1, random));

                ticket = new Ticket
                {
                    Balls = pickedNumbers.Take(NumNormalBallsInATicket).OrderBy(n => n).ToArray(),
                    Thunderball = pickedNumbers.Last()
                };
            }
            while (!ValidateTicket(ticket));

            foreach (var ball in ticket.Balls)
            {
                container.BallSets[0].Balls.Remove(ball);
                container.BallSets[1].Balls.Remove(ball);
            }

            container.BallSets[2].Balls.Remove(ticket.Thunderball);

            _ticketQueue.Enqueue(ticket);
        }

        private int[] PickNumbersFromSet(BallSet set, int numToSelect, Random random)
        {
            var pickedNumbers = new int[numToSelect];
            pickedNumbers[0] = set.Balls[random.Next(0, set.Balls.Count)];

            if (numToSelect == 1)
                return pickedNumbers;

            for (var i = 1; i < numToSelect; i++)
            {
                int pickedNumber;

                do
                {
                    pickedNumber = set.Balls[random.Next(0, set.Balls.Count)];
                }
                while (pickedNumbers.Contains(pickedNumber));

                pickedNumbers[i] = pickedNumber;
            }

            return pickedNumbers;
        }

        private bool ValidateTicket(Ticket ticket)
        {
            if (ticket is null)
                return false;

            if (ticket.Balls is null || ticket.Balls.Length == 0 || ticket.Balls.Length > NumNormalBallsInATicket)
                return false;

            if (ticket.Balls.Any(b => b == 0))
                return false;

            if (ticket.Thunderball == 0)
                return false;

            if (_ticketQueue.Contains(ticket))
                return false;

            return true;
        }
    }
}
