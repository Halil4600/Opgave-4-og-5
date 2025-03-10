namespace TCPClient
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Press 1 for String Client or 2 for JSON Client.");
            string choice = Console.ReadLine();

            if (choice == "1")
                await new TCPStringClient().StartClient();
            else if (choice == "2")
                await new TCPJsonClient().StartClient();
        }
    }
}
