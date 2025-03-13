using System;
using System.Threading;
using Ex02.ConsoleUtils;

namespace Ex02
{
    internal class Move<T>
    {
        private readonly Board<T> m_BoardGame;
        private readonly Player m_Player1;
        private readonly Player m_Player2;
        private readonly ComputerPlayer<T> m_ComputerPlayer;

        public Move(Board<T> i_Board, Player i_Player1, Player i_Player2, T[] i_CellValues)
        {
            m_BoardGame = i_Board;
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_ComputerPlayer = new ComputerPlayer<T>(i_CellValues);

            foreach (T cellValue in m_BoardGame.CellValues)
            {
                m_ComputerPlayer.RevealedCells[cellValue] = new (int, int)[2];
                m_ComputerPlayer.RevealedCells[cellValue][0] = (-1, -1);
                m_ComputerPlayer.RevealedCells[cellValue][1] = (-1, -1);
            }
        }

        public Board<T> BoardGame
        {
            get { return m_BoardGame; }
        }

        public int[] HumanPlayerSelection()
        {
            string humanMove;
            char header;
            char rowNumber;
            int row = -1;
            int col = -1;
            bool isValidMoveFlag = false;
            int[] cellLocation = null;

            while (!isValidMoveFlag)
            {
                ConsoleCommands.AskForHumanMove();
                humanMove = Console.ReadLine();
                if (humanMove == "Q")
                {
                    cellLocation = new int[] { -1, -1 };
                    break;
                }

                if (string.IsNullOrWhiteSpace(humanMove) || humanMove.Length == 0)
                {
                    ConsoleCommands.EmptyhumanMoveMsg();
                    continue;
                }

                if (humanMove.Length != 2)
                {
                    ConsoleCommands.InvalidLengthMoveMsg();
                    continue;
                }

                header = humanMove[0];
                rowNumber = humanMove[1];
                if (!char.IsUpper(header) ||  header < 'A' || header >= 'A' + m_BoardGame.Columns)
                {
                    ConsoleCommands.InvalidHeaderLetterRangeMessage();
                    continue;
                }

                if (!char.IsDigit(rowNumber))
                {
                    ConsoleCommands.PrintNotDigitMessage();
                    continue;
                }

                if (rowNumber < '1' || rowNumber >= '1' + m_BoardGame.Rows)
                {
                    ConsoleCommands.InvalidDigitRangeMessage();
                    continue;
                }

                row = rowNumber - '1';
                col = header - 'A';
                if (m_BoardGame.Cells[row, col].IsExposed)
                {
                    ConsoleCommands.ExposedCellMessage();
                    continue;
                }

                isValidMoveFlag = true;
            }

            if (isValidMoveFlag)
            {
                cellLocation = new int[2] { row, col };
                m_BoardGame.Cells[row, col].IsExposed = true;
            }

            return cellLocation;
        }

        public int CompleteMoveHuman(int playerNumber)
        {
            int[] selection1;
            int[] selection2;
            bool isAMatch = false;
            int result = 0;

            selection1 = HumanPlayerSelection();
            if (selection1[0] == -1 && selection1[1] == -1)
            {
                result = -1;
            }
            else
            {
                m_BoardGame.DrawBoard();
                selection2 = HumanPlayerSelection();
                if (selection2[0] == -1 && selection2[1] == -1)
                {
                    result = -1;
                }
                else
                {
                    if (m_BoardGame.Cells[selection1[0], selection1[1]].CellValue.Equals(m_BoardGame.Cells[selection2[0], selection2[1]].CellValue))
                    {
                        deleteFromComputerMemory(selection1[0], selection1[1]);
                        deleteFromComputerMemory(selection2[0], selection2[1]);
                        m_BoardGame.DrawBoard();
                        if (playerNumber == 1)
                        {
                            m_Player1.Score += 1;
                            ConsoleCommands.ItIsAMatchMsg(m_Player1.Name);
                        }
                        else if (playerNumber == 2)
                        {
                            m_Player2.Score += 1;
                            ConsoleCommands.ItIsAMatchMsg(m_Player2.Name);
                        }

                        isAMatch = true;
                    }
                    else
                    {
                        addToComputerMemory(selection1[0], selection1[1]);
                        addToComputerMemory(selection2[0], selection2[1]);
                        m_BoardGame.DrawBoard();
                        ConsoleCommands.ItIsAMissMatchMsg();
                        Thread.Sleep(2000);
                        Screen.Clear();
                        m_BoardGame.Cells[selection1[0], selection1[1]].IsExposed = false;
                        m_BoardGame.Cells[selection2[0], selection2[1]].IsExposed = false;
                        m_BoardGame.DrawBoard();
                        isAMatch = false;
                    }
                }
            }

            if (result != -1)
            {
                result = isAMatch ? 1 : 0;
            }

            return result;
        }

        public (int, int) CompleteFirstMoveComputer()
        {
            (int, int) firstMove = m_ComputerPlayer.ComputerFirstMove(m_BoardGame);

            m_BoardGame.Cells[firstMove.Item1, firstMove.Item2].IsExposed = true;
            m_BoardGame.DrawBoard();

            return (firstMove.Item1, firstMove.Item2);
        }

        public bool CompleteSecondMoveComputer((int,int) selection1)
        {
            (int, int) secondMove;
            bool isAMatch = false;

            secondMove = m_ComputerPlayer.ComputerSecondMove(m_BoardGame, (selection1.Item1, selection1.Item2));
            m_BoardGame.Cells[secondMove.Item1, secondMove.Item2].IsExposed = true;
            if (m_BoardGame.Cells[selection1.Item1, selection1.Item2].CellValue.Equals(m_BoardGame.Cells[secondMove.Item1, secondMove.Item2].CellValue))
            {
                deleteFromComputerMemory(selection1.Item1, selection1.Item2);
                deleteFromComputerMemory(secondMove.Item1, secondMove.Item2);
                m_BoardGame.DrawBoard();
                ConsoleCommands.ItIsAMatchMsg(m_Player2.Name);
                m_Player2.Score += 1;
                isAMatch = true;
            }
            else
            {
                addToComputerMemory(selection1.Item1, selection1.Item2);
                addToComputerMemory(secondMove.Item1, secondMove.Item2);
                m_BoardGame.DrawBoard();
                ConsoleCommands.ItIsAMissMatchMsg();
                Thread.Sleep(2000);
                Screen.Clear();
                m_BoardGame.Cells[selection1.Item1, selection1.Item2].IsExposed = false;
                m_BoardGame.Cells[secondMove.Item1, secondMove.Item2].IsExposed = false;
                m_BoardGame.DrawBoard();
            }

            return isAMatch;
        }

        private void addToComputerMemory(int row, int col)
        {
            var cellContent = m_BoardGame.Cells[row, col].CellValue;

            if (m_ComputerPlayer.RevealedCells[cellContent][0] == (-1, -1))
            {
                m_ComputerPlayer.RevealedCells[cellContent][0] = (row, col);
            }
            else if (m_ComputerPlayer.RevealedCells[cellContent][1] == (-1, -1))
            {
                m_ComputerPlayer.RevealedCells[cellContent][1] = (row, col);
            }
        }

        private void deleteFromComputerMemory(int row, int col)
        {
            var cellContent = m_BoardGame.Cells[row, col].CellValue;

            if (m_ComputerPlayer.RevealedCells[cellContent][0] == (row, col))
            {
                m_ComputerPlayer.RevealedCells[cellContent][0] = (-1, -1);
            }
            else if (m_ComputerPlayer.RevealedCells[cellContent][1] == (row, col))
            {
                m_ComputerPlayer.RevealedCells[cellContent][1] = (-1, -1);
            }
        }
    }
}
