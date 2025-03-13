
namespace Ex02
{
    internal class Player
    {
        private readonly string m_Name;
        private readonly bool m_IsComputer;
        private int m_score;

        public Player(string i_Name, bool i_IsComputer)
        {
            m_Name = i_Name;
            m_IsComputer = i_IsComputer;
            m_score = 0;
        }

        public string Name
        {
            get { return m_Name; }
        }

        public int Score
        {
            get { return m_score; }
            set
            {
                m_score = value;
            }
        }

        public bool IsComputer
        {
            get { return m_IsComputer; }
        }
    }
}
