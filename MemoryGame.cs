using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class MemoryGame
    {
        public static void RunGame()
        {
            Player player1;
            Player player2;
            Move<char> move;
            int humanContinueFlag = 1;
            Board<char> board;
            (int, int) firstSelectionComputer;
            bool computerContinueFlag = true;

            player1 = SetUpPlayer(true);
            player2 = SetUpOpponent();
            board = SetUpBoard();
            move = new Move<char>(board, player1, player2,board.m_CellValues);
            move.BoardGame.DrawBoard();
            while (move.BoardGame.NumberOfUnexposedCells() > 0 && humanContinueFlag != -1)
            {
                humanContinueFlag = 1;
                while (move.BoardGame.NumberOfUnexposedCells() > 0 && humanContinueFlag == 1)
                {
                    ConsoleCommands.TurnMsg(player1.Name);
                    humanContinueFlag = move.CompleteMoveHuman(1);
                    if (humanContinueFlag == -1)
                    {
                        UserInterface.QuitImmediatley(player1, player2);
                        break;
                    }
                }

                if (humanContinueFlag != -1)
                {
                    humanContinueFlag = 1;
                    if (player2.IsComputer)
                    {
                        while (move.BoardGame.NumberOfUnexposedCells() > 0 && computerContinueFlag)
                        {
                            ConsoleCommands.TurnMsg(player2.Name);
                            firstSelectionComputer = move.CompleteFirstMoveComputer();
                            computerContinueFlag = move.CompleteSecondMoveComputer(firstSelectionComputer);
                        }

                        computerContinueFlag = true;
                    }
                    else
                    {
                        while (move.BoardGame.NumberOfUnexposedCells() > 0 && humanContinueFlag == 1)
                        {
                            ConsoleCommands.TurnMsg(player2.Name);
                            humanContinueFlag = move.CompleteMoveHuman(2);
                            if (humanContinueFlag == -1)
                            {
                                UserInterface.QuitImmediatley(player1, player2);
                                break;
                            }
                        }
                    }
                }
            }

            if (humanContinueFlag != -1)
            {
                UserInterface.GameOver(player1, player2);
            }
        }

        private static Player SetUpPlayer(bool i_IsFirstPlayer)
        {
            string playerName = UserInterface.AskForPlayerName(i_IsFirstPlayer);

            return new Player(playerName, false);
        }

        private static Player SetUpOpponent()
        {
            int numberOfPlayers = UserInterface.AskForHumanPlayersNumber();
            Player opponent;

            if (numberOfPlayers == 2)
            {
                opponent = new Player(UserInterface.AskForPlayerName(false), false);
            }
            else
            {
                opponent = new Player("computer", true);
            }

            return opponent;
        }

        private static Board<char> SetUpBoard()
        {
            int[] boardDimensions = UserInterface.ChooseBoardDimensions();
            char [] cellsValue = NumberGenerator(boardDimensions[0], boardDimensions[1]);

            return new Board<char>(boardDimensions[0], boardDimensions[1], cellsValue);
        }

        public static char GenerateRandomValue()
        {
            Random randomNumber = new Random();

            return (char)('A' + randomNumber.Next(0, 26));
        }

        public static char [] NumberGenerator(int i_Rows, int i_Columns)
        {
            List<char> alreadyGeneratedValues = new List<char>();
            char randomValue;

            for (int i = 0; i < i_Columns * i_Rows ; i += 2)
            {
                randomValue = GenerateRandomValue();
                while (alreadyGeneratedValues.Contains(randomValue))
                {
                    randomValue = GenerateRandomValue();
                }

                alreadyGeneratedValues.Add(randomValue);
            }

            return alreadyGeneratedValues.ToArray();
        }
    }
}
