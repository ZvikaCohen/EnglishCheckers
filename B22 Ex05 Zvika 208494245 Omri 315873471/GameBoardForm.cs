using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B22_Ex05_Zvika_208494245_Omri_315873471
{
    public partial class GameBoardForm : Form
    {
        private int m_GameSize;
        private Button[,] m_GameButtons;
        public GameBoardForm(string i_GameSize)
        {
            m_GameSize = getBoardSize(i_GameSize);
            InitializeComponent();
        }

        private void GameBoardForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size( 300,   300);
            m_GameButtons = new Button[m_GameSize, m_GameSize];
            int left = 2, top = 2;
            for(int i = 0; i < m_GameSize; i++)
            {
                left = 2;
                for(int j = 0; j < m_GameSize; j++)
                {
                    m_GameButtons[i, j] = new Button();
                    m_GameButtons[i, j].Location = new Point(left, top);
                    m_GameButtons[i, j].Size = new Size(60, 60);
                    left += 60;
                    if((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        m_GameButtons[i, j].Enabled = false;
                    }

                    Controls.Add(m_GameButtons[i,j]);
                }

                top += 60;
            }
        }

        private int getBoardSize(string i_BoardSize)
        {
            int size = 0;
            switch(i_BoardSize)
            {
                case "6x6":
                    {
                        size = 6;
                        break;
                    }
                case "8x8":
                    {
                        size = 8;
                        break;
                    }
                case "10x10":
                    {
                        size = 10;
                        break;
                    }
            }
            return size;
        }
    }
}
