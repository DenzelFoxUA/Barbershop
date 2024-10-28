
namespace Barbershop.Models
{
    public class Client : Person
    {
        public bool IsWaiting { get; set; } = false;
        public bool IsLeaving { get; set; } = false;

        public void ShowStatus(string barbersName = "")
        {
            if (IsWaiting == true)
                Console.WriteLine($"{Name}: I'm waiting for my turn.");
            else if (!IsWaiting && IsLeaving == false)
                Console.WriteLine($"{Name}: {barbersName} is doing my hair.");
            else
                Console.WriteLine($"{Name}: No room for me. I'll go to another shop.");
        }

    }
}
