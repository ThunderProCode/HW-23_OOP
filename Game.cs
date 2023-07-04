namespace HW
{
    class Game
    {
        private int playerRow;
        private int playerColumn;
        private bool fountainEnabled;

        private readonly Room[,] world;
        private readonly int numRows;
        private readonly int numColumns;

        public Game(int numRows, int numColumns)
        {
            this.numRows = numRows;
            this.numColumns = numColumns;
            world = new Room[numRows, numColumns];
            InitializeWorld();
            playerRow = 0;
            playerColumn = 0;
            fountainEnabled = false;
        }

        private void InitializeWorld()
        {
            Random random = new Random();

            // Set up the Entrance Room
            int entranceRow = random.Next(numRows);
            int entranceColumn = random.Next(numColumns);
            world[entranceRow, entranceColumn] = new Room();
            world[entranceRow, entranceColumn].Description = "You see light coming from the cavern entrance. This is the entrance.";

            // Set up the Fountain Room
            int fountainRow = random.Next(numRows);
            int fountainColumn = random.Next(numColumns);
            while (fountainRow == entranceRow && fountainColumn == entranceColumn)
            {
                // Ensure the Fountain Room is not placed on the same location as the Entrance Room
                fountainRow = random.Next(numRows);
                fountainColumn = random.Next(numColumns);
            }
            world[fountainRow, fountainColumn] = new Room();
            world[fountainRow, fountainColumn].Description = "You hear water dripping in this room. The Fountain of Objects is here!";
        }

        private void DisplayRoomInfo()
        {
            Room currentRoom = world[playerRow, playerColumn];
            Console.WriteLine($"You are in the room at (Row={playerRow}, Column={playerColumn}).");
            Console.WriteLine(currentRoom.Description);
        }

        private void EnableFountain()
        {
            if (playerRow == GetFountainRow() && playerColumn == GetFountainColumn())
            {
                fountainEnabled = true;
                world[playerRow, playerColumn].Description = "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";
            }
            else
            {
                Console.WriteLine("There is no effect. You are not in the fountain room.");
            }
        }

        private int GetFountainRow()
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    if (world[row, column] != null && world[row, column].Description.Contains("Fountain of Objects"))
                    {
                        return row;
                    }
                }
            }
            return -1; // Fountain not found
        }

        private int GetFountainColumn()
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    if (world[row, column] != null && world[row, column].Description.Contains("Fountain of Objects"))
                    {
                        return column;
                    }
                }
            }
            return -1; // Fountain not found
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the Fountain of Objects game!");
            Console.WriteLine("Try to find the Fountain of Objects and return to the cavern entrance to win.");
            Console.WriteLine("Good luck!\n");

            while (true)
            {
                DisplayRoomInfo();
                Console.Write("What do you want to do? ");
                string input = Console.ReadLine().ToLower();

                if (input.StartsWith("move"))
                {
                    ProcessMoveCommand(input);
                }
                else if (input == "enable fountain")
                {
                    EnableFountain();
                }

                if (playerRow == 0 && playerColumn == 0 && fountainEnabled)
                {
                    Console.WriteLine("\nThe Fountain of Objects has been reactivated, and you have escaped with your life!");
                    Console.WriteLine("You win!");
                    break;
                }
            }
        }

        private void ProcessMoveCommand(string input)
        {
            string direction = input.Split(' ')[1];
            int newRow = playerRow;
            int newColumn = playerColumn;

            switch (direction)
            {
                case "north":
                    newRow--;
                    break;
                case "south":
                    newRow++;
                    break;
                case "east":
                    newColumn++;
                    break;
                case "west":
                    newColumn--;
                    break;
                default:
                    Console.WriteLine("Invalid direction. Try again.");
                    return;
            }

            if (newRow < 0 || newRow >= numRows || newColumn < 0 || newColumn >= numColumns)
            {
                Console.WriteLine("You cannot move in that direction. Try again.");
            }
            else
            {
                playerRow = newRow;
                playerColumn = newColumn;
            }
        }
    }
}
