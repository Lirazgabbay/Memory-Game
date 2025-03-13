using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class ComputerPlayer<T>
    {
        private Dictionary<T, (int, int)[]> m_RevealedCells;

        public ComputerPlayer(T[] i_CellContents)
        {
            T cellContent;
            m_RevealedCells = new Dictionary<T, (int, int)[]>();

            for (int i = 0; i < i_CellContents.Length; i++)
            {
                cellContent = i_CellContents[i];
                m_RevealedCells[cellContent] = new (int, int)[2] { (-1, -1), (-1, -1) };
            }
        }

        public Dictionary<T, (int, int)[]> RevealedCells
        {
            get { return m_RevealedCells; }
        }

        public (int, int) ComputerFirstMove(Board<T> i_BoardGame)
        {
            (int, int) firstMove = (-1, -1);

            foreach (var cellContent in m_RevealedCells)
            {
                if (cellContent.Value[0] != (-1, -1) && cellContent.Value[1] != (-1, -1))
                {
                    firstMove = cellContent.Value[0];
                    break;
                }
            }

            if (firstMove == (-1, -1))
            {
                firstMove = generateUnknownCellMove(i_BoardGame);
            }

            return firstMove;
        }

        public (int, int) ComputerSecondMove(Board<T> i_Board, (int, int) i_computerFirstMove)
        {
            (int, int) secondMove = (-1,-1);
            var cellContent = i_Board.Cells[i_computerFirstMove.Item1, i_computerFirstMove.Item2].CellValue;

            if (m_RevealedCells.TryGetValue(cellContent, out var pair))
            {
                if (pair[0] != i_computerFirstMove && pair[0] != (-1, -1))
                {
                    secondMove = pair[0];
                }
                else if (pair[1] != i_computerFirstMove && pair[1] != (-1, -1))
                {
                    secondMove = pair[1];
                }
            }

            if (secondMove == (-1, -1))
            {
                secondMove = generateUnknownCellMove(i_Board);
            }

            return secondMove;
        }

        private (int, int) generateUnknownCellMove(Board<T> i_Board)
        {
            int row;
            int col;
            Random random = new Random();

            do
            {
                row = random.Next(i_Board.Rows);
                col = random.Next(i_Board.Columns);
            } while (i_Board.Cells[row, col].IsExposed || !isCellUnknownToComputer(i_Board.Cells[row, col].CellValue,row,col));

            return (row, col);
        }

        private bool isCellUnknownToComputer(T cell , int i_Row , int i_Col)
        {
            return ((m_RevealedCells[cell][0] != (i_Row, i_Col)) && (m_RevealedCells[cell][1] != (i_Row, i_Col)));
        }
    }
}
