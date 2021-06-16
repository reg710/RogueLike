using System;

namespace gridDemo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // [x] Set up a grid (start small)
            // [x] Set up key grab - user input 
            // [x] Have the player react to user input
            // [x] Set up the player on the grid
            // [x] Mark the item to be collected
            // [x] Create 'threat' that moves in opposition to players moves
            // [x] If threat hits trap or treasure, game ends


            // Create a grid
            int size = 15;
            string[,] board = new string[size, size];

            // Fill up the array with something
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    board[row, column] = ".";
                }
            }

            //Setting the player
            int positionX = 0;
            int positionY = 0;
            int threatX = 0;
            int threatY = 0;
            board[positionY, positionX] = "+";

            //set a random amount of treasure
            Random rd = new Random();
            int treasureCount = rd.Next(1, 5);

            //randomly set the treasure on the board
            // keep it from being set to a space already marked as treasure or threat
            for (int i = 0; i < treasureCount; i++)
            {
                while (true)
                {
                    int treasureX = rd.Next(1, size);
                    int treasureY = rd.Next(1, size);

                    if (board[treasureX, treasureY] == "$" || board[treasureX, treasureY] == "O")
                    {
                        continue;
                    }
                    board[treasureX, treasureY] = "$";
                    break;
                }
            }

            //set a random amount of traps
            int trapCount = rd.Next(1, 15);

            //randomly set the traps on the board
            for (int i = 0; i < trapCount; i++)
            {
                while (true)
                {
                    int trapX = rd.Next(1, size);
                    int trapY = rd.Next(1, size);

                    if (board[trapX, trapY] == "$" || board[trapX, trapY] == "O")
                    {
                        continue;
                    }
                    board[trapX, trapY] = "O";
                    break;
                }
            }

            //Place moving threat that moves in opposing direction as player
            // Later try to add more than one
            int threat = 1;
            while (true)
            {
                threatX = rd.Next(1, size);
                threatY = rd.Next(1, size);

                if (board[threatX, threatY] == "$" || board[threatX, threatY] == "O")
                {
                    continue;
                }
                board[threatX, threatY] = "X";
                break;
            }

            bool playerDeath = false;
            //gameplay loop, which can only run when player is in bounds
            while (treasureCount > 0 && playerDeath == false)
            {
                // print out the board
                Console.Clear();
                drawBoard(board);
                Console.WriteLine("You have " + treasureCount + " remaining treasures to collect.");
                Console.WriteLine("Watch out for O (traps) and X (a threating figure)!");
                Console.WriteLine("Please hit W,A,S,D to move.");
                // the 'true' makes it so you don't see the key you typed on your console.
                string userTypey = Console.ReadKey(true).Key.ToString();

                //Player's input controls with treasure counter
                if (userTypey == "D" && positionX < size - 1)
                {
                    //resets player's previous position to neutral and moves player coordinates
                    board[positionY, positionX] = ".";
                    positionX++;

                    //this if/else keeps the threat from out of boundsing by moving to opposite side
                    //or moves the threat by one coordinate
                    if (threatX == 0)
                    {
                        board[threatX, threatY] = ".";
                        threatX = size - 1;
                    }
                    else
                    {
                        if (board[threatX - 1, threatY] == "O" || board[threatX - 1, threatY] == "$")
                        {
                            playerDeath = true;
                        }
                        board[threatX, threatY] = ".";
                        threatX--;
                    }

                    //this if/else loops checks to see if player finds treasures, traps, or threat
                    if (board[positionY, positionX] == "$")
                    {
                        treasureCount--;
                    }
                    else if (board[positionY, positionX] == "O" || board[positionY, positionX] == "X")
                    {
                        playerDeath = true;
                    }
                }

                else if (userTypey == "S" && positionY < size - 1)
                {
                    board[positionY, positionX] = ".";
                    positionY++;

                    if (threatY == 0)
                    {
                        board[threatX, threatY] = ".";
                        threatY = size - 1;
                    }
                    else
                    {
                        if (board[threatX, threatY - 1] == "O" || board[threatX, threatY - 1] == "$")
                        {
                            playerDeath = true;
                        }
                        board[threatX, threatY] = ".";
                        threatY--;
                    }

                    if (board[positionY, positionX] == "$")
                    {
                        treasureCount--;
                    }
                    else if (board[positionY, positionX] == "O" || board[positionY, positionX] == "X")
                    {
                        playerDeath = true;
                    }
                }

                else if (userTypey == "A" && positionX > 0)
                {
                    board[positionY, positionX] = ".";
                    positionX--;

                    if (threatX == size - 1)
                    {
                        board[threatX, threatY] = ".";
                        threatX = 0;
                    }
                    else
                    {
                        if (board[threatX + 1, threatY] == "O" || board[threatX + 1, threatY] == "$")
                        {
                            playerDeath = true;
                        }
                        board[threatX, threatY] = ".";
                        threatX++;
                    }

                    if (board[positionY, positionX] == "$")
                    {
                        treasureCount--;
                    }
                    else if (board[positionY, positionX] == "O" || board[positionY, positionX] == "X")
                    {
                        playerDeath = true;
                    }
                }

                else if (userTypey == "W" && positionY > 0)
                {
                    board[positionY, positionX] = ".";
                    positionY--;

                    if (threatY == size - 1)
                    {
                        board[threatX, threatY] = ".";
                        threatY = 0;
                    }
                    else
                    {
                        if (board[threatX, threatY + 1] == "O" || board[threatX, threatY + 1] == "$")
                        {
                            playerDeath = true;
                        }
                        board[threatX, threatY] = ".";
                        threatY++;
                    }

                    if (board[positionY, positionX] == "$")
                    {
                        treasureCount--;
                    }
                    else if (board[positionY, positionX] == "O" || board[positionY, positionX] == "X")
                    {
                        playerDeath = true;
                    }

                }
                else
                {
                    Console.WriteLine("Type that again");
                }

                board[threatX, threatY] = "X";
                board[positionY, positionX] = "+";
                Console.Clear();
                drawBoard(board);
            }

            if (treasureCount == 0)
            {
                Console.WriteLine("WINNER");
            }
            else
            {
                Console.WriteLine("You have died.");
            }

        }


        //Set up a function
        //Setting up a recipe - when given something it knows its a string array it's going to be called "board"
        static void drawBoard(string[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    Console.Write(board[row, column] + " ");
                }
                Console.WriteLine();
            }

        }

    }
}


