using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sudoku
{
    public partial class Form1 : Form
    {
        private const int m_formWidth = 420;
        private const int m_formHeight = 300;
        private const int m_textBoxWidth = 26;
        private const int m_textBoxHeight = 20;
        private const int m_soDoKuSize = 9;
        private const int m_topSpace = 20;
        private const int m_leftSpace = 50;
        private const int m_splitSpace = 5;
        private TextBox[][] m_allTextBoxes;
        private int[][] m_data;

        public Form1()
        {
            InitializeComponent();
            this.Text = "SoDoKu";
            this.ClientSize = new Size(m_formWidth, m_formHeight);
            InitizlizeTextBoxes();
            m_data = new int[m_soDoKuSize][];
            for (int i = 0; i < m_data.Length; ++i)
            {
                m_data[i] = new int[m_soDoKuSize];
            }
        }

        private void InitizlizeTextBoxes()
        {
            m_allTextBoxes = new TextBox[m_soDoKuSize][];
            for (int i = 0; i < m_allTextBoxes.Length; ++i)
            {
                m_allTextBoxes[i] = new TextBox[m_soDoKuSize];
            }
            for (int i = 0; i < m_allTextBoxes.Length; ++i)
            {
                for (int j = 0; j < m_allTextBoxes[i].Length; ++j)
                {
                    TextBox tb = new TextBox();
                    int x = m_leftSpace + (m_splitSpace + m_textBoxWidth) * j;
                    int y = m_textBoxWidth + (m_splitSpace + m_textBoxHeight) * i;
                    tb.Location = new System.Drawing.Point(x, y);
                    tb.Size = new System.Drawing.Size(m_textBoxWidth, m_textBoxHeight);
                    tb.TextAlign = HorizontalAlignment.Center;
                    this.Controls.Add(tb);
                    m_allTextBoxes[i][j] = tb;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_allTextBoxes.Length; ++i)
            {
                for (int j = 0; j < m_allTextBoxes[i].Length; ++j)
                {
                    m_allTextBoxes[i][j].Text = "";
                    m_allTextBoxes[i][j].BackColor = Color.White;
                }
            }
        }

        private void GetData()
        {
            for (int i = 0; i < m_allTextBoxes.Length; ++i)
            {
                for (int j = 0; j < m_allTextBoxes[i].Length; ++j)
                {
                    try
                    {
                        if (m_allTextBoxes[i][j].Text.Trim() == "")
                        {
                            m_data[i][j] = 0;
                        }
                        else
                        {
                            m_data[i][j] = int.Parse(m_allTextBoxes[i][j].Text.Trim());
                            m_allTextBoxes[i][j].BackColor = Color.Green;
                        } 
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Row " + (i + 1) + ": Col " + (j + 1) + "format uncorret!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetData();
            SoDoKU sdk = new SoDoKU(m_data);
            int[][] rdata = sdk.ShowAnswer();
            if (rdata != null)
            {
                for (int i = 0; i < m_allTextBoxes.Length; ++i)
                {
                    for (int j = 0; j < m_allTextBoxes[i].Length; ++j)
                    {
                        m_allTextBoxes[i][j].Text = rdata[i][j].ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("can not find a anwer!");
            }
        }
    }
}
