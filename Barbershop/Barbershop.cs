using Barbershop.Models;

namespace Barbershop
{
    public class Barbershop
    {
        //кількість вільних місць в холі перукарні
        public const int NUM_OF_SEATS = 4;

        //статичне поле черги клієнтів на стрижку для вільного доступу кількох потоків
        public static Queue<Client> WaitingSeats { get; private set; } = new Queue<Client>(NUM_OF_SEATS);

        //єдиний потік клієнтів, який створює об"єкт і регулює його поведінку - додає до черги або пропускає, якщо немає місця
        private Thread _clientsFlow;

        //показник що регулює загальну кількість людей в черзі
        private int _numOfClients;

        //кількість об"єктів перукарів (потоків)
        private int _numOfBarbers;

        // parameter regulating how Barber will be buisy today
        //bigger the number - more lazy day expected
        private int _clientVisitInterval;

        public int NumberOfClients 
        { 
            get => _numOfClients; 
            set
            {
                if(value < 0) _numOfClients = value * (-1);
                else _numOfClients = value;
            }
        }

        public int ClientsVisitsTimeInterval 
        {
            get => _clientVisitInterval;
            
            set
            {
                if (value < 0) _clientVisitInterval = value * (-1);
                else _clientVisitInterval = value;
            }
        
        }

        public int NumberOfBarbers
        {
            get => _numOfBarbers;
            set
            {
                if (value < 0) _numOfBarbers = value * (-1);
                else _numOfBarbers = value;
            }
        }

        public List<Barber> Barbers { get; private set; }

        public Barbershop() 
        {
            _numOfBarbers = 1;
            _numOfClients = 10;
            _clientVisitInterval = 5000; //msec
            Barbers = new List<Barber>();
            _clientsFlow = new Thread(StartClientFlow);
            
        }

        public void RunBarberShop()
        {
            for (int i = 1; i <= _numOfBarbers; i++)
            {
                var barber = new Barber(i);
                Barbers.Add(barber);
            }

            _clientsFlow.Start();
        }

        private void StartClientFlow()
        {

            int counter = 1;

            while (counter <= NumberOfClients)
            {
                Client client = new Client() { Name = $"Client {counter++}" };

                MoveClient(client);

                Thread.Sleep(ClientsVisitsTimeInterval);
            }
        }

        private void MoveClient(Client client)
        {

            if (client is null) return;

            if (WaitingSeats.Count >= Barbershop.NUM_OF_SEATS)
            {
                client.IsWaiting = false;
                client.IsLeaving = true;
            }
            else
            {
                client.IsWaiting = true;
                WaitingSeats.Enqueue(client);
            }

            client.ShowStatus();
        }
    }
}
