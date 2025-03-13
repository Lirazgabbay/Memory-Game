using Ex02.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class UserInterface
    {
        public static string AskForPlayerName(bool i_IsFirstPlayer)
        {
            string name;

            if (i_IsFirstPlayer)
            {
                ConsoleCommands.AskForUserName();
            }
            else
            {
                ConsoleCommands.AskForUpponentName();
            }

            name = Console.ReadLine();
            while (name.Contains(' ') || name.Length > 20)
            {
                if (name.Contains(" "))
                {
                    ConsoleCommands.SpaceInPlayerName();
                }
                else
                {
                    ConsoleCommands.InvalidPlayerNameLength();
                }

                name = Console.ReadLine();
            }

            return name;
        }

        public static int[] ChooseBoardDimensions()
        {
            int[] boardDimensions = new int[2];
            string boardDimensionsString;
            string[] dimensionsPartsString;
            bool isValidBoardDimensions = false;

            while (!isValidBoardDimensions)
            {
                ConsoleCommands.ChooseBoardDimensionsMsg();
                boardDimensionsString = Console.ReadLine();
                dimensionsPartsString = boardDimensionsString.Split(' ');
                if (dimensionsPartsString.Length != 2)
                {
                    ConsoleCommands.InvalidBoardLength();
                    continue;
                }

                if (!int.TryParse(dimensionsPartsString[0], out boardDimensions[0]))
                {
                    ConsoleCommands.InvalidBoardRowsLength();
                    continue;
                }

                if (!int.TryParse(dimensionsPartsString[1], out boardDimensions[1]))
                {
                    ConsoleCommands.InvalidBoardColumnsLength();
                    continue;
                }

                if (boardDimensions[0] < 4 || boardDimensions[0] > 6 || boardDimensions[1] < 4 || boardDimensions[1] > 6)
                {
                    ConsoleCommands.InvalidBoardRange();
                    continue;
                }

                if ((boardDimensions[0] * boardDimensions[1]) % 2 != 0)
                {
                    ConsoleCommands.OddBoardSizeMsg();
                    continue;
                }

                isValidBoardDimensions = true;
            }

            return boardDimensions;
        }

        public static void GameOverStatus(Player player1, Player player2)
        {
            string winner;

            if (player1.Score > player2.Score)
            {
                winner = player1.Name;
            }
            else if (player1.Score < player2.Score)
            {
                winner = player2.Name;
            }
            else
            {
                winner = "tie";
            }

            ConsoleCommands.GameOverWinnerMsg(winner);
            ConsoleCommands.GameOverStatusMsg(player1, player2);
        }

        public static void GameOver(Player player1, Player player2)
        {
            string choice;

            GameOverStatus(player1, player2);
            ConsoleCommands.PlayAgainMsg();
            choice = Console.ReadLine().ToLower();
            while (choice != "yes" && choice != "no") 
            {
                ConsoleCommands.InvalidPlayAgainChoice();
                ConsoleCommands.PlayAgainMsg();
                choice = Console.ReadLine().ToLower();
            }
                
            if (choice == "yes")
            {
                Screen.Clear();
                MemoryGame.RunGame();
            }

            if (choice == "no")
            {
                ConsoleCommands.ExitMsg();
            }
        }

        public static void QuitImmediatley(Player player1, Player player2)
        {
            ConsoleCommands.QuitMsg();
            GameOverStatus(player1, player2);
        }

        public static int AskForHumanPlayersNumber()
        {
            int humanPlayersCount = -1;
            string humanPlayersCountString;
            bool isValidNumber = false;
            bool isInRange = false;

            ConsoleCommands.AskGameMode();
            while (!isInRange || !isValidNumber)
            {
                humanPlayersCountString = Console.ReadLine();
                isValidNumber = int.TryParse(humanPlayersCountString, out humanPlayersCount);
                isInRange = humanPlayersCount == 1 || humanPlayersCount == 2;
                if (!isValidNumber)
                {
                    ConsoleCommands.InvalidNumberInput();
                }
                else if (!isInRange)
                {
                    ConsoleCommands.InvalidNumberOfPlayers();
                }
                else
                {
                    isInRange = true;
                    isValidNumber = true;
                }
            }

            return humanPlayersCount;
        }
    }
}
