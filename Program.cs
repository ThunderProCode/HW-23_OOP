namespace HW
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Cavern of Objects!");
            Console.WriteLine("Choose a game size: small, medium, or large");
            string size = Console.ReadLine().ToLower();
            int numRows, numColumns;

            switch (size)
            {
                case "small":
                    numRows = 4;
                    numColumns = 4;
                    break;
                case "medium":
                    numRows = 6;
                    numColumns = 6;
                    break;
                case "large":
                    numRows = 8;
                    numColumns = 8;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting the game.");
                    return;
            }

            Game game = new Game(numRows, numColumns);
            game.StartGame();
        }
    }
}