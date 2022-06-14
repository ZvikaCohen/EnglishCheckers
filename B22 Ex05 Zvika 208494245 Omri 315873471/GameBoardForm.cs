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
            CenterToScreen();
            Size buttonSize = new Size(40, 40);
            this.Left = 10;
            this.Top = 100;
            int newHeight = Top + 40 +m_GameSize*buttonSize.Height;
            int newWidth = Left*2+m_GameSize*buttonSize.Width;
            this.Size = new Size(newWidth, newHeight);
            m_GameButtons = new Button[m_GameSize, m_GameSize];
            int left = Left, top = Top;
            for(int i = 0; i < m_GameSize; i++)
            {
                left = 4;
                for(int j = 0; j < m_GameSize; j++)
                {
                    m_GameButtons[i, j] = new Button();
                    m_GameButtons[i, j].Location = new Point(left, top);
                    m_GameButtons[i, j].Size = buttonSize;
                    left += buttonSize.Width;
                    if((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        m_GameButtons[i, j].Enabled = false;
                    }

                    if ((i < (m_GameSize / 2) - 1) && ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)))
                    {
                        m_GameButtons[i, j].Text = "O";
                    }
                    else if (i > (m_GameSize / 2) && ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)))
                    {
                        m_GameButtons[i, j].Text = "X";
                    }

                    Controls.Add(m_GameButtons[i,j]);
                }

                top += buttonSize.Height;
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
