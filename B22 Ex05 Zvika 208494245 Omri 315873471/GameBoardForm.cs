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
        Size m_ButtonSize = new Size(40, 40);
        private int m_GameSize;
        private string m_PlayerOneName, m_PlayerTwoName;
        private int m_PlayerOnePoints = 0, m_PlayerTwoPoints = 0;
        private UpgradedButton[,] m_GameButtons;
        public GameBoardForm(string i_GameSize, string i_PlayerOneName, string i_PlayerTwoName)
        {
            m_GameSize = getBoardSize(i_GameSize);
            setPlayersNames(i_PlayerOneName, i_PlayerTwoName);
            InitializeComponent();
        }

        private void setPlayersNames(string i_PlayerOneName, string i_PlayerTwoName)
        {
            m_PlayerOneName = i_PlayerOneName;
            if (i_PlayerOneName == "")
            {
                m_PlayerOneName = "Player 1";
            }

            m_PlayerTwoName = i_PlayerTwoName;
            if (i_PlayerTwoName == "")
            {
                m_PlayerTwoName = "Player 2:";
            }

        }
        private void GameBoardForm_Load(object sender, EventArgs e)
        {
            initLabels();
            Left = 80;
            Top = 100;
            int newHeight = Top + m_GameSize*m_ButtonSize.Height;
            int newWidth = Left + m_GameSize*m_ButtonSize.Width;
            Size = new Size(newWidth, newHeight);
            m_GameButtons = new UpgradedButton[m_GameSize, m_GameSize];
            initGameBoard();
        }

        private void initGameBoard()
        {
            int left, top = Top/2 - m_GameSize;
            for (int i = 0; i < m_GameSize; i++)
            {
                left = Left/2 - (m_GameSize-1);
                for (int j = 0; j < m_GameSize; j++)
                {
                    m_GameButtons[i, j] = new UpgradedButton(new Point(i,j));
                    m_GameButtons[i, j].Location = new Point(left, top);
                    m_GameButtons[i, j].Size = m_ButtonSize;
                    left += m_ButtonSize.Width;
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
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

                    Controls.Add(m_GameButtons[i, j]);
                }

                top += m_ButtonSize.Height;
            }
        }
        private void initLabels()
        {
            Label playerOne = new Label();
            Label playerTwo = new Label();
            playerOne.Text = m_PlayerOneName + ":" + m_PlayerOnePoints;
            playerTwo.Text = m_PlayerTwoName + ":" + m_PlayerTwoPoints;
            int PlayerTwoLabelLeftMargin = m_GameSize * m_ButtonSize.Width - 2*playerTwo.Text.Length - Left / 10;
            Point playerOnePoint = new Point(Left/10, 15);
            Point playerTwoPoint = new Point(PlayerTwoLabelLeftMargin, 15);
            playerOne.Location = playerOnePoint;
            playerTwo.Location = playerTwoPoint;
            playerOne.AutoSize = true;
            playerTwo.AutoSize = true;
            playerOne.Font = new Font("Arial", 12);
            playerTwo.Font = new Font("Arial", 12);
            Controls.Add(playerOne);
            Controls.Add(playerTwo);
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
