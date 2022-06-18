using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace B22_Ex05_Zvika_208494245_Omri_315873471
{
    public partial class GameBoardForm : Form
    {
        private List<UpgradedButton> m_Player1CoinSet = new List<UpgradedButton>();
        private List<UpgradedButton> m_Player2CoinSet = new List<UpgradedButton>();
        private List<UpgradedButton> m_ComputerNextMoveButtons = new List<UpgradedButton>();
        private UpgradedButton[,] m_GameButtons;
        private UpgradedButton m_CurrentPressedButton = null;
        private UpgradedButton m_SecondEatingButton = null;
        private PlayersTurn.ePlayersTurn m_CurrentTurn = PlayersTurn.ePlayersTurn.Player1;
        private Label m_PlayerOne, m_PlayerTwo;
        private Color m_ValidSpotColor = Color.BurlyWood, m_CurrentPlayerColor = Color.BurlyWood;
        private Size m_ButtonSize = new Size(40, 40);
        private bool m_EatingIsPossible = false;
        private bool m_SecondEatingStep = false;
        private bool m_ComputerIsSecondPlayer = false;
        private int m_GameSize;
        private int m_PlayerOneCoinsCount, m_PlayerTwoCoinsCount;
        private int m_Player1Points = 0, m_Player2Points = 0;
        private string m_PlayerOneName, m_PlayerTwoName;
        private bool m_P1OutOfMoves = false, m_P2OutOfMoves = false, m_Tie = false;
        private PlayersTurn.ePlayersTurn m_Winner;


        public GameBoardForm(
            string i_GameSize,
            string i_PlayerOneName,
            string i_PlayerTwoName,
            bool i_SecondPlayerIsComputer)
        {
            m_ComputerIsSecondPlayer = i_SecondPlayerIsComputer;
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
            resetBoard();
        }

        private void formatPlayersCoinsArrays()
        {
            for (int i = m_Player1CoinSet.Count - 1; i >= 0; i--)
            {
                resetCoinPossibleEatingsAndMoves(m_Player1CoinSet[i]);
                m_Player1CoinSet.Remove(m_Player1CoinSet[i]);
            }

            for (int i = m_Player2CoinSet.Count - 1; i >= 0; i--)
            {
                resetCoinPossibleEatingsAndMoves(m_Player2CoinSet[i]);
                m_Player2CoinSet.Remove(m_Player2CoinSet[i]);
            }
        }

        private void updatePlayerCoinsArrays()
        {
            formatPlayersCoinsArrays();
            for (int i = 0; i < m_GameSize; i++)
            {
                for (int j = 0; j < m_GameSize; j++)
                {
                    if (i == 0 && m_GameButtons[i, j].Text == "X")
                    {
                        m_GameButtons[i, j].Text = "K";
                    }

                    if (i == m_GameSize - 1 && m_GameButtons[i, j].Text == "O")
                    {
                        m_GameButtons[i, j].Text = "U";
                    }

                    if (m_GameButtons[i, j].Text == "X" || m_GameButtons[i, j].Text == "K")
                    {
                        m_Player1CoinSet.Add(m_GameButtons[i, j]);
                    }

                    else if (m_GameButtons[i, j].Text == "O" || m_GameButtons[i, j].Text == "U")
                    {
                        m_Player2CoinSet.Add(m_GameButtons[i, j]);
                    }
                }
            }
        }

        private void checkIfPlayerOutOfMoves()  /// this need to be fixed -- sayes that in the initial board there are no moves to make
        {
            int p1moveCount = 0, p2moveCount = 0;

            if (m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1)
            {
                foreach (UpgradedButton Coin in m_Player1CoinSet)
                {
                    if (Coin.canMove)
                    {
                        p1moveCount++;
                    }
                }
                if (p1moveCount == 0)
                {
                    m_P1OutOfMoves = true;
                }
            }

            else
            {
                foreach (UpgradedButton Coin in m_Player2CoinSet)
                {
                    if (Coin.canMove)
                    {
                        p2moveCount++;
                    }
                }

                if (p2moveCount == 0)
                {
                    m_P2OutOfMoves = true;
                }
            }
        }

        private bool gameOver()
        {
            bool over = false;

            if (m_P1OutOfMoves && !m_P2OutOfMoves)
            {
                m_Winner = PlayersTurn.ePlayersTurn.Player2;
                over = true;
            }
            else if (!m_P1OutOfMoves && m_P2OutOfMoves)
            {
                m_Winner = PlayersTurn.ePlayersTurn.Player1;
                over = true;
            }
            else if (m_P1OutOfMoves && m_P2OutOfMoves)
            {
                m_Tie = true;
                over = true;
            }

            return over;
        }

        private void tieMessage()
        {
            string message = string.Format("Tie! {0} Another round?", Environment.NewLine);
            string caption = "Tie!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                initGameBoard();
            }
            else
            {
                this.Close();
            }
        }

        private void winnerMessage()
        {
            if (m_Winner == PlayersTurn.ePlayersTurn.Player1)
            {
                m_Player1Points++;
            }
            else
            {
                m_Player2Points++;
            }

            string message = string.Format("{0} is the winner! {1} Another round?",m_Winner ,Environment.NewLine);
            string caption = "Winner winner chicken dinner!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Controls.Clear();
                resetBoard();

            }
            else
            {
                this.Close();
            }
        }

        void resetBoard()
        {
            Left = 80;
            Top = 100;
            initLabels();
            int newHeight = Top + m_GameSize * m_ButtonSize.Height;
            int newWidth = Left + m_GameSize * m_ButtonSize.Width;
            Size = new Size(newWidth, newHeight);
            m_GameButtons = new UpgradedButton[m_GameSize, m_GameSize];
            initGameBoard();
        }

        private void buttonClicked(int i_Row, int i_Col)
        {
            if (m_CurrentPressedButton == null && m_GameButtons[i_Row, i_Col].Text != "")
            {
                markSelectedButton(i_Row, i_Col);
            }

            else if (m_GameButtons[i_Row, i_Col].Text != "") 
            {
                resetSteps(i_Row, i_Col);
                markSelectedButton(i_Row, i_Col);
            }

            else
            {
                if (stepIsValidAndPossible(i_Row, i_Col))
                {
                    makeStep(i_Row, i_Col);
                    if(!m_SecondEatingStep)
                    {
                        changeTurn();
                    }

                    else
                    {
                        m_SecondEatingStep = false;
                        checkAndUpdateWhoCanEatForCurrentPlayer();
                        markSelectedButton(i_Row, i_Col);
                        showPossibleStepsFromCurrentCoin(i_Row,i_Col);
                        m_SecondEatingButton = m_GameButtons[i_Row, i_Col];
                    }

                    checkAndUpdateWhoCanEatForCurrentPlayer();
                }

                else
                {
                    resetSteps(i_Row, i_Col);
                    markSelectedButton(i_Row, i_Col);
                }

                if(m_ComputerIsSecondPlayer && m_CurrentTurn == PlayersTurn.ePlayersTurn.Player2)
                {
                    updatePlayerCoinsArrays();
                    checkAndUpdateWhoCanEatForCurrentPlayer();
                    makeComputerMove();
                    changeTurn();
                }
            }

            updatePlayerCoinsArrays();
            checkIfGameOverAndAnnounceWinOrTie();
        }

        void checkIfGameOverAndAnnounceWinOrTie()
        {
            checkAndUpdateWhoCanEatForCurrentPlayer();
            checkAndUpdateWhoCanMove(m_CurrentTurn);
            checkIfPlayerOutOfMoves();
            checkIfPlayerOutOfCoins();

            if (gameOver())
            {
                if (m_Tie)
                {
                    tieMessage();
                }
                else
                {
                    winnerMessage();
                }
            }
        }

        private void checkIfPlayerOutOfCoins()
        {
            m_P1OutOfMoves = m_Player1CoinSet.Count == 0;
            m_P2OutOfMoves = m_Player2CoinSet.Count == 0;

            //if (m_Player1CoinSet.Count == 0)
            //{
            //    m_P1OutOfMoves = true;
            //}
            //else
            //{
            //    m_P1OutOfMoves = false;
            //}

            //if (m_Player2CoinSet.Count == 0)
            //{
            //    m_P2OutOfMoves = true;
            //}
        }

        private void makeComputerMove()
        {
            Random rnd = new Random();
            UpgradedButton buttonToMove = null;
            int randomCoinIndex = 0;
            updateComputerEatingsArray();
            if(m_EatingIsPossible)
            {
                randomCoinIndex = rnd.Next(0, m_ComputerNextMoveButtons.Count);
            }

            else
            {
                updateComputerPossibleSteps();
                randomCoinIndex = rnd.Next(0, m_ComputerNextMoveButtons.Count);
            }
            if (m_ComputerNextMoveButtons.Count != 0)
            {
                buttonToMove = m_ComputerNextMoveButtons[randomCoinIndex];
                m_CurrentPressedButton = buttonToMove;
                getNewRowAndColForComputer(ref buttonToMove, out int newRow, out int newCol);

                if (m_EatingIsPossible)
                {
                    makeEatingStep(buttonToMove, newRow, newCol);
                }

                m_GameButtons[newRow, newCol].Text = buttonToMove.Text;
                buttonToMove.Text = "";
                resetSteps(newRow, newCol);
            }
        }

        private void getNewRowAndColForComputer(ref UpgradedButton i_Button, out int o_NewRow, out int o_NewCol)
        {
            o_NewRow = o_NewCol = 0;
            // int possibleStepsCounter = getPossibleStepsCounter(i_Button);
            List<Direction.eDirection> directionsList = new List<Direction.eDirection>();
            fillDirectionsListAndGetNewRowAndCol(i_Button, ref directionsList, out o_NewRow, out o_NewCol);
        }

        private void fillDirectionsListAndGetNewRowAndCol(
            UpgradedButton i_Button,
            ref List<Direction.eDirection> i_ListOfDirections,
            out int o_NewRow,
            out int o_NewCol)
        {
            Random rnd = new Random();
            int randomIndex = 0, eatCounter = 0;
            o_NewRow = o_NewCol = 0;
            if(i_Button.m_CanEatDownLeft)
            {
                eatCounter++;
                i_ListOfDirections.Add(Direction.eDirection.DownLeft);
            }

            if(i_Button.m_CanEatDownRight)
            {
                eatCounter++;
                i_ListOfDirections.Add(Direction.eDirection.DownRight);
            }

            if(i_Button.m_CanEatUpLeft)
            {
                eatCounter++;
                i_ListOfDirections.Add(Direction.eDirection.UpLeft);
            }

            if(i_Button.m_CanEatUpRight)
            {
                eatCounter++;
                i_ListOfDirections.Add(Direction.eDirection.UpRight);
            }

            if(i_Button.m_CanMoveDownLeft)
            {
                i_ListOfDirections.Add(Direction.eDirection.DownLeft);
            }

            if(i_Button.m_CanMoveDownRight)
            {
                i_ListOfDirections.Add(Direction.eDirection.DownRight);
            }

            if(i_Button.m_CanMoveUpLeft)
            {
                i_ListOfDirections.Add(Direction.eDirection.UpLeft);
            }

            if(i_Button.m_CanMoveUpRight)
            {
                i_ListOfDirections.Add(Direction.eDirection.UpRight);
            }

            if(m_EatingIsPossible)
            {
                randomIndex = rnd.Next(0, eatCounter);
            }

            else
            {
                randomIndex = rnd.Next(0, i_ListOfDirections.Count);
            }

            translateDirectionToRowAndCol(
                i_ListOfDirections[randomIndex],
                i_Button,
                out o_NewRow,
                out o_NewCol);
        }

        private void translateDirectionToRowAndCol(
            Direction.eDirection i_Direction,
            UpgradedButton i_Button,
            out int o_NewRow,
            out int o_NewCol)
        {
            o_NewRow = o_NewCol = 0;
            int oldRow = i_Button.m_PositionOnBoard.X;
            int oldCol = i_Button.m_PositionOnBoard.Y;
            int difference = m_EatingIsPossible ? 2 : 1;
            switch(i_Direction)
            {
                case Direction.eDirection.DownLeft:
                    {
                        o_NewRow = oldRow + difference;
                        o_NewCol = oldCol - difference;
                        break;
                    }
                case Direction.eDirection.DownRight:
                    {
                        o_NewRow = oldRow + difference;
                        o_NewCol = oldCol + difference;
                        break;
                    }
                case Direction.eDirection.UpLeft:
                    {
                        o_NewRow = oldRow - difference;
                        o_NewCol = oldCol - difference;
                        break;
                    }
                case Direction.eDirection.UpRight:
                    {
                        o_NewRow = oldRow - difference;
                        o_NewCol = oldCol + difference;
                        break;
                    }
            }
        }

        private void updateComputerEatingsArray()
        {
            for(int i = m_ComputerNextMoveButtons.Count - 1; i >= 0; i--)
            {
                m_ComputerNextMoveButtons.Remove(m_ComputerNextMoveButtons[i]);
            }

            updatePlayerCoinsArrays();
            checkAndUpdateWhoCanEatForCurrentPlayer();

            foreach(UpgradedButton Coin in m_Player2CoinSet)
            {
                if(Coin.m_CanEatDownLeft || Coin.m_CanEatDownRight || Coin.m_CanEatUpLeft || Coin.m_CanEatUpRight)
                {
                    m_ComputerNextMoveButtons.Add(Coin);
                }
            }
        }

        private void updateComputerPossibleSteps()
        {
            for (int i = m_ComputerNextMoveButtons.Count - 1; i >= 0; i--) // First, reset the old array.
            {
                m_ComputerNextMoveButtons.Remove(m_ComputerNextMoveButtons[i]);
            }

            resetCheckAndUpdateWhoCanMoveForComputerPlayer();

            foreach (UpgradedButton Coin in m_Player2CoinSet)
            {
                if (Coin.m_CanMoveDownLeft || Coin.m_CanMoveDownRight || Coin.m_CanMoveUpLeft || Coin.m_CanMoveUpRight)
                {
                    m_ComputerNextMoveButtons.Add(Coin);
                }
            }

        }

        private void showPossibleStepsFromCurrentCoin(int i_CurrentRow, int i_CurrentCol)
        {
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
            if(i_Row - 1 >= 0 && i_Col - 1 >= 0 && m_GameButtons[i_Row-1, i_Col-1].Text == "")
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
            if(m_SecondEatingStep)
            {
                if (pressedButton.m_PositionOnBoard == m_SecondEatingButton.m_PositionOnBoard)
                {
                    answer = true;
                }
            }

            else if(pressedButton.BackColor == m_ValidSpotColor)
            {
                answer = true;
            }

            return answer;
        }

        private void makeStep(int i_NewRow, int i_NewCol)
        {
            if(m_EatingIsPossible)
            {
                makeEatingStep(m_CurrentPressedButton, i_NewRow, i_NewCol);
            }

            m_GameButtons[i_NewRow, i_NewCol].Text = m_CurrentPressedButton.Text;
            m_CurrentPressedButton.Text = "";
            if(m_EatingIsPossible)
            {
                updatePlayerCoinsArrays();
                checkAndUpdateWhoCanEatForCurrentPlayer();
                if (m_GameButtons[i_NewRow, i_NewCol].m_CanEatDownLeft || m_GameButtons[i_NewRow, i_NewCol].m_CanEatDownRight
                                                                       || m_GameButtons[i_NewRow, i_NewCol].m_CanEatUpLeft
                                                                       || m_GameButtons[i_NewRow, i_NewCol].m_CanEatUpRight)
                {
                    m_SecondEatingStep = true;
                }
            }
            
            resetSteps(i_NewRow, i_NewCol);
        }

        private void makeEatingStep(UpgradedButton i_Button, int i_NewRow, int i_NewCol)
        {
            int oldRow = 0, oldCol = 0;
            if(m_CurrentTurn == PlayersTurn.ePlayersTurn.Player2 && m_ComputerIsSecondPlayer)
            {
                oldRow = i_Button.m_PositionOnBoard.X;
                oldCol = i_Button.m_PositionOnBoard.Y;
            }

            else
            {
                oldRow = m_CurrentPressedButton.m_PositionOnBoard.X;
                oldCol = m_CurrentPressedButton.m_PositionOnBoard.Y;
            }

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
            if (currentButtonPressedIsCurrentPlayer(i_Row, i_Col))
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
            m_CurrentPressedButton.BackColor = default(Color);
            for(int i = 0; i < m_GameSize; i++)
            {
                for(int j = 0; j < m_GameSize; j++)
                {
                    if(m_GameButtons[i, j].Enabled)
                    {
                        m_GameButtons[i, j].BackColor = default(Color);
                    }
                }
            }
        }

        private void initGameBoard()
        {
            int left, top = (Top / 2 - m_GameSize);
            for(int i = 0; i < m_GameSize; i++)
            {
                left = Left / 2 - (m_GameSize - 1);
                for(int j = 0; j < m_GameSize; j++)
                {
                    m_GameButtons[i, j] = new UpgradedButton(new Point(i, j));
                    m_GameButtons[i, j].Location = new Point(left, top);
                    m_GameButtons[i, j].Size = m_ButtonSize;
                    left += m_ButtonSize.Width;
                    if((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        m_GameButtons[i, j].Enabled = false;
                    }

                    else
                    {
                        m_GameButtons[i, j].BackColor = default(Color);
                    }

                    if((i < (m_GameSize / 2) - 1) && ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)))
                    {
                        m_GameButtons[i, j].Text = "O";
                        m_Player2CoinSet.Add(m_GameButtons[i, j]);
                    }

                    else if(i > (m_GameSize / 2) && ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)))
                    {
                        m_GameButtons[i, j].Text = "X";
                        m_Player1CoinSet.Add(m_GameButtons[i, j]);
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
            m_PlayerOne.Text = m_PlayerOneName + ":" + m_Player1Points;
            m_PlayerTwo.Text = m_PlayerTwoName + ":" + m_Player2Points;
            int playerTwoLabelLeftMargin = m_GameSize * m_ButtonSize.Width - 2 * m_PlayerTwo.Text.Length - Left / 10;
            Point playerOnePoint = new Point(Left / 10, 15);
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

        private void resetCoinPossibleEatingsAndMoves(UpgradedButton i_Button)
        {
            i_Button.m_CanEatDownLeft = false;
            i_Button.m_CanEatDownRight = false;
            i_Button.m_CanEatUpLeft = false;
            i_Button.m_CanEatUpRight = false;

            i_Button.m_CanMoveUpRight = false;
            i_Button.m_CanMoveUpLeft = false;
            i_Button.m_CanMoveDownLeft = false;
            i_Button.m_CanMoveDownRight = false;
        }

        private void checkAndUpdateWhoCanEatForCurrentPlayer()
        {
            m_EatingIsPossible = false;
            if (m_CurrentTurn == PlayersTurn.ePlayersTurn.Player1)
            {
                foreach (UpgradedButton Coin in m_Player1CoinSet)
                {
                    resetCoinPossibleEatingsAndMoves(Coin);
                    canCoinEat(Coin);
                }
            }

            else
            {
                foreach (UpgradedButton Coin in m_Player2CoinSet)
                {
                    resetCoinPossibleEatingsAndMoves(Coin);
                    canCoinEat(Coin);
                }
            }
        }

        private void resetCheckAndUpdateWhoCanMoveForComputerPlayer()
        {
            foreach (UpgradedButton Coin in m_Player2CoinSet)
            {
                resetCoinPossibleEatingsAndMoves(Coin);
                canCoinMove(Coin);
            }
        }

        private void checkAndUpdateWhoCanMove(PlayersTurn.ePlayersTurn i_Player)
        {
            if(i_Player == PlayersTurn.ePlayersTurn.Player1)
            {
                foreach (UpgradedButton Coin in m_Player1CoinSet)
                {
                    canAnyCoinMove(Coin);
                }
            }
            else
            {
                foreach (UpgradedButton Coin in m_Player2CoinSet)
                {
                    canCoinMove(Coin);
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

        private void canAnyCoinMove(UpgradedButton i_Coin)
        {
            int coinRow = i_Coin.m_PositionOnBoard.X;
            int coinCol = i_Coin.m_PositionOnBoard.Y;

            if (coinRow < m_GameSize - 1 && coinCol < m_GameSize - 1 && m_GameButtons[coinRow + 1, coinCol + 1].Text == "")
            {
                i_Coin.m_CanMoveDownRight = true;
            }

            if (coinRow < m_GameSize - 1 && coinCol >= 1 && m_GameButtons[coinRow + 1, coinCol - 1].Text == "")
            {
                i_Coin.m_CanMoveDownLeft = true;
            }

            if (coinRow >0 && coinCol < m_GameSize - 1 && m_GameButtons[coinRow - 1, coinCol + 1].Text == "")
            {
                i_Coin.m_CanMoveUpRight = true;
            }

            if (coinRow > 0 && coinCol >= 1 && m_GameButtons[coinRow - 1, coinCol - 1].Text == "")
            {
                i_Coin.m_CanMoveUpLeft = true;
            }
        }

        private void canCoinMove(UpgradedButton i_Coin)
        {
            int coinRow = i_Coin.m_PositionOnBoard.X;
            int coinCol = i_Coin.m_PositionOnBoard.Y;
            if (i_Coin.Text == "U")
            {
                if (coinRow >= 1 && coinCol < m_GameSize - 1 && m_GameButtons[coinRow - 1, coinCol + 1].Text == "")
                {
                    i_Coin.m_CanMoveUpRight = true;
                }

                if (coinRow >= 1 && coinCol >= 1 && m_GameButtons[coinRow - 1, coinCol - 1].Text == "")
                {
                    i_Coin.m_CanMoveUpLeft = true;
                }
            }

            if (coinRow < m_GameSize - 1 && coinCol < m_GameSize - 1 && m_GameButtons[coinRow + 1, coinCol + 1].Text == "")
            {
                i_Coin.m_CanMoveDownRight = true;
            }

            if (coinRow < m_GameSize - 1 && coinCol >= 1 && m_GameButtons[coinRow + 1, coinCol - 1].Text == "")
            {
                i_Coin.m_CanMoveDownLeft = true;
            }
        }

    }
}
