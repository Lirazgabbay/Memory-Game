using System;
using System.Text;

namespace Ex02
{
    internal class ConsoleCommands
    {
        public static void AskForUserName()
        {
            Console.WriteLine("please type your name:");
        }

        public static void AskForUpponentName()
        {
            Console.WriteLine("please type the upponent name:");
        }

        public static void AskGameMode()
        {
            Console.WriteLine("Enter 1 to play against the computer or 2 to play against another player:");
        }

        public static void InvalidNumberInput()
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }

        public static void InvalidNumberOfPlayers()
        {
            Console.WriteLine("Invalid number.Please enter 1 to play against the computer or 2 to play against another player:");
        }

        public static void SpaceInPlayerName()
        {
            Console.WriteLine("Invalid input. Player name should not contain any spaces. try again:");
        }

        public static void InvalidPlayerNameLength()
        {
            Console.WriteLine("Invalid name length. Player names should contain a maximum of 20 characters. Please try again:");
        }

        public static void ChooseBoardDimensionsMsg()
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("Choose board dimensions.");
            message.AppendLine("Enter two numbers separated by a space.");
            message.AppendLine("The first number represents rows, and the second number represents columns.");
            message.AppendLine("Minimum size is 4x4, maximum is 6x6.");
            message.AppendLine("Board size must have an even number of cells.");
            Console.WriteLine(message.ToString());
        }
        
        public static void InvalidBoardLength()
        {
            Console.WriteLine("Invalid board length. Please enter two numbers separated by a space.");
        }

        public static void InvalidBoardRowsLength()
        {
            Console.WriteLine("Invalid input. Please enter valid numbers for rows.");
        }

        public static void InvalidBoardColumnsLength()
        {
            Console.WriteLine("Invalid input. Please enter valid numbers for columns.");
        }

        public static void InvalidBoardRange()
        {
            Console.WriteLine("Invalid input. Board dimensions should be between 4x4 and 6x6.");
        }

        public static void OddBoardSizeMsg()
        {
            Console.WriteLine("Invalid input. Board size must have an even number of cells.");
        }

        public static void AskForHumanMove()
        {
            StringBuilder message = new StringBuilder();

            message.Append("Please make your selection. ");
            message.Append("Enter 2 characters where the first represents the column number (header) ");
            message.Append("and the second represents the row number.(no spaces between the charcters) ");
            message.Append("A legal choice is an empty cell on the board. ");
            Console.WriteLine(message.ToString());
        }

        public static void EmptyhumanMoveMsg()
        {
            Console.WriteLine("No input detected.");
        }

        public static void InvalidLengthMoveMsg()
        {
            Console.WriteLine("Invalid Move length.");
        }

        public static void InvalidHeaderLetterRangeMessage()
        {
            Console.WriteLine("Invalid Header. should be an upper case letter that on the board game.");
        }

        public static void PrintNotDigitMessage()
        {
            Console.WriteLine("Invalid number. should be a digit.");
        }

        public static void InvalidDigitRangeMessage()
        {
            Console.WriteLine("Invalid number. should be a digit that on the board game.");
        }

        public static void ExposedCellMessage()
        {
            Console.WriteLine("You have selected an already exposed cell.");
        }

        public static void GameOverWinnerMsg(String winner)
        {
            if(winner == "tie")
            {
                Console.WriteLine("it's a tie!");
            }
            else
            {
                Console.WriteLine("the winner is: " +  winner);
            }   
        }

        public static void GameOverStatusMsg(Player player1, Player player2)
        {
            string msg;

            msg =
                string.Format(
@"Score status: 
{0} with {1} points. 
{2} with {3} points.",
player1.Name, player1.Score, player2.Name, player2.Score);
        Console.WriteLine(msg);
        }

        public static void QuitMsg()
        {
            Console.WriteLine("You chose to quit the game. I hope you had fun!");
        }

        public static void PlayAgainMsg()
        {
            Console.WriteLine("Play again? (yes/no):");
        }

        public static void TurnMsg(string name)
        {
            Console.WriteLine($"It's {name}'s turn!");
        }

        public static void ItIsAMatchMsg(string name)
        {
            Console.WriteLine($"It's a match! {name} got another round.");
        }

        public static void ItIsAMissMatchMsg()
        {
            Console.WriteLine("It's a miss, better luck next time.");
        }

        public static void InvalidPlayAgainChoice()
        {
            Console.WriteLine("invalid input, please try again.");
        }

        public static void ExitMsg()
        {
            Console.WriteLine("You chose to exit. I hope you had fun!");
        }
    }
}
