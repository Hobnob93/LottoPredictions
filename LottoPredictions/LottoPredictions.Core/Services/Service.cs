using LottoPredictions.Core.Interfaces;

namespace LottoPredictions.Core.Services
{
    public class Service : IService
    {
        public void Run()
        {
            Console.WriteLine("Service Loaded");

            Console.ReadLine();
        }
    }
}
