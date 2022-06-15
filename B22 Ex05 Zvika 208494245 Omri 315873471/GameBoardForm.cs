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
        private PlayersTurn.ePlayersTurn m_CurrentTurn = PlayersTurn.ePlayersTurn.Player1;
        Size m_ButtonSize = new Size(40, 40);
        private int m_GameSize;
        private string m_PlayerOneName, m_PlayerTwoName;
        private int m_PlayerOneCoinsCount, m_PlayerTwoCoinsCount;
        private int m_PlayerOnePoints = 0, m_PlayerTwoPoints = 0;
        private UpgradedButton[,] m_GameButtons;
        private UpgradedButton[] m_Player1CoinSet, m_Player2CoinSet;
        private UpgradedButton m_CurrentPressedButton = null;
        private Label playerOne, playerTwo;

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
                m_PlayerTwoName = "Player 2";
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



        private void buttonClicked(int i_Row, int i_Col)
        {
            if(m_CurrentPressedButton == null && m_GameButtons[i_Row, i_Col].Text != "") // First button press
            {
                markSelectedButton(i_Row, i_Col);
                checkEatingStepsForPlayerButtons();
                showPossibleStepsFromCurrentCoin();
            }

            else if(m_GameButtons[i_Row, i_Col].Text != "") // Else if: There is already a button clicked.
            {
                resetSteps(i_Row, i_Col);
            }

            else // Second button press
            {
                if(stepIsValidAndPossible(i_Row, i_Col))
                {
                    makeStep(i_Row, i_Col);
                    changeTurn();
                    checkAndUpdateWhoCanEatForCurrentPlayer();
                }

                else
                {
                    resetSteps(i_Row, i_Col);
                }
            }

            // button clicked. If it's nothing - don't do anything.
            // If it's someone, check if it's his turn. If yes, mark it.
            // Show possible steps from this coin.
            // If there is an eating possible, show only eating moves.
        }

        private void checkEatingStepsForPlayerButtons()
        {

        }

        private void showPossibleStepsFromCurrentCoin()
        {

        }

        private bool stepIsValidAndPossible(int i_Row, int i_Col)
        {
            return true;
        }

        private void makeStep(int i_NewRow, int i_NewCol)
        {
            m_GameButtons[i_NewRow, i_NewCol].Text = m_CurrentPressedButton.Text;
            m_CurrentPressedButton.Text = "";
            resetSteps(i_NewRow, i_NewCol);

        }
        private void markSelectedButton(int i_Row, int i_Col)
        {
            if(currentButtonPressedIsCurrentPlayer(i_Row, i_Col))
            {
                m_CurrentPressedButton = m_GameButtons[i_Row, i_Col];
                m_CurrentPressedButton.BackColor = Color.FromArgb(100, 200, 0);
            }
        }

        private bool currentButtonPressedIsCurrentPlayer(int i_Row, int i_Col)
        {
            bool answer = ((m_GameButtons[i_Row, i_Col].Text == "X" && m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1)
                           || (m_GameButtons[i_Row, i_Col].Text == "O" && m_CurrentTurn == PlayersTurn.ePlayersTurn.Player2));

            return answer;
        }

        private void resetSteps(int i_Row, int i_Col)
        {
            m_CurrentPressedButton.BackColor = default(Color);
            m_CurrentPressedButton = null;
            markSelectedButton(i_Row, i_Col);
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
                    int copyOfI = i, copyOfJ = j;
                    m_GameButtons[i, j].Click += (sender, e) => buttonClicked(copyOfI, copyOfJ);
                }

                top += m_ButtonSize.Height;
            }
        }
        private void initLabels()
        {
            playerOne = new Label(); 
            playerTwo = new Label();
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
            playerOne.BackColor = Color.Chocolate;
        }

        private void checkAndUpdateWhoCanEatForCurrentPlayer()
        {

        }

        private void changeTurn()
        {
            m_CurrentTurn = m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1
                                ? PlayersTurn.ePlayersTurn.Player2
                                : PlayersTurn.ePlayersTurn.Player1;
            playerOne.BackColor = m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1 ? Color.Chocolate : default;
            playerTwo.BackColor = m_CurrentTurn == PlayersTurn.ePlayersTurn.Player2 ? Color.Chocolate : default;
        }
        private int getBoardSize(string i_BoardSize)
        {
            int size = 0;
            switch(i_BoardSize)
            {
                case "6x6":
                    {
                        m_PlayerOneCoinsCount = m_PlayerTwoCoinsCount = 6;
                        size = 6;
                        break;
                    }
                case "8x8":
                    {
                        m_PlayerOneCoinsCount = m_PlayerTwoCoinsCount = 12;
                        size = 8;
                        break;
                    }
                case "10x10":
                    {
                        m_PlayerOneCoinsCount = m_PlayerTwoCoinsCount = 20;
                        size = 10;
                        break;
                    }
            }

            return size;
        }
    }
}
