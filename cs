using System;

namespace ConsoleApp52
{
    class Players
    {
        public string p1name, p2name;
        public void Welcome()
        {
            Console.WriteLine("***Welcome to Connect four two player game***");
        }
        public void PlayersInformationFunction()
        {
            Console.Write("Enter the name of first player:-");
            p1name = Console.ReadLine();
            Console.Write("Enter the name of second player:-");
            p2name = Console.ReadLine();
            
        }
    }
    class gameboard
    {
        public int[,] Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;
            }
        }

        int[,] board = new int[6, 7];

        public void myboardfunction()
        {
            
            // This will print per row
            for (int row = 0; row < 6; row++)
            {
                //First, print the border at the left:
                Console.Write("|");
                
                // This will print each column of array
                for (int column = 0; column < 7; column++)
                {
                    //print the column for the row. Since the initial value of array is 0, print # if it is 0.
                    if (board[row, column] == 0)
                    {
                        Console.Write(" # ");
                    }
                    // we need to assign a value to player one... example 1
                    else if (board[row, column] == 1)
                    {
                        Console.Write(" O ");
                    }
                    // and for player 2... value is 2
                    else if (board[row, column] == 2)
                    {
                        Console.Write(" X ");
                    }
                }

               
                Console.WriteLine("|");
            }
          
            Console.WriteLine("  1  2  3  4  5  6  7");

            
        }
    }
    class game : Players
    {
        public int command1, command2;           

        public void commandplayer(gameboard board)
        {
            int row;
            int column;

            int turn = 0;
            do
            {

                do
                {
                    Console.Write("{0} Enter your number(1 to 7):- ", p1name);
                    int temp = 0;
                    int.TryParse(Console.ReadLine(), out temp);
                    command1 = temp;
                    row = (sign(command1, board.Board));
                }
                while (row < 0); // This means we need to return a positive number​
                column = Convert.ToInt32(command1) - 1; // We need to use the index number not the column number

                // Since we left the loop, that means we found the available row...
                board.Board[row, column] = 1; // And we will assign the sign of player 1
                board.myboardfunction();

                if(winner(1, row, column, board.Board))
                    break;
                
                turn++;

                do
                {
                    Console.Write("{0} Enter your number(1 to 7):- ", p2name);
                    command2 = Convert.ToInt32(Console.ReadLine());
                    row = (sign(command2, board.Board));
                }
                while (row < 0);
                column = Convert.ToInt32(command2) - 1; // Get column given by player 2.

                board.Board[row, column] = 2;
                board.myboardfunction();

                if (winner(2, row, column, board.Board))
                    break;

                turn++;
            }
            while (turn < 42); //This loop will return until we call break;

            if (turn >= 42)
            {
                //game is draw
                Console.WriteLine("Game draw.");
            }
            
        }
        public int sign(int command, int[,] board)
        {

            if (command <= 7 && command >= 0)
            {
                int row = 5; //this is the index of the lowest row/
                while (row >= 0) // <- condition is while >= 0
                {
                    if (board[row, command - 1] == 0) // This means that row is vacant, lets investigate this
                    {
                        return row;
                    }

                   

                    row--; // if filled, 0 will be -1
                }
              
                Console.WriteLine("column selected is already full");
            }

            Console.WriteLine("your number should be less than or equal to 7");
            return -1;
            
        }

        public bool winner(int playerNum, int row, int column, int[,] board)
        {

            //check win for horizontal
            //for horizontal, it is [0,1], [0,2], [0,3], [0,4]...we need to find a line of four. if you notice, the row doesnt change. so we can do this loop:
            for (int c = 0; c <= 3; c++)
            {

                if (board[row, c] == playerNum &&
                    board[row, c + 1] == playerNum &&
                    board[row, c + 2] == playerNum &&
                    board[row, c + 3] == playerNum)
                {
                    // let us try
                    Console.WriteLine("Player " + playerNum.ToString() + " wins!");
                    return true;
                }

            //check win for vertical
            
            for (int c = 0; c <= 2; c++)
            {
                if(board[c,column]==playerNum &&   
                    board[c+1,column]==playerNum   &&    
                    board[c+2,column]==playerNum  && 
                    board[c+3, column] == playerNum)
                {
                    Console.WriteLine("Player " + playerNum.ToString() + " wins!");
                    return true;
                }
            }

            //check win for diagonal
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    if (board[i, j] == 1 && board[i + 1, j + 1] == 1 && board[i + 2, j + 2] == 1 && board[i + 3, j + 3] == 1)
                    {
                        Console.WriteLine("Player " + playerNum.ToString() + " wins!");
                        break;
                    }
                }
            }

            
             
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            game mygame = new game();
            mygame.Welcome();
            mygame.PlayersInformationFunction();
            Console.WriteLine();

            gameboard myboard = new gameboard();
            myboard.myboardfunction();

            //game mygame = new game();
            mygame.commandplayer(myboard);
            //myboard.myboardfunction(); 

            Console.Read();
        }
    }
}
