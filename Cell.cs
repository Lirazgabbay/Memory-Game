using System;

namespace Ex02
{
    internal struct Cell<T>
    {
        private readonly T m_CellValue;
        private bool m_IsExposed;

        public Cell(T i_CellValue)
        {
            m_IsExposed = false;
            m_CellValue = i_CellValue;
        }

        public T CellValue
        {
            get { return m_CellValue; }
        }

        public bool IsExposed
        {
            get { return m_IsExposed; }
            set
            {
                m_IsExposed = value;  
            }
        }
    }
}
