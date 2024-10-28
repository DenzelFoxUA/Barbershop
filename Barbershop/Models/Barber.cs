
namespace Barbershop.Models
{
    public class Barber : Person, IDisposable
    {
        private bool _disposed = false;

        //лише один перукар має доступ до потоку клієнтів за раз
        private static Semaphore barbersSemaphore = new Semaphore(1, 1); 
        public Thread BarberWorkFlow { get; }

        public bool IsBuisy { get; set; } = false;

        public Barber()
        {
            BarberWorkFlow = new Thread(DoHair);
            BarberWorkFlow.Start();
        }

        public Barber(int index)
        {
            Name = "Barber" + index;
            BarberWorkFlow = new Thread(DoHair);
            BarberWorkFlow.Start();
        }

        public void ShowCurrentState()
        {

            string info = string.Empty;
            info = IsBuisy == true ? $"{this} I'm buisy. Do my work." :
                   $"{this} I'm free. Drinking coffee, waiting for clients.";
            Console.WriteLine(info);

        }

        public void DoHair()
        {
            int durationOfService = new Random().Next(7000, 10000),
                   coffeBreakDuration = 1000; // check for client every 1 sec;
            int waitCounter = 1;
            int maxWaitCounterValue = 5; //if more than 5 turns in a row without clients, flow is terminated


            while (waitCounter <= maxWaitCounterValue)
            {
                //barbersSemaphore.WaitOne();
                if (!IsBuisy && Barbershop.WaitingSeats.Count == 0)
                {
                    ShowCurrentState();
                    Thread.Sleep(coffeBreakDuration);//час відпочинку
                    waitCounter++;
                }
                else if (!IsBuisy && Barbershop.WaitingSeats.Count > 0)
                {
                    IsBuisy = true;
                    DequeueClient();
                    waitCounter = 0;
                    IsBuisy = false;

                    Thread.Sleep(durationOfService);//час на роботу
                }
                //barbersSemaphore.Release();

            }
            Dispose();
        }

        private void DequeueClient()
        {
            Client client;

            //реалізовано з використанням монітору так як симафор дуже сповільнює роботу потоків
            //за рахунок строк Thread.Sleep
            bool acquiredLock = false;
            Monitor.Enter(Barbershop.WaitingSeats, ref acquiredLock);

            try
            {
                client = Barbershop.WaitingSeats.Dequeue();
                client.IsWaiting = false;
                Console.WriteLine("---------------- HAIRCUT IN PROCESS ---------------------");
                client.ShowStatus(this.Name);
                ShowCurrentState();
                Console.WriteLine("---------------------------------------------------------");
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("There no clients. " + ex.Message);
                IsBuisy = false;
                ShowCurrentState();
            }
            finally
            {
                if (acquiredLock is true) Monitor.Exit(Barbershop.WaitingSeats);
            }
        }

        public override string ToString()
        {
            return $"Barber {Name}:";
        }

        public virtual void Dispose(bool isDisposed)
        {
            string disposeMessage = $"{Name}: Workday is over. Im off.";
            if (_disposed) return;
            if (isDisposed)
            {
                Console.WriteLine(disposeMessage);
            }
            else
            {
                Console.WriteLine(disposeMessage);
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Barber()
        {
            Dispose(false);
        }
    }
}
