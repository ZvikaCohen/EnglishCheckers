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
        private Size m_ButtonSize = new Size(40, 40);
        private int m_GameSize;
        private string m_PlayerOneName, m_PlayerTwoName;
        private int m_PlayerOneCoinsCount, m_PlayerTwoCoinsCount;
        private int m_PlayerOnePoints = 0, m_PlayerTwoPoints = 0;
        private UpgradedButton[,] m_GameButtons;
        private List<UpgradedButton> m_Player1CoinSet = new List<UpgradedButton>();
        private List<UpgradedButton> m_Player2CoinSet = new List<UpgradedButton>();
        private UpgradedButton m_CurrentPressedButton = null;
        private Label m_PlayerOne, m_PlayerTwo;
        private Color m_ValidSpotColor = Color.BurlyWood, m_CurrentPlayerColor = Color.BurlyWood;
        private bool m_EatingIsPossible = false;

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

        private void formatPlayersCoinsArrays()
        {
            for(int i = m_Player1CoinSet.Count-1; i >= 0; i--)
            {
                m_Player1CoinSet.Remove(m_Player1CoinSet[i]);
            }
            

            for (int i = m_Player2CoinSet.Count - 1; i >= 0 ; i--)
            {
                m_Player2CoinSet.Remove(m_Player2CoinSet[i]);
            }
        }

        private void updatePlayerCoinsArrays()
        {
            formatPlayersCoinsArrays();
            for(int i = 0; i < m_GameSize; i++)
            {
                for(int j = 0; j < m_GameSize; j++)
                {
                    if (i == 0 && m_GameButtons[i,j].Text == "X")
                    {
                        m_GameButtons[i, j].Text = "K";
                    }

                    if (i == m_GameSize-1 && m_GameButtons[i, j].Text == "O")
                    {
                        m_GameButtons[i, j].Text = "U";
                    }

                    if (m_GameButtons[i, j].Text == "X" || m_GameButtons[i, j].Text == "K")
                    {
                        m_Player1CoinSet.Add(m_GameButtons[i,j]);
                    }

                    else if(m_GameButtons[i, j].Text == "O" || m_GameButtons[i, j].Text == "U")
                    {
                        m_Player2CoinSet.Add(m_GameButtons[i,j]);
                    }
                }
            }
        }

        private void buttonClicked(int i_Row, int i_Col)
        {
            checkAndUpdateWhoCanEatForCurrentPlayer();
            if (m_CurrentPressedButton == null && m_GameButtons[i_Row, i_Col].Text != "") // First button press
            {
                markSelectedButton(i_Row, i_Col);
              //  checkAndUpdateWhoCanEatForCurrentPlayer();
            }

            else if(m_GameButtons[i_Row, i_Col].Text != "") // Else if: There is already a button clicked.
            {
                resetSteps(i_Row, i_Col);
                markSelectedButton(i_Row, i_Col);
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
                    markSelectedButton(i_Row,i_Col);
                }
            }
            updatePlayerCoinsArrays();
            // button clicked. If it's nothing - don't do anything.
            // If it's someone, check if it's his turn. If yes, mark it.
            // Show possible steps from this coin.
            // If there is an eating possible, show only eating moves.
        }

        private void showPossibleStepsFromCurrentCoin(int i_CurrentRow, int i_CurrentCol)
        {
            // Check if player has steps to eat with. If yes, mark the eating steps.
            if(m_EatingIsPossible)
            {
                if(m_GameButtons[i_CurrentRow, i_CurrentCol].m_CanEatUpRight)
                {
                    markUpRight(i_CurrentRow-1, i_CurrentCol + 1);
                }

                if (m_GameButtons[i_CurrentRow, i_CurrentCol].m_CanEatUpLeft)
                {
                    markUpLeft(i_CurrentRow-1, i_CurrentCol - 1);
                }

                if (m_GameButtons[i_CurrentRow, i_CurrentCol].m_CanEatDownRight)
                {
                    markDownRight(i_CurrentRow+1, i_CurrentCol + 1);
                }

                if (m_GameButtons[i_CurrentRow, i_CurrentCol].m_CanEatDownLeft)
                {
                    markDownLeft(i_CurrentRow+1, i_CurrentCol-1);
                }
            }
            else
            {
                // If not:
                if(m_GameButtons[i_CurrentRow, i_CurrentCol].Text == "X") // Player 1
                {
                    markUpLeft(i_CurrentRow, i_CurrentCol);
                    markUpRight(i_CurrentRow, i_CurrentCol);
                }
                else if(m_GameButtons[i_CurrentRow, i_CurrentCol].Text == "O") // Player 2
                {
                    markDownLeft(i_CurrentRow, i_CurrentCol);
                    markDownRight(i_CurrentRow, i_CurrentCol);
                }
                else // King
                {
                    markUpLeft(i_CurrentRow, i_CurrentCol);
                    markUpRight(i_CurrentRow, i_CurrentCol);
                    markDownLeft(i_CurrentRow, i_CurrentCol);
                    markDownRight(i_CurrentRow, i_CurrentCol);
                }
            }
        }

        private void markUpLeft(int i_Row, int i_Col)
        {
            if(i_Row - 1 > 0 && i_Col - 1 >= 0 && m_GameButtons[i_Row-1, i_Col-1].Text == "")
            {
                m_GameButtons[i_Row - 1, i_Col - 1].BackColor = m_ValidSpotColor;
            }
        }

        private void markDownLeft(int i_Row, int i_Col)
        {
            if (i_Row + 1 < m_GameSize && i_Col - 1 >= 0 && m_GameButtons[i_Row + 1, i_Col - 1].Text == "")
            {
                m_GameButtons[i_Row + 1, i_Col - 1].BackColor = m_ValidSpotColor;
            }
        }

        private void markUpRight(int i_Row, int i_Col)
        {
            if (i_Row - 1 >= 0 && i_Col + 1 < m_GameSize && m_GameButtons[i_Row - 1, i_Col + 1].Text == "")
            {
                m_GameButtons[i_Row - 1, i_Col + 1].BackColor = m_ValidSpotColor;
            }
        }

        private void markDownRight(int i_Row, int i_Col)
        {
            if (i_Row + 1 < m_GameSize && i_Col + 1 < m_GameSize && m_GameButtons[i_Row + 1, i_Col + 1].Text == "")
            {
                m_GameButtons[i_Row + 1, i_Col + 1].BackColor = m_ValidSpotColor;
            }
        }
        private bool stepIsValidAndPossible(int i_Row, int i_Col)
        {
            bool answer = false;
            UpgradedButton pressedButton = m_GameButtons[i_Row, i_Col];
            if(pressedButton.BackColor == m_ValidSpotColor)
            {
                answer = true;
            }

            return answer;
        }

        private void makeStep(int i_NewRow, int i_NewCol)
        {
            if(m_EatingIsPossible) // Make eating step
            {
                makeEatingStep(i_NewRow, i_NewCol);
            }

            m_GameButtons[i_NewRow, i_NewCol].Text = m_CurrentPressedButton.Text;
            m_CurrentPressedButton.Text = "";
            resetSteps(i_NewRow, i_NewCol);
        }

        private void makeEatingStep(int i_NewRow, int i_NewCol)
        {
            int oldRow = m_CurrentPressedButton.m_PositionOnBoard.X;
            int oldCol = m_CurrentPressedButton.m_PositionOnBoard.Y;
            if(oldRow - i_NewRow == 2 && oldCol - i_NewCol == 2) // UpLeft
            {
                m_GameButtons[oldRow - 1, oldCol - 1].Text = "";
            }
            if (oldRow - i_NewRow == 2 && oldCol - i_NewCol == -2) // UpRight
            {
                m_GameButtons[oldRow - 1, oldCol + 1].Text = "";
            }
            if (oldRow - i_NewRow == -2 && oldCol - i_NewCol == 2) // DownLeft
            {
                m_GameButtons[oldRow + 1, oldCol - 1].Text = "";
            }
            if (oldRow - i_NewRow == -2 && oldCol - i_NewCol == -2) // DownRight
            {
                m_GameButtons[oldRow + 1, oldCol + 1].Text = "";
            }
        }

        private void markSelectedButton(int i_Row, int i_Col)
        {
            if(currentButtonPressedIsCurrentPlayer(i_Row, i_Col))
            {
                m_CurrentPressedButton = m_GameButtons[i_Row, i_Col];
                m_CurrentPressedButton.BackColor = Color.FromArgb(100, 200, 0);
                showPossibleStepsFromCurrentCoin(i_Row, i_Col);
            }
        }

        private bool currentButtonPressedIsCurrentPlayer(int i_Row, int i_Col)
        {
            bool answer = (((m_GameButtons[i_Row, i_Col].Text == "X" || m_GameButtons[i_Row, i_Col].Text == "K") && m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1)
                           || ((m_GameButtons[i_Row, i_Col].Text == "O" || m_GameButtons[i_Row, i_Col].Text == "U") && m_CurrentTurn == PlayersTurn.ePlayersTurn.Player2));

            return answer;
        }

        private void resetSteps(int i_Row, int i_Col)
        {
            if(m_CurrentPressedButton != null)
            {
                m_CurrentPressedButton.BackColor = default(Color);
            }
            m_CurrentPressedButton = null;
            for (int i = 0; i < m_GameSize; i++)
            {
                for (int j = 0; j < m_GameSize; j++)
                {
                    if (m_GameButtons[i, j].Enabled)
                    {
                        m_GameButtons[i, j].BackColor = default(Color);
                    }
                }
            }
        }


        private void initGameBoard()
        {
            int left, top = (Top/2 - m_GameSize), coinsCount1=0, coinsCount2=0;

            //m_Player1CoinSet.Capacity = (m_GameSize / 2) * (m_GameSize / 2 - 1);
           // m_Player2CoinSet.Capacity = (m_GameSize / 2) * (m_GameSize / 2 - 1);

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
                    else
                    {
                        m_GameButtons[i, j].BackColor = default(Color);
                    }

                    if ((i < (m_GameSize / 2) - 1) && ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)))
                    {
                        m_GameButtons[i, j].Text = "O";
                       // m_Player2CoinSet.Add(m_GameButtons[i, j]);
                    }

                    else if (i > (m_GameSize / 2) && ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)))
                    {
                        m_GameButtons[i, j].Text = "X";
                      //  m_Player1CoinSet.Add(m_GameButtons[i, j]);
                    }

                    m_GameButtons[i, j].m_PositionOnBoard = new Point(i, j);
                    Controls.Add(m_GameButtons[i, j]);
                    int copyOfI = i, copyOfJ = j;
                    m_GameButtons[i, j].Click += (sender, e) => buttonClicked(copyOfI, copyOfJ);
                }

                top += m_ButtonSize.Height;
            }
        }
        private void initLabels()
        {
            m_PlayerOne = new Label();
            m_PlayerTwo = new Label();
            m_PlayerOne.Text = m_PlayerOneName + ":" + m_PlayerOnePoints;
            m_PlayerTwo.Text = m_PlayerTwoName + ":" + m_PlayerTwoPoints;
            int playerTwoLabelLeftMargin = m_GameSize * m_ButtonSize.Width - 2* m_PlayerTwo.Text.Length - Left / 10;
            Point playerOnePoint = new Point(Left/10, 15);
            Point playerTwoPoint = new Point(playerTwoLabelLeftMargin, 15);
            m_PlayerOne.Location = playerOnePoint;
            m_PlayerTwo.Location = playerTwoPoint;
            m_PlayerOne.AutoSize = true;
            m_PlayerTwo.AutoSize = true;
            m_PlayerOne.Font = new Font("Arial", 12);
            m_PlayerTwo.Font = new Font("Arial", 12);
            Controls.Add(m_PlayerOne);
            Controls.Add(m_PlayerTwo);
            m_PlayerOne.BackColor = m_CurrentPlayerColor;
        }

        private void resetCoinPossibleEatings(UpgradedButton i_Button)
        {
            i_Button.m_CanEatDownLeft = false;
            i_Button.m_CanEatDownRight = false;
            i_Button.m_CanEatUpLeft = false;
            i_Button.m_CanEatUpRight = false;
        }
        private void checkAndUpdateWhoCanEatForCurrentPlayer()
        {
            m_EatingIsPossible = false;
            if (m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1)
            {
                foreach (UpgradedButton Coin in m_Player1CoinSet)
                {
                    resetCoinPossibleEatings(Coin);
                    canCoinEat(Coin);
                }
            }
            else
            {
                foreach (UpgradedButton Coin in m_Player2CoinSet)
                {
                    resetCoinPossibleEatings(Coin);
                    canCoinEat(Coin);
                }
            }
        }

        private void canCoinEat(UpgradedButton i_Coin)
        {
            int coinRow = i_Coin.m_PositionOnBoard.X, coinCol = i_Coin.m_PositionOnBoard.Y;
            if(i_Coin.Text == "X" || i_Coin.Text == "K" || i_Coin.Text == "U")
            {
                if(coinRow >= 2 && coinCol < m_GameSize - 2
                                && checkIfNextPositionIsEnemy(i_Coin, coinRow - 1, coinCol + 1)
                                && m_GameButtons[coinRow - 2, coinCol + 2].Text == "")
                {
                    m_EatingIsPossible = true;
                    i_Coin.m_CanEatUpRight = true;
                }

                if(coinRow >= 2 && coinCol >= 2 && checkIfNextPositionIsEnemy(i_Coin, coinRow - 1, coinCol - 1)
                   && m_GameButtons[coinRow - 2, coinCol - 2].Text == "")
                {
                    m_EatingIsPossible = true;
                    i_Coin.m_CanEatUpLeft = true;
                }
            }

            if(i_Coin.Text == "O" || i_Coin.Text == "U" || i_Coin.Text == "K")
            {
                if(coinRow < m_GameSize - 2 && coinCol < m_GameSize - 2
                                            && checkIfNextPositionIsEnemy(i_Coin, coinRow + 1, coinCol + 1)
                                            && m_GameButtons[coinRow + 2, coinCol + 2].Text == "")
                {
                    m_EatingIsPossible = true;
                    i_Coin.m_CanEatDownRight = true;
                }

                if (coinRow < m_GameSize - 2 && coinCol >= 2 && checkIfNextPositionIsEnemy(i_Coin, coinRow+1, coinCol-1) && m_GameButtons[coinRow + 2, coinCol - 2].Text == "")
                {
                    m_EatingIsPossible = true;
                    i_Coin.m_CanEatDownLeft = true;
                }
            }
        }

        private bool checkIfNextPositionIsEnemy(UpgradedButton i_CurrentButton, int i_NewRow, int i_NewCol)
        {
            bool answer = false;
            string oppositePlayer = "", oppositePlayerKing = "";
            switch(i_CurrentButton.Text)
            {
                case "X":
                    {
                        oppositePlayer = "O";
                        oppositePlayerKing = "U";
                        break;
                    }
                case "K":
                    {
                        oppositePlayer = "O";
                        oppositePlayerKing = "U";
                        break;
                    }
                case "O":
                    {
                        oppositePlayer = "X";
                        oppositePlayerKing = "K";
                        break;
                    }
                case "U":
                    {
                        oppositePlayer = "X";
                        oppositePlayerKing = "K";
                        break;
                    }
            }

            if(m_GameButtons[i_NewRow, i_NewCol].Text == oppositePlayer
               || m_GameButtons[i_NewRow, i_NewCol].Text == oppositePlayerKing)
            {
                answer = true;
            }

            return answer;
        }




        private void changeTurn()
        {
            m_CurrentTurn = m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1
                                ? PlayersTurn.ePlayersTurn.Player2
                                : PlayersTurn.ePlayersTurn.Player1;
            m_PlayerOne.BackColor = m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1 ? m_CurrentPlayerColor : default;
            m_PlayerTwo.BackColor = m_CurrentTurn == PlayersTurn.ePlayersTurn.Player2 ? m_CurrentPlayerColor : default;
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
