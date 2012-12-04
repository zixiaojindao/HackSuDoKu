using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sudoku
{
    class SoDoKU
    {
        private int[][] m_input;
        private int[][] m_data;
        private int m_size = 9;
        private int m_blockSize = 3;
        private int m_rowBlocks;  
        private bool[][] m_rowCheck;
        private bool[][] m_colCheck;
        private bool[][] m_blockCheck;

        public SoDoKU(int[][] input)
        {
            m_input = input;
            int size = m_size;
            m_rowBlocks = m_size / m_blockSize;
            m_rowCheck = new bool[size][];
            m_colCheck = new bool[size][];
            m_blockCheck = new bool[size][];
            m_data = new int[size][];
            for (int i = 0; i < size; ++i)
            {
                m_rowCheck[i] = new bool[size];
                m_colCheck[i] = new bool[size];
                m_blockCheck[i] = new bool[size];
                m_data[i] = new int[size];
                for (int j = 0; j < size; ++j)
                {
                    m_rowCheck[i][j] = false;
                    m_colCheck[i][j] = false;
                    m_blockCheck[i][j] = false;
                    m_data[i][j] = 0;
                }
                
            }
            if (Certain() == true)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        if (m_input[i][j] != 0)
                        {
                            m_rowCheck[i][m_input[i][j] - 1] = true;
                            m_colCheck[j][m_input[i][j] - 1] = true;
                            int bi = i / m_blockSize;
                            int bj = j / m_blockSize;
                            int bindex = bi * m_rowBlocks + bj;
                            m_blockCheck[bindex][m_input[i][j] - 1] = true;
                        }
                    }
                }
            }
        }

        private bool Calculate(int c)
        {
            if (c == m_size * m_size)
                return true;

            int i = c / m_size;
            int j = c % m_size;
            int bi = i / m_blockSize;
            int bj = j / m_blockSize;
            int bindex = bi * m_rowBlocks + bj;
            if(m_input[i][j] != 0)
            {
                m_data[i][j] = m_input[i][j];
                return Calculate(c + 1);
            }
            for (int k = 1; k <= m_size; ++k)
            {
                if (m_rowCheck[i][k - 1] == false && 
                    m_colCheck[j][k - 1] == false && 
                    m_blockCheck[bindex][k - 1] == false)
                {
                    m_rowCheck[i][k - 1] = true;
                    m_colCheck[j][k - 1] = true;
                    m_blockCheck[bindex][k - 1] = true;
                    m_data[i][j] = k;
                    if (Calculate(c + 1) == true)
                    {
                        return true;
                    }
                    m_rowCheck[i][k - 1] = false;
                    m_colCheck[j][k - 1] = false;
                    m_blockCheck[bindex][k - 1] = false;
                    m_data[i][j] = 0;
                }
            }
            return false;
        }

        private bool Certain()
        {
            if (m_data.Length < m_size)
            {
                return false;
            }
            for (int i = 0; i < m_size; ++i)
            {
                if (m_data[i].Length < m_size)
                {
                    return false;
                }
            }
            for (int i = 0; i < m_size; ++i)
            {
                bool[] check = new bool[m_size];
                for (int j = 0; j < m_size; ++j)
                {
                    if (m_input[i][j] != 0)
                    {
                        if (m_input[i][j] > m_size || m_input[i][j] < 1)
                        {
                            return false;
                        }

                        if (check[m_input[i][j] - 1] == true)
                        {
                            return false;
                        }
                        else
                        {
                            check[m_input[i][j] - 1] = true;
                        }
                    }
                }
            }

            for (int j = 0; j < m_size; ++j)
            {
                bool[] check = new bool[m_size];
                for (int i = 0; i < m_size; ++i)
                {
                    if (m_input[i][j] != 0)
                    {
                        if (m_input[i][j] > m_size || m_input[i][j] < 1)
                        {
                            return false;
                        }

                        if (check[m_input[i][j] - 1] == true)
                        {
                            return false;
                        }
                        else
                        {
                            check[m_input[i][j] - 1] = true;
                        }
                    } 
                }
            }
            return true;
        }

        public int[][] ShowAnswer()
        {
            if (Certain() == false)
            {
                return null;
            }
            if (Calculate(0) == true)
            {
                return m_data;
            }
            return null;
        }
    }
}
