using LottoPredictions.Core.Models;

namespace LottoPredictions.Core.Interfaces
{
    public interface ITicketGenerator
    {
        public int NumGeneratedTickets { get; }
        public bool HasNext { get; }

        public void GenerateTickets(SetsContainer container, int numTickets);
        public Ticket Next();
    }
}
