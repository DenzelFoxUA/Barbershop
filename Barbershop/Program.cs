using System.Diagnostics.CodeAnalysis;

namespace Barbershop
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TestBarberShop buisyDay = new TestBarberShop()
            {
                NameOfTheTest = "Buisy day",
                NumberOfBarbers = 2,
                ClientAppearanceTimeInterval = 2000,
                NumberOfClients = 30
            };

            buisyDay.RunTest();

            TestBarberShop buisyDayWithMoreWorkers = new TestBarberShop()
            {
                NameOfTheTest = "Buisy day with more workers",
                NumberOfBarbers = 4,
                ClientAppearanceTimeInterval = 2000,
                NumberOfClients = 30
            };

            buisyDayWithMoreWorkers.RunTest();

            TestBarberShop lazyDay = new TestBarberShop()
            {
                NameOfTheTest = "Lazy day",
                NumberOfBarbers = 4,
                ClientAppearanceTimeInterval = 12000,
                NumberOfClients = 5
            };

            lazyDay.RunTest();

            //zero or negate values
            TestBarberShop badValuesTest = new TestBarberShop()
            {
                NameOfTheTest = "BadTest",
                NumberOfBarbers = -1,
                ClientAppearanceTimeInterval = -1,
                NumberOfClients = -1
            };

            badValuesTest.RunTest();

        }

        public class TestBarberShop
        {

            private Barbershop testSample;

            public TestBarberShop()
            {
                testSample = new Barbershop();
            }

            [SetsRequiredMembers]
            public TestBarberShop(int numOfBarbers, int visitsInetrval_InMSeconds, int numOfClients, string testName) 
            {
                NameOfTheTest = testName;
                testSample = new Barbershop();
                NumberOfBarbers = numOfBarbers;
                ClientAppearanceTimeInterval = visitsInetrval_InMSeconds;
                NumberOfClients = numOfClients;
            }

            public required string NameOfTheTest { get; set; }
            public required int NumberOfBarbers { get; set; }
            public required int ClientAppearanceTimeInterval { get; set; }
            public required int NumberOfClients { get; set; }

            public void RunTest()
            {
                Console.WriteLine(NameOfTheTest);
                testSample.NumberOfBarbers = NumberOfBarbers;
                testSample.ClientsVisitsTimeInterval = ClientAppearanceTimeInterval;
                testSample.NumberOfClients = NumberOfClients;
                testSample.RunBarberShop();
                Console.ReadLine();
                Console.Clear();
            }
        
        }

    }
}
