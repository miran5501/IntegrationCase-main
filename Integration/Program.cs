using Integration.Service;
using System;
using System.Threading;

namespace Integration
{
    public abstract class Program
    {
        public static void Main(string[] args)
        {
            var service = new ItemIntegrationService();

            // Her iş parçacığı başlamadan önce ve bitince mesaj yazdırıyoruz.
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("[Thread 1] Processing item: a");
                service.SaveItem("a");
                Console.WriteLine("[Thread 1] Finished processing item: a");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("[Thread 2] Processing item: b");
                service.SaveItem("b");
                Console.WriteLine("[Thread 2] Finished processing item: b");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("[Thread 3] Processing item: c");
                service.SaveItem("c");
                Console.WriteLine("[Thread 3] Finished processing item: c");
            });

            Thread.Sleep(500);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("[Thread 4] Processing item: a");
                service.SaveItem("a");
                Console.WriteLine("[Thread 4] Finished processing item: a");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("[Thread 5] Processing item: b");
                service.SaveItem("b");
                Console.WriteLine("[Thread 5] Finished processing item: b");
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("[Thread 6] Processing item: c");
                service.SaveItem("c");
                Console.WriteLine("[Thread 6] Finished processing item: c");
            });

            Thread.Sleep(5000);

            Console.WriteLine("Everything recorded:");

            service.GetAllItems().ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
