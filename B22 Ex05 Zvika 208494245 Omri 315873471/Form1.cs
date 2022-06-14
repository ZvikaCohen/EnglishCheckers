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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SixOnSixButton_CheckedChanged(object sender, EventArgs e)
        {
            if(SixOnSixButton.Checked)
            {
                EightOnEightButton.Checked = false;
                TenOnTenButton.Checked = false;
            }
            
        }

        private void EightOnEightButton_CheckedChanged(object sender, EventArgs e)
        {
            if(EightOnEightButton.Checked)
            {
                SixOnSixButton.Checked = false;
                TenOnTenButton.Checked = false;
            }
        }

        private void TenOnTenButton_CheckedChanged(object sender, EventArgs e)
        {
            if(TenOnTenButton.Checked)
            {
                SixOnSixButton.Checked = false;
                EightOnEightButton.Checked = false;
            }
        }

        private void PlayersLabel_Click_1(object sender, EventArgs e)
        {

        }

        private void PlayerOneNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PlayerTwoNameTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PlayerTwoCheckBox_Clicked(object sender, EventArgs e)
        {
            if(PlayerTwoCheckBox.Checked)
            {
                PlayerTwoNameTextBox.Enabled = true;
                PlayerTwoNameTextBox.Text = "";
            }
            else
            {
                PlayerTwoNameTextBox.Enabled = false;
                PlayerTwoNameTextBox.Text = "[Computer]";
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            GameBoardForm gameBoard = new GameBoardForm(getGameSize(), PlayerOneNameTextBox.Text, PlayerTwoNameTextBox.Text);
            gameBoard.StartPosition = FormStartPosition.CenterScreen;
            gameBoard.ShowDialog();
        }

        private string getGameSize()
        {
            string gameSize = "";
            if(SixOnSixButton.Checked)
            {
                gameSize = SixOnSixButton.Text;
            }

            if (EightOnEightButton.Checked)
            {
                gameSize = EightOnEightButton.Text;
            }

            if (TenOnTenButton.Checked)
            {
                gameSize = TenOnTenButton.Text;
            }

            return gameSize;
        }
    }
}
