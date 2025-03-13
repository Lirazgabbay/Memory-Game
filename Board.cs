using System;
using System.Text;

namespace Ex02
{
    internal class Board<T>
    {
        private readonly int r_Rows;
        private readonly int r_Columns;
        private char[] m_Headers;
        private readonly Cell<T>[,] m_Cells;
        public T[] m_CellValues; 

        public Board(int i_rows, int i_columns, T[] i_CellValues)
        {
            r_Rows = i_rows;
            r_Columns = i_columns;
            m_Cells = new Cell<T>[r_Rows, r_Columns];
            m_CellValues = new T[r_Rows * r_Columns];
            for (int i = 0; i < i_CellValues.Length; i++)
            {
                m_CellValues[i] = i_CellValues[i];
                m_CellValues[m_CellValues.Length - 1 - i] = i_CellValues[i];
            }

            initializeCells();
            initializeHeaders();
        }

        private void initializeHeaders()
        {
            m_Headers = new char[r_Columns];
            for (int i = 0; i < r_Columns; i++)
            {
                m_Headers[i] = (char)('A' + i);
            }
        }

        private void initializeCells()
        {
            int index = 0;

            shuffle(m_CellValues);
            for (int row = 0; row < r_Rows; row++)
            {
                for (int col = 0; col < r_Columns; col++)
                {
                    m_Cells[row, col] = new Cell<T>(m_CellValues[index++]);
                }
            }
        }

        private void shuffle(T[] io_ArrayToBeShuffled)
        {
            Random rand = new Random();
            T temporaryArrayItem;
            int randomIndex;

            for (int i = 0; i < io_ArrayToBeShuffled.Length; i++)
            {
                randomIndex = rand.Next(0, io_ArrayToBeShuffled.Length);
                temporaryArrayItem = io_ArrayToBeShuffled[i];
                io_ArrayToBeShuffled[i] = io_ArrayToBeShuffled[randomIndex];
                io_ArrayToBeShuffled[randomIndex] = temporaryArrayItem;
            }
        }

        public Cell<T>[,] Cells
        {
            get { return m_Cells; }
        }

        public T[] CellValues
        {
            get { return m_CellValues; }
        }

        public int Columns
        {
            get { return r_Columns; }
        }

        public int Rows
        {
            get { return r_Rows; }
        }

        public int NumberOfUnexposedCells()
        {
            int unexposedCells = 0;

            for (int row = 0; row < r_Rows; row++)
            {
                for (int col = 0; col < r_Columns; col++)
                {
                    if (!m_Cells[row, col].IsExposed)
                    {
                        unexposedCells++;
                    }
                }
            }

            return unexposedCells;
        }

        public void DrawBoard()
        {
            StringBuilder table = new StringBuilder();

            table.Append(new string(' ', 4));
            foreach (char header in m_Headers)
            {
                table.Append(header + new string(' ', 3));
            }

            table.Append(Environment.NewLine);
            table.Append(new string(' ', 2) + new string('=', r_Columns * 4 + 1));
            table.Append(Environment.NewLine);
            for (int row = 1; row <= r_Rows; row++)
            {
                table.Append(row + " ");
                table.Append("|");
                for (int col = 0; col < r_Columns; col++)
                {
                    if (m_Cells[row - 1, col].IsExposed)
                    {
                        table.Append(" " + m_Cells[row - 1, col].CellValue + " |");
                    }
                    else
                    {
                        table.Append("   |");
                    }
                }

                table.Append(Environment.NewLine);
                table.Append("  " + new string('=', r_Columns * 4 + 1));
                table.Append(Environment.NewLine);
            }

            Console.Write(table.ToString());
        }
    }
}
